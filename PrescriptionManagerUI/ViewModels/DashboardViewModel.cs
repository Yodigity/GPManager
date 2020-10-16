using AutoMapper;
using Caliburn.Micro;
using PrescriptionManagerUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using PrescriptionManagerUI.Models;
using PrescriptionManagerUI.Views;
using PrescriptionManagerUI.EventModels;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Helpers;

namespace PrescriptionManagerUI.ViewModels
{
    public class DashboardViewModel : Screen
    {
        IUserEndpoint _userEndpoint;
        IPatientEndpoint _patientEndpoint;
        StatusInfoViewModel _status;
        IWindowManager _window;
        IMapper _mapper;
        IEventAggregator _events;
        IPrescriptionEndpoint _prescriptionEndpoint;
        PatientModel selectedPatient;
        PrescriptionModel selectedPrescription;

        public DashboardViewModel(IUserEndpoint userEndpoint, IPatientEndpoint patientEndpoint, IPrescriptionEndpoint prescriptionEndpoint, StatusInfoViewModel status, IWindowManager window, IMapper mapper, IEventAggregator events)
        {
            _userEndpoint = userEndpoint;
            _patientEndpoint = patientEndpoint;
            _prescriptionEndpoint = prescriptionEndpoint;
            _status = status;
            _window = window;
            _mapper = mapper;
            _events = events;



        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadPatients();
                await GetPrescriberId();

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

        private async Task LoadPatients()
        {
            var patientList = await _patientEndpoint.GetAll();
            var patients = _mapper.Map<List<PatientDisplayModel>>(patientList);
            Patients = new BindingList<PatientDisplayModel>(patients);
        }

        private BindingList<PatientDisplayModel> _patients;

        public BindingList<PatientDisplayModel> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                NotifyOfPropertyChange(() => Patients);
            }
        }

        public PatientDisplayModel _selectedPatient;

        public PatientDisplayModel SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                NotifyOfPropertyChange(() => SelectedPatient);
            }

        }

        private BindingList<PatientModel> _patientDetailBox = new BindingList<PatientModel>();

        public BindingList<PatientModel> PatientDetailBox
        {
            get { return _patientDetailBox; }
            set
            {
                PatientDetailBox = value;
                NotifyOfPropertyChange(() => PatientDetailBox);
                

            }
        }

        public bool CanEditPatient
        {
            get
            {
                var output = false;
                if (PatientDetailBox.Count > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        private BindingList<PrescriptionModel> _prescriptionDetailBox = new BindingList<PrescriptionModel>();

        public BindingList<PrescriptionModel> PrescriptionDetailBox
        {
            get { return _prescriptionDetailBox; }
            set
            {
                _prescriptionDetailBox = value;
                NotifyOfPropertyChange(() => PrescriptionDetailBox);
                NotifyOfPropertyChange(() => EnablePrescriptionController);
                
            }
        }

        private PrescriptionModel _selectedPrescription = new PrescriptionModel();

        public PrescriptionModel SelectedPrescription
        {
            get { return _selectedPrescription; }
            set
            {
                _selectedPrescription = value;
                NotifyOfPropertyChange(() => SelectedPrescription);
            }
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


        private string _drugName;

        public string DrugName
        {
            get { return _drugName; }
            set
            {
                _drugName = value;
                NotifyOfPropertyChange(() => DrugName);
                NotifyOfPropertyChange(() => CanAddPrescription);
                NotifyOfPropertyChange(() => EnablePrescriptionController);
            }
        }

        private double _dosage;

        public double Dosage
        {
            get { return _dosage; }
            set
            {
                _dosage = value;
                NotifyOfPropertyChange(() => Dosage);
                NotifyOfPropertyChange(() => CanAddPrescription);
                NotifyOfPropertyChange(() => EnablePrescriptionController);
            }
        }

        private string _prescriberId;

        public string PrescriberId
        {
            get { return _prescriberId; }
            set
            {
                _prescriberId = value;
                NotifyOfPropertyChange(() => PrescriberId);
            }
        }

        private DateTime _renewalDate = DateTime.Now;

        public DateTime RenewalDate
        {
            get { return _renewalDate; }
            set
            {
                _renewalDate = value;
                NotifyOfPropertyChange(() => RenewalDate);
                NotifyOfPropertyChange(() => CanAddPrescription);
                NotifyOfPropertyChange(() => EnablePrescriptionController);
            }
        }



 



        public async Task ViewDetails(int patientId = 0)
        {
            PatientModel patientDetails;
            if(patientId != 0)
            {
                 patientDetails = await _patientEndpoint.GetPatientDetails(patientId);
            }
            else
            {
                 patientDetails = await _patientEndpoint.GetPatientDetails(SelectedPatient.Id);

            }

            PatientDetailBox.Clear();
            PrescriptionDetailBox.Clear();


           
            PatientModel patient = new PatientModel
            {
                ID = patientDetails.ID,
                FirstName = patientDetails.FirstName,
                LastName = patientDetails.LastName,
                DOB = patientDetails.DOB,
                Email = patientDetails.Email,
                PhoneNumber = patientDetails.PhoneNumber,
                Prescriptions = patientDetails.Prescriptions
            };

            foreach (var item in patient.Prescriptions)
            {
                PrescriptionDetailBox.Add(item);
            }
            PatientDetailBox.Add(patient);
            selectedPatient = patient;

            ResetPrescription();

            

        }
        public void ViewNotes(int patientId = 0)
        {
            /*PatientNoteViewModel w = new PatientNoteViewModel();
      
            Activate*/

             _events.PublishOnUIThreadAsync(new OpenPatientNoteEvent());


        }

        /*  public bool _editModeActive = false;
          public bool EditModeActive
          {
              get
              {
                  return _editModeActive;

               }
              set 
              {
                  EditModeActive = value;
                  NotifyOfPropertyChange(() => EditModeActive);
              }

          } */

        /*  public void EditPatient()
          {
              EditModeActive = true;

              NotifyOfPropertyChange(() => PatientDetailBox);
          }*/

        /*   public void CancelEditPatient()
           {
               EditModeActive = false;

               NotifyOfPropertyChange(() => PatientDetailBox);

           }*/

        public async Task EditPatientDetails()
        {
            var formattedPhoneNumber = FormatPhoneNumberHelper.FormatPhoneNumber(PatientDetailBox.First().PhoneNumber);
            var patient = new PatientModel
            {
                ID = PatientDetailBox.First().ID,
                FirstName = PatientDetailBox.First().FirstName,
                LastName = PatientDetailBox.First().LastName,
                Email = PatientDetailBox.First().Email,
                PhoneNumber = formattedPhoneNumber
            };

            await _patientEndpoint.EditPatient(patient);

            await LoadPatients();

            
        }
        

        public async Task GetPrescriberId()
        {
            var userData = await _userEndpoint.GetUserId();

            var userName = $"{userData.FirstName} {userData.LastName}" ;
            PrescriberId = userName;

        }

        public bool EnablePrescriptionController
        {

            get
            {
                bool output = false;
                if (PatientDetailBox.Count > 0)
                {
                    output = true;
                }

                return output;
            }

        }

        public bool CanAddPrescription
        {
            get{
                bool output = false;

                if (DrugName?.Length > 0 && Dosage > 0 && RenewalDate > DateTime.Now)
                {
                    output = true;
                }
                return output;
            }

            
        }

        public async Task AddPrescription()
        {
            //ErrorMessage = "";

            if (PrescriptionId != 0 && DrugName != SelectedPrescription.DrugName)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                _status.UpdateMessage("Unauthorized", "Changing medication name is not allowed. Please create a new prescription");
                await _window.ShowDialog(_status, null, settings);

                DrugName = selectedPrescription.DrugName;
            }
            else
            {
                var prescription = new PrescriptionModel
                {
                    ID = PrescriptionId,
                    PatientID = PatientDetailBox.First().ID,
                    DrugName = DrugName,
                    Dosage = Dosage,
                    PrescriberId = PrescriberId,
                    RenewalDate = RenewalDate
                };


                await _prescriptionEndpoint.AddPrescription(prescription);

                ResetPrescription();

                await ViewDetails(prescription.PatientID);
            }

        }

        public void SelectPrescription()
        {
            selectedPrescription = new PrescriptionModel
            {
                ID = SelectedPrescription.ID,
                DrugName = SelectedPrescription.DrugName,
                Dosage = SelectedPrescription.Dosage,
                RenewalDate = SelectedPrescription.RenewalDate
            };

            PrescriptionId = SelectedPrescription.ID;
            DrugName = SelectedPrescription.DrugName;
            Dosage = SelectedPrescription.Dosage;
            RenewalDate = SelectedPrescription.RenewalDate;
        }

        public void CancelPrescription()
        {
            ResetPrescription();
        }

        public void ResetPrescription()
        {
            PrescriptionId = 0;
            DrugName = "";
            Dosage = 0;
            RenewalDate = DateTime.Now;
        }


        //Currently not in use due to inability to data share with dashboard.
        public async Task OpenAddView()
        {
            PatientModel selectedPatient = new PatientModel
            {
                ID = PatientDetailBox.First().ID,
                FirstName = PatientDetailBox.First().FirstName,
                LastName = PatientDetailBox.First().LastName
            };
          

            await _events.PublishOnUIThreadAsync(new OpenAddPrescriptionEvent());
        }






        /*     public async Task ViewPatientDetails()
             {
                 PatientModel existingItem = PatientDetailBox.First(x => x.ID == SelectedPatient.Id);
                 if (existingItem != null)
                 {
                     PatientDetailBox.Clear();
                     await ViewDetails();
                 }
                 else
                 {
                     await ViewDetails();

                 }



             }*/
    }
}



