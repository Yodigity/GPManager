using PrescriptionManagerUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public interface IPatientEndpoint
    {
        Task AddPatient(PatientModel patientData);
        Task<List<PatientModel>> GetAll();
        Task<PatientModel> GetPatientDetails(int id);
        Task EditPatient(PatientModel patientData);
        Task<List<PatientPrescriptionRenewalModel>> GetPatientPrescriptionRenewals();
        Task<List<PatientNoteModel>> GetPatientNotes(int patientId);
    }
}