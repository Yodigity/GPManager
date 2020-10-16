using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Helpers
{
    public class FormatPhoneNumberHelper
    {
        static public String FormatPhoneNumber(String phoneNumber)
        {
            String formattedPhoneNumber;
            if (!phoneNumber.StartsWith("+"))
            {
                formattedPhoneNumber = $"+44{phoneNumber.Substring(1)}";
            }
            else
            {
                formattedPhoneNumber = phoneNumber;
            }

            return formattedPhoneNumber;
        }
        
    }
}
