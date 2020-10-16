using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Configuration;

namespace PrescriptionManagerUI.Library.API
{
    public class SmsEndpoint : ISmsEndpoint
    {
        IAPIHelper _apiHelper;


        public SmsEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public void SendMessages(List<PatientPrescriptionRenewalModel> patientData)
        {
            foreach (PatientPrescriptionRenewalModel patient in patientData)
            {
                var accountSid = ConfigurationManager.AppSettings["AccountSid"];
                var authToken = ConfigurationManager.AppSettings["authToken"];
                Console.WriteLine();
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: $"Dear {patient.FirstName} {patient.LastName}. Our records show that one of your prescriptions is due for medical review. Please contact your GP at nearest opportunity to arrange an appointment.",
                    from: new Twilio.Types.PhoneNumber("+17277777196"),
                    to: new Twilio.Types.PhoneNumber(patient.PhoneNumber)
                );

                Console.WriteLine(message.Sid);
            }
        }


    }
}
