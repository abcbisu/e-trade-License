using etrade.models;

namespace etrade.models
{
    public class Login:Identity
    {
        /// <summary>
        /// Intermediate secured code
        /// </summary>
        public string IMCode { get; set; }
        public string Otp { get; set; }
        //public int LoginType { get; set; }
    }
}