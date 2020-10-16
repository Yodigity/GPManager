using AutoMapper;
using Caliburn.Micro;
using PrescriptionDataManager.Library.API;
using PrescriptionManagerUI.EventModels;
using PrescriptionManagerUI.Library.API;
using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Models;
using PrescriptionManagerUI.Views;
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
    public class PatientNoteViewModel: Screen
    { 
        StatusInfoViewModel _status;
        IEventAggregator _events;
        IWindowManager _window;
        IMapper _mapper;
        IPatientEndpoint _patientEndpoint;
        IUserEndpoint _userEndpoint;
        INoteEndpoint _noteEndpoint;

        
        public int PatientId { get; set; }
        public PatientNoteViewModel( StatusInfoViewModel status, IEventAggregator events, IWindowManager window, IMapper mapper, IPatientEndpoint patientEndpoint, IUserEndpoint userEndpoint
            , INoteEndpoint noteEndpoint) 
        {
            _status = new StatusInfoViewModel();
            _events = events;
            _window = new WindowManager();
            _mapper = mapper;
            _patientEndpoint = patientEndpoint;
            _userEndpoint = userEndpoint;
            _noteEndpoint = noteEndpoint;
        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadPatientData();
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

        private async Task LoadPatientData()
        {
            var patientList = await _patientEndpoint.GetAll();
            var patients = _mapper.Map<List<PatientDisplayModel>>(patientList);
            Patients = new BindingList<PatientDisplayModel>(patients);
        }

        public async Task GetPrescriberId()
        {
            var userData = await _userEndpoint.GetUserId();

            var userName = $"{userData.FirstName} {userData.LastName}";
            PrescriberId = userName;

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

        private BindingList<PatientNoteDisplayModel> _notes = new BindingList<PatientNoteDisplayModel>();

        public BindingList<PatientNoteDisplayModel> Notes
        {
            get { return _notes; }
            set 
            { 
                _notes = value;
                NotifyOfPropertyChange(() => Notes);
            }
        }

        private PatientNoteDisplayModel _selectedNote;

        public PatientNoteDisplayModel SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                NotifyOfPropertyChange(() => SelectedNote);
            }
        }



        public async Task ViewNotes(int patientId = 0)
        {
            
            var patientNotesList = await _patientEndpoint.GetPatientNotes(SelectedPatient.Id);
            var notes = _mapper.Map<List<PatientNoteDisplayModel>>(patientNotesList);
          
            Notes = new BindingList<PatientNoteDisplayModel>(notes);
        }

        public void OpenNote()
        {
            SelectedNoteView w = new SelectedNoteView();

            w.NoteTitle.Text = SelectedNote.Title;
            w.NoteText.Text = SelectedNote.Text;

            w.Show();


        }

        public bool CanAddNote
        {
            get
            {
                var output = false;
                if (AddNoteTitle?.Length > 0 && AddNoteText?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        private string _addNoteTitle;

        public string AddNoteTitle
        {
            get { return _addNoteTitle; }
            set 
            {
                _addNoteTitle = value;
                NotifyOfPropertyChange(() => AddNoteTitle);
                NotifyOfPropertyChange(() => CanAddNote);
                    }
        }

        private string _addNoteText;

        public string AddNoteText
        {
            get { return _addNoteText; }
            set 
            { 
                _addNoteText = value;
                NotifyOfPropertyChange(() => AddNoteText);
                NotifyOfPropertyChange(() => CanAddNote);
            }
        }

        public async Task AddNote()
        {
            var newNote = new PatientNoteModel
            {
                PatientId = SelectedPatient.Id,
                Title = AddNoteTitle.Trim(),
                Text = AddNoteText.Trim(),
                AuthorId = PrescriberId,
            };

            await _noteEndpoint.AddNote(newNote);

            ClearAddNoteSection();


        }

        public void CancelNote()
        {
            ClearAddNoteSection();
        }

        private void ClearAddNoteSection()
        {
            AddNoteTitle = "";
            AddNoteText = "";
            NotifyOfPropertyChange(() => AddNoteTitle);
            NotifyOfPropertyChange(() => AddNoteText);
        }

        public void Exit()
        {
            _events.PublishOnUIThread(new LogoutEvent());
        }
    }
}
