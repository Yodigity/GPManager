using Caliburn.Micro;
using PrescriptionDataManager.EventModels;
using PrescriptionDataManager.Library.API;
using PrescriptionDataManager.Library.Models;
using PrescriptionManagerUI.EventModels;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogonEvent>, IHandle<LogoutEvent>, IHandle<OpenPatientNoteEvent> //IHandle<OpenAddPrescriptionEvent>
    {
        private LoginViewModel _loginVM;
        private IEventAggregator _events;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;
        public PatientModel _selectedPatient;

        public ShellViewModel(LoginViewModel loginVM, IEventAggregator events, ILoggedInUserModel user, IAPIHelper apiHelper)
        {

            _loginVM = loginVM;

            _events = events;
            _events.Subscribe(this);


            _user = user;
            _apiHelper = apiHelper;



            ActivateItem(_loginVM);
        }


        public bool IsLoggedIn
        {
            get
            {
                var output = false;
                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }
                return output;
            }
        }

        public void Logout()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void ExitApplication()
        {
            TryClose();
        }
        public void UserManagement()
        {
             ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void AddUser()
        {
            ActivateItem(IoC.Get<UserAddViewModel>());
        }

        public void PrescriptionRenewalManager()
        {
            ActivateItem(IoC.Get<SmsViewModel>());
        }

        public void AddPatient()
        {
            ActivateItem(IoC.Get<PatientAddViewModel>());
        }

        public void Handle(LogonEvent message)
        {
            ActivateItem(IoC.Get<DashboardViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void Handle(LogoutEvent message)
        {
            if (IsLoggedIn == true)
            {
                ActivateItem(IoC.Get<DashboardViewModel>());
            }
            else
            {
                Logout();
                NotifyOfPropertyChange(() => IsLoggedIn);
            }
        }

        public void Handle(OpenAddPrescriptionEvent message)
        {
            ActivateItem(IoC.Get<PrescriptionAddViewModel>());
        }

        public void Handle(OpenPatientNoteEvent message)
        {
            /*APIHelper apihelper = new APIHelper();
            PatientEndpoint patientEndpoint = new PatientEndpoint(apihelper);
            PatientNoteViewModel w = new PatientNoteViewModel( patientEndpoint);
            w.PatientId = message._patientId;*/
            ActivateItem(IoC.Get<PatientNoteViewModel>());
        }
    }
}
