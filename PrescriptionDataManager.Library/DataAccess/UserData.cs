using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrescriptionDataManager.Library.Internal.DataAccess;
using PrescriptionDataManager.Library.Models;

namespace PrescriptionDataManager.Library.DataAccess
{
    public class UserData
    {

        /* private readonly ISqlDataAccess _sql;

         public UserData(IConfiguration config, ISqlDataAccess sql)
         {

             _sql = sql;
         }*/
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            //var output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id }, "AJRMData");
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUser_Lookup", new { Id }, "PrescriptionDataManagerUpdated");
            return output;
        }

        public void RegisterUserDetails(UserAddModel userDetails)
        {
            SqlDataAccess sql = new SqlDataAccess();

            try
            {

                sql.StartTransaction("PrescriptionDataManagerUpdated");

                sql.SaveDataInTransaction("dbo.spUser_Add", new
                {
                    id = userDetails.Id,
                    firstName = userDetails.FirstName,
                    lastName = userDetails.LastName,
                    email = userDetails.Email
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
