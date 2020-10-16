using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrescriptionManagerUI.Library.Models
{
    public class PrescriptionModel
    {
        public int ID { get; set; }
        public int PatientID { get; set; }

        public string DrugName { get; set; }

        public double Dosage { get; set; }
        public string PrescriberId { get; set; }
        public DateTime RenewalDate { get; set; }

        public virtual PatientModel Patient { get; set; }
    }
}