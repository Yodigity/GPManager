using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Models
{
    public class PrescriptionDisplayModel
    {
        public int ID { get; set; }
        public int PatientID { get; set; }

        public string DrugName { get; set; }

        public double Dosage { get; set; }
        public DateTime RenewalDate { get; set; }
    }
}
