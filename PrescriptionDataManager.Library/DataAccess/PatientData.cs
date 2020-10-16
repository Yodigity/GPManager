using PrescriptionDataManager.Library.Internal.DataAccess;
using PrescriptionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionDataManager.Library.DataAccess
{
    public class PatientData
    {
        public List<PatientModel> GetAllPatients()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<PatientModel, dynamic>("dbo.spPatient_GetAll", new { }, "PrescriptionDataManagerUpdated");

            return output;
        }

        public PatientModel GetById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<PatientModel, dynamic>("dbo.spPatient_GetDetails", new { id }, "PrescriptionDataManagerUpdated").First();

            var prescriptionOutput = sql.LoadData<PrescriptionModel, dynamic>("dbo.Patient_GetPrescriptionDetails", new { id }, "PrescriptionDataManagerUpdated");

            prescriptionOutput.ForEach((prescription) => output.Prescriptions.Add(prescription));

            return output;
        }

        public List<PatientPrescriptionRenewalModel> GetPatientPrescriptionRenewals()
        {
            DateTime today = DateTime.Now;
            DateTime renewalDate = today.AddDays(14);
          

           
            SqlDataAccess sql = new SqlDataAccess();

            var renewalPrescriptions = new List<PatientPrescriptionRenewalModel>();

            var prescriptionOutput = sql.LoadData<PatientPrescriptionRenewalModel, dynamic>("dbo.spPrescription_GetPendingRenewals", new { renewalDate, today
            }, "PrescriptionDataManagerUpdated");


            foreach(PatientPrescriptionRenewalModel prescriptionData in prescriptionOutput)
            {
                var prescription = new PatientPrescriptionRenewalModel
                {
                    FirstName = prescriptionData.FirstName,
                    LastName = prescriptionData.LastName,
                    PhoneNumber = prescriptionData.PhoneNumber,
                    PrescriptionId = prescriptionData.PrescriptionId,
                    DrugName = prescriptionData.DrugName,
                    PatientId = prescriptionData.PatientId,
                    RenewalDate = prescriptionData.RenewalDate
                };

                renewalPrescriptions.Add(prescription);
            }

            return renewalPrescriptions;
        }

        public List<PatientNoteModel> GetPatientNotes(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var patientNotes = new List<PatientNoteModel>();

            var notesOutput = sql.LoadData<PatientNoteModel, dynamic>("dbo.spPatient_GetNotes", new { id }, "PrescriptionDataManagerUpdated");

            foreach(PatientNoteModel noteData in notesOutput)
            {
                var note = new PatientNoteModel
                {
                    Id = noteData.Id,
                    PatientId = noteData.Id,
                    Title = noteData.Title,
                    Text = noteData.Text,
                    AuthorId = noteData.AuthorId,
                    CreatedDate = noteData.CreatedDate

                };

                patientNotes.Add(note);
            }

            return patientNotes;
        }

        public void SendMessages()
        {
            Console.Write("Got to patient Data");
        }

        public void AddPatient(PatientModel patientData)
        {
            SqlDataAccess sql = new SqlDataAccess();

            try
            {
                sql.StartTransaction("PrescriptionDataManagerUpdated");

                sql.SaveDataInTransaction("dbo.spPatient_Add", new
                {
                    FirstName = patientData.FirstName,
                    LastName = patientData.LastName,
                    DOB = patientData.DOB,
                    Email = patientData.Email,
                    PhoneNumber = patientData.PhoneNumber

                });

                sql.CommitTransaction();
            }
            catch (Exception)
            {
                sql.RollbackTransation();
                throw;
            }

        }

        public void EditPatient(PatientModel patientData)
        {
            SqlDataAccess sql = new SqlDataAccess();

            try
            {
                sql.StartTransaction("PrescriptionDataManagerUpdated");

                sql.SaveDataInTransaction("dbo.spPatient_UpdateDetails", new
                {
                    Id = patientData.ID,
                    FirstName = patientData.FirstName,
                    LastName = patientData.LastName,
                    Email = patientData.Email,
                    PhoneNumber = patientData.PhoneNumber
                });

                sql.CommitTransaction();
            }
            catch(Exception)
            {
                sql.RollbackTransation();
                throw;
            }
        }


    }
}
