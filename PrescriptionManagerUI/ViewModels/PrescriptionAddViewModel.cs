using Caliburn.Micro;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Windows;

namespace PrescriptionManagerUI.ViewModels
{
    public class PrescriptionAddViewModel : Screen
    {
        IPatientEndpoint _patientEndpoint;
        IPrescriptionEndpoint _prescriptionEndpoint;
        StatusInfoViewModel _status;
        IWindowManager _window;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
      


        public PrescriptionAddViewModel(IPatientEndpoint patientEndpoint, IPrescriptionEndpoint prescriptionEndpoint, 
            StatusInfoViewModel status, IWindowManager window, IAPIHelper apiHelper, IEventAggregator eventAggregator)
        {
            
            _status = status;
            _window = window;
            _apiHelper = apiHelper;
            _events = eventAggregator;
     
            _patientEndpoint = patientEndpoint;
            _prescriptionEndpoint = prescriptionEndpoint;
            

     /*       PatientId = _patient?.ID.ToString();
            PatientFirstName = _patient?.FirstName;
            PatientLastName = _patient?.LastName;*/
            

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {


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

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        private string _patientId;

        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        private string _patientFirstName;

        public string PatientFirstName
        {
            get { return _patientFirstName; }
            set 
            { 
                _patientFirstName = value;
                NotifyOfPropertyChange(() => PatientFirstName);
            }
        }

        private string  _patientLastName;

        public string  PatientLastName
        {
            get { return _patientLastName; }
            set 
            { 
                _patientLastName = value;
                NotifyOfPropertyChange(() => PatientLastName);
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
                //NotifyOfPropertyChange(() => CanAddPrescription);
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
               // NotifyOfPropertyChange(() => CanAddPrescription);
            }
        }

        private string _prescriberId;

        public string PrescriberId
        {
            get { return _prescriberId; }
            set { _prescriberId = value; }
        }

        private DateTime _renewalDate;

        public DateTime RenewalDate
        {
            get { return _renewalDate; }
            set 
            { 
                _renewalDate = value;
                NotifyOfPropertyChange(() => RenewalDate);
                //NotifyOfPropertyChange(() => CanAddPrescription);
            }
        }

       /* public bool CanAddPrescription
        {
            get{
                bool output = false;

                if (DrugName.Length > 0 && Dosage > 0 & RenewalDate != null)
                {
                    output = true;
                }
                return output;
            }

        }*/

        public async Task AddPrescription()
        {
            ErrorMessage = "";

        

            var prescription = new PrescriptionModel
            {
                PatientID = Int32.Parse(PatientId),
                DrugName = DrugName,
                Dosage = Dosage,
                PrescriberId = PrescriberId,
                RenewalDate = RenewalDate
            };

            await _prescriptionEndpoint.AddPrescription(prescription);

            
        }


    }
}
