using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrescriptionDataManager.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedData { get; set; }


        public void ResetUserModel()
        {
            Token = "";
            Id = "";
            FirstName = "";
            LastName = "";
            Email = "";
            CreatedData = DateTime.MinValue;
        }
    }
}