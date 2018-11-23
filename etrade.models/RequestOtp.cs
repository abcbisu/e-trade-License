using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etrade.models
{
    public class RequestOtp
    {
        public IdentityType IdentityType { get; set; }
        public string Identity { get; set; }
    }
}
