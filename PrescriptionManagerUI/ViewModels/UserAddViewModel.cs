using Caliburn.Micro;
using PrescriptionManagerUI.EventModels;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrescriptionManagerUI.ViewModels
{
    public class UserAddViewModel: Screen
    {
        IWindowManager _window;
        StatusInfoViewModel _status;
        IEventAggregator _events;
        IUserEndpoint _userEndpoint;
        private IAPIHelper _apiHelper;
        public UserAddViewModel(IWindowManager window, StatusInfoViewModel status, IEventAggregator events, IUserEndpoint userEndpoint, IAPIHelper apiHelper)
        {
            _window = window;
            _status = status;
            _events = events;
            _userEndpoint = userEndpoint;
            _apiHelper = apiHelper;
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

       

       private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
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
            }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set 
            { 
                _confirmPassword = value;
                NotifyOfPropertyChange(() => ConfirmPassword);
            }
        }





        public void AddUser() 
        {
            var userDetails = new UserAddModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
            _userEndpoint.AddUser(userDetails);
        }

        public void Exit()
        {
            _events.PublishOnUIThread(new LogoutEvent());
        }

    }
}
