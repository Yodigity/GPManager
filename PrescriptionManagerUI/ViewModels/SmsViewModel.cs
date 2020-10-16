using AutoMapper;
using Caliburn.Micro;
using PrescriptionManagerUI.EventModels;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrescriptionManagerUI.ViewModels
{
    public class SmsViewModel : Screen
    {

        StatusInfoViewModel _status;
        IMapper _mapper;
        IWindowManager _window;
        IEventAggregator _events;
        IPatientEndpoint _patientEndpoint;
        ISmsEndpoint _smsEndpoint;
      
        

        public SmsViewModel(StatusInfoViewModel status,
            IWindowManager window, IMapper mapper, IEventAggregator events, IPatientEndpoint patientEndpoint, ISmsEndpoint smsEndpoint)
        { 
        
            _status = status;
            _mapper = mapper;
            _window = window;
            _events = events;
            _patientEndpoint = patientEndpoint;
            _smsEndpoint = smsEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadPatientPrescriptions();



            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";




                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized access", "You do not have permission to access this page");
                    await _window.ShowDialog(_status, null, settings);

                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    await _window.ShowDialog(_status, null, settings);

                }
            }
        }

        private async Task LoadPatientPrescriptions()
        {
            var patientList = await _patientEndpoint.GetPatientPrescriptionRenewals();
            var patients = _mapper.Map<List<PatientPrescriptionRenewalDisplayModel>>(patientList);
            Patients = new BindingList<PatientPrescriptionRenewalDisplayModel>(patients);

            
        }


        private BindingList<PatientPrescriptionRenewalDisplayModel> _patients;

        public BindingList<PatientPrescriptionRenewalDisplayModel> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                NotifyOfPropertyChange(() => Patients);
            }
        }

        public PatientPrescriptionRenewalDisplayModel _selectedPatient;

        public PatientPrescriptionRenewalDisplayModel SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                NotifyOfPropertyChange(() => SelectedPatient);
                NotifyOfPropertyChange(() => CanAddToStage);
            }

        }

        private PatientPrescriptionRenewalDisplayModel _selectedStagePatient;

        public PatientPrescriptionRenewalDisplayModel SelectedStagePatient
        {
            get { return _selectedStagePatient; }
            set
            {
                _selectedStagePatient = value;
                NotifyOfPropertyChange(() => SelectedStagePatient);
                NotifyOfPropertyChange(() => CanRemoveFromStage);
            }
        }


        private BindingList<PatientPrescriptionRenewalDisplayModel> _stage = new BindingList<PatientPrescriptionRenewalDisplayModel>();

        public BindingList<PatientPrescriptionRenewalDisplayModel> Stage
        {
            get { return _stage; }
            set
            {
                _stage = value;
                NotifyOfPropertyChange(() => Stage);
                NotifyOfPropertyChange(() => CanRemoveFromStage);
            }
        }

        public bool CanAddToStage
        {
            get
            {
                bool output = false;
                if (SelectedPatient != null)
                {
                    output = true;

                }
                return output;
            }
        }

        public void AddToStage()
        {
            if (SelectedPatient != null)
            {
                PatientPrescriptionRenewalDisplayModel Item = new PatientPrescriptionRenewalDisplayModel
                {
                    PatientId = SelectedPatient.PatientId,
                    FirstName = SelectedPatient.FirstName,
                    LastName = SelectedPatient.LastName,
                    PhoneNumber = SelectedPatient.PhoneNumber,
                    PrescriptionId = SelectedPatient.PrescriptionId,
                    DrugName = SelectedPatient.DrugName,
                    RenewalDate = SelectedPatient.RenewalDate
                };
                Stage.Add(Item);
                Patients.Remove(SelectedPatient);
                NotifyOfPropertyChange(() => CanAddToStage);
            }


        }

        public bool CanRemoveFromStage
        {
            get
            {
                var output = false;
                if (SelectedStagePatient != null)
                {
                    output = true;
                }
                return output;
            }
        }

        public void RemoveFromStage()
        {
            if(SelectedStagePatient != null)
            {
                

                PatientPrescriptionRenewalDisplayModel Item = new PatientPrescriptionRenewalDisplayModel
                {
                    PatientId = SelectedStagePatient.PatientId,
                    FirstName = SelectedStagePatient.FirstName,
                    LastName = SelectedStagePatient.LastName,
                    PrescriptionId = SelectedStagePatient.PrescriptionId,
                    DrugName = SelectedStagePatient.DrugName,
                    RenewalDate = SelectedStagePatient.RenewalDate
                };
                Stage.Remove(SelectedStagePatient);
                Patients.Add(Item);

                NotifyOfPropertyChange(() => CanRemoveFromStage);
            }
            
        }

        class ItemEqualityComparer : IEqualityComparer<PatientPrescriptionRenewalDisplayModel>
        {
            public bool Equals(PatientPrescriptionRenewalDisplayModel x, PatientPrescriptionRenewalDisplayModel y)
            {
                // Two items are equal if their keys are equal.
                return x.PatientId == y.PatientId;
            }

            public int GetHashCode(PatientPrescriptionRenewalDisplayModel obj)
            {
                return obj.PatientId.GetHashCode();
            }

        
        }

        public void SendMessages()
        {
            List<PatientPrescriptionRenewalDisplayModel> refinedPatientList = Stage.Distinct(new ItemEqualityComparer()).ToList();

            var patients = _mapper.Map<List<PatientPrescriptionRenewalModel>>(refinedPatientList);

           
            _smsEndpoint.SendMessages(patients);

        }






        private int _prescriptionId;

        public int PrescriptionId
        {
            get { return _prescriptionId; }
            set
            {
                _prescriptionId = value;
                NotifyOfPropertyChange(() => PrescriptionId);
            }
        }


        public void Exit()
        {
            _events.PublishOnUIThread(new LogoutEvent());
        }

    }
}
