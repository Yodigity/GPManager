using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using PrescriptionManagerUI.EventModels;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Helpers;

namespace PrescriptionManagerUI.ViewModels
{
    public class PatientAddViewModel : Screen
    {
        IPatientEndpoint _patientEndpoint;
        private IEventAggregator _events;
        private StatusInfoViewModel _status;
        private IWindowManager _window;

        public PatientAddViewModel(IPatientEndpoint patientEndpoint, IEventAggregator events, StatusInfoViewModel status, IWindowManager window)
        {
            _status = status;
            _window = window;
            _events = events;
            _patientEndpoint = patientEndpoint;
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
                    _window.ShowDialog(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    _window.ShowDialog(_status, null, settings);

                }
                _events.PublishOnUIThread(new LogoutEvent());
                //TryClose();
            }
        }
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanAddPatient);
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanAddPatient);
            }
        }

        private DateTime _dateOfBirth = DateTime.Now;

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                NotifyOfPropertyChange(() => DateOfBirth);
                NotifyOfPropertyChange(() => CanAddPatient);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => CanAddPatient);
            }
        }

        private string _phoneNumber;


        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);
                NotifyOfPropertyChange(() => CanAddPatient);
            }
        }

        public bool CanAddPatient
        {
            get
            {
                bool output = false;
                if (FirstName?.Length > 0 && LastName?.Length > 0 && PhoneNumber?.Length > 10)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task AddPatient()
        {

            string formattedPhoneNumber = FormatPhoneNumberHelper.FormatPhoneNumber(PhoneNumber);

            var patientData = new PatientModel
            {
                FirstName = FirstName,
                LastName = LastName,
                DOB = DateOfBirth,
                Email = Email,
                PhoneNumber = formattedPhoneNumber
            };


            await _patientEndpoint.AddPatient(patientData);

        }

        public void Exit()
        {
            _events.PublishOnUIThread(new LogoutEvent());
        }

    }
}

