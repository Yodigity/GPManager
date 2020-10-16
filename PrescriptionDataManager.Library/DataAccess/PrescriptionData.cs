using PrescriptionDataManager.Library.Internal.DataAccess;
using PrescriptionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionDataManager.Library.DataAccess
{
    public class PrescriptionData
    {
        public void AddPrescription(PrescriptionModel prescription, string presciberId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            try
            {

                sql.StartTransaction("PrescriptionDataManagerUpdated");

                sql.SaveDataInTransaction("dbo.spPrescription_Add", new
                {
                    PatientId = prescription.PatientID.ToString(),
                    DrugName = prescription.DrugName,
                    Dosage = prescription.Dosage,
                    PrescriberId = presciberId,
                    RenewalDate = prescription.RenewalDate
                });

                sql.CommitTransaction();
            }
            catch (Exception)
            {
                sql.RollbackTransation();
                throw;

            }

            /*      var output = sql.SaveData<PrescriptionModel>("dbo.spPrescription_Add", new { PatientId = prescription.PatientID.ToString(), DrugName = prescription.DrugName, Dosage = prescription.Dosage,
                  PrescriberId = prescription.PrescriberId, RenewalDate = prescription.RenewalDate}, "PrescriptionDataMangerUpdated");*/
        }

        public void EditPrescription(PrescriptionModel prescription, string presciberId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            try
            {

                sql.StartTransaction("PrescriptionDataManagerUpdated");

                sql.SaveDataInTransaction("dbo.spPrescription_Edit", new
                {
                    PrescriptionId = prescription.ID,
                    Dosage = prescription.Dosage,
                    PrescriberId = presciberId,
                    RenewalDate = prescription.RenewalDate
                });

                sql.CommitTransaction();
            }
            catch (Exception)
            {
                sql.RollbackTransation();
                throw;

            }
        }

        }
}
