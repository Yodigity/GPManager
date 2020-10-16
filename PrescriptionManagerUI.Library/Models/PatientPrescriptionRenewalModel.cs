using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.Models
{
    public class PatientPrescriptionRenewalModel
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DrugName { get; set; }
        public int PrescriptionId { get; set; }
        public DateTime RenewalDate { get; set; }

    }
}
