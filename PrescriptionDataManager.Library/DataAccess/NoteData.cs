using PrescriptionDataManager.Library.Internal.DataAccess;
using PrescriptionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionDataManager.Library.DataAccess
{
    public class NoteData
    {
        public void AddNote(PatientNoteModel note)
        {
            SqlDataAccess sql = new SqlDataAccess();

            

            try
            {
                sql.StartTransaction("PrescriptionDataManagerUpdated");

                sql.SaveDataInTransaction("dbo.spNote_Add",
                    new
                    {
                        note.PatientId,
                        note.Title,
                        note.Text,
                        note.AuthorId

                    });
                sql.CommitTransaction();
            }
            catch
            {
             
                sql.RollbackTransation();
                throw;
            
        }
        }
    }
}
