using System;

namespace etrade.entities
{
    public class OtpResponse
    {
        public OtpResponse() { }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? NextOtpSendOn { get; set; }
    }
}