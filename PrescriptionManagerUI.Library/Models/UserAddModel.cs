using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.Models
{
    public class UserAddModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set;  }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
