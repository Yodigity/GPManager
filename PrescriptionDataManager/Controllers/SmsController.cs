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
    public class SmsController : ApiController
    {

       /* [Route("api/Sms/GetPhoneNumbers")]
        public List<String> GetPhoneNumbers()
        {

        }*/


        [Route("api/Sms/Send")]
        public void SendMessages(PatientModel patientData)
        {
            PatientData data = new PatientData();

            data.SendMessages();
        } 

    }
}
