using PrescriptionDataManager.Library.DataAccess;
using PrescriptionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PrescriptionDataManager.Controllers
{
    public class NoteController : ApiController
    {

        [Route("api/Notes")]
        public List<PatientNoteModel> GetPatientNotes(int patientId)
        {
            PatientData data = new PatientData();
            return data.GetPatientNotes(patientId);
        }

        [Route("api/Notes/Add")]
        public void AddNote(PatientNoteModel note)
        {
            NoteData data = new NoteData();
            data.AddNote(note);
        }
    }
}
