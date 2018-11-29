using System;
using System.Collections.Generic;

namespace etrade.models
{
    public class OtpResponse
    {
        public OtpResponse() { }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? NextOtpSendOn { get; set; }
    }
    public class RspResult<T>
    {
        public bool Exec { get; set; }
        public bool HasPendingCommand { get; set; }
        public string PendingCommnad { get; set; }
        public string CommanDescription { get; set; }
        public T Result { get; set; }
        public Dictionary<string, object> Params {get;set;}
        public RspResult()
        {
            Params = new Dictionary<string, object>();
        }
    }
}