using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.Models
{
    public class SmsMessageModel
    {
        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public string PatientPhoneNumber { get; set; }

        public string GpPracticeNumber { get; set; }

        public string Message = "Dear patient. Out records show that one of your prescriptions is due for medical review. Please contact your GP at nearest opportunity";

    }
}
