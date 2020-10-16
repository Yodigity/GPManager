using PrescriptionManagerUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public interface ISmsEndpoint
    {
        void SendMessages(List<PatientPrescriptionRenewalModel> patientData);
    }
}