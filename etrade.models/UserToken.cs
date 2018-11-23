using System;

namespace etrade.models
{
    public class UserToken
    {
        //encrypted(userid:token)
        public string Token { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}