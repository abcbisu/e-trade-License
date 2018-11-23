using System;

namespace etrade.entities
{
    public class UserToken
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
