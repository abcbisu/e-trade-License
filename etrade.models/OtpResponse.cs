using System;

namespace etrade.models
{
    public class OtpResponse
    {
        public OtpResponse() { }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? NextOtpSendOn { get; set; }
    }
}