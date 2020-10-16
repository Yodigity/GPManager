using AutoMapper;
using Caliburn.Micro;
using PrescriptionDataManager.Helpers;
using PrescriptionDataManager.Library.API;
using PrescriptionDataManager.Library.Models;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Models;
using PrescriptionManagerUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrescriptionDataManager
{
    class Bootstrapper : BootstrapperBase
    {
        SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        private IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PatientModel, PatientDisplayModel>();
                cfg.CreateMap<PatientPrescriptionRenewalDisplayModel, PatientPrescriptionRenewalModel>();
                cfg.CreateMap<PatientPrescriptionRenewalModel, PatientPrescriptionRenewalDisplayModel>();
                cfg.CreateMap<PatientNoteModel, PatientNoteDisplayModel>();
                //cfg.CreateMap<PatientNoteModel>
                //TODO: Register PatientDetails ???
            });

            var output = config.CreateMapper();

            return output;
        }

        protected override void Configure()
        {
            _container.Instance(ConfigureAutoMapper());

            _container.Instance(_container)
                .PerRequest<IPatientEndpoint, PatientEndpoint>()
                .PerRequest<IUserEndpoint, UserEndpoint>()
                .PerRequest<ISmsEndpoint, SmsEndpoint>()
                .PerRequest<INoteEndpoint, NoteEndpoint>()
                .PerRequest<IPrescriptionEndpoint, PrescriptionEndpoint>();




            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IAPIHelper, APIHelper>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
