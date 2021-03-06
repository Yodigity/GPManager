﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrescriptionManagerUI.Library.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedData { get; set; }

        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();


        public string RoleList
        {
            get { return string.Join(", ", Roles.Select(x => x.Value)); }

        }
    }
}