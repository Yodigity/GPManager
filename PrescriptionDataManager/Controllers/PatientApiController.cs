
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using PrescriptionDataManager.Library.DataAccess;
using PrescriptionDataManager.Library.Models;

namespace PrescriptionDataManager.Controllers
{
    public class PatientApiController : ApiController
    {
        [Route("api/Patient")]
        public List<PatientModel> Get()
        {
            PatientData data = new PatientData();
            return data.GetAllPatients();
        }

        [Authorize]
        [Route("api/PatientDetails")]
        public PatientModel GetbyId(int id)
        {
            PatientData data = new PatientData();
            return data.GetById(id);
        }


        [Authorize]
        [Route("api/Patient/Renewals")]
        public List<PatientPrescriptionRenewalModel> GetPatientPrescriptionRenewals()
        {
            PatientData data = new PatientData();
           return data.GetPatientPrescriptionRenewals();

        }


        [Authorize]
        [Route("api/Patient/Add")]
        public void AddPatient(PatientModel patientData)
        {
            PatientData data = new PatientData();

            data.AddPatient(patientData);
        }

        [Authorize]
        [Route("api/Patient/Edit")]
        public void EditPatient(PatientModel patientData)
        {
            PatientData data = new PatientData();

            data.EditPatient(patientData);
        }
    }
}
