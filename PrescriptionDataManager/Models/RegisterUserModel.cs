using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrescriptionDataManager.Models
{
    public class RegisterUserModel
    {
        public int ID { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedData { get; set; }
    }
}