using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace etrade
{
   public static class Validate
    {
        public static void Email(string email)
        {
            if(!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                throw new Exception("Invalid Email");
            }
        }
        public static void Mobile(string number)
        {
            if (!Regex.Match(number, @"^(\+[0-9]{9})$").Success)
            {
                throw new Exception("Invalid Mobile No");
            }
        }
    }
}
