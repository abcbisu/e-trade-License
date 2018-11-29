using System;
using System.Configuration;
using System.Data;
using etrade.dal;
using etrade;
using etrade.entities;
using etrade.models;

namespace etrade.Dal
{
    public class OtpDal: DalBase
    {
        public OtpDal(long? userId) : base(userId){}
        public etrade.entities.OtpResponse RequerstOTP(string identity,IdentityType IdType, string Flag) //IdentyType may be Email or Mobile, Flag=lgn for Login, Flag=RecvPass for Recover Password
        {
            int OtpExpireAfterSeconds = 0;
            int otpNextSendAfterSeconds = 0;
            OtpExpireAfterSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["OtpExpireAfterSeconds"]);
            otpNextSendAfterSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["otpNextSendAfterSeconds"]);
            var cmd = NewCommand("etrade.Insert_Otp");

            //if (Flag== "lgn" || Flag== "RecvPass")
            //{
                cmd.Parameters.AddWithValue("@identifire", identity);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@userId", _actorId);
            //}
            cmd.Parameters.AddWithValue("@identityType", IdType);
            cmd.Parameters.AddWithValue("@expireAfterSeconds", OtpExpireAfterSeconds);
            cmd.Parameters.AddWithValue("@nextSendAfterSeconds", otpNextSendAfterSeconds);
            cmd.Parameters.AddWithValue("@otpVal", new Random().Next(10000000, 99999999));//8 digit random number
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var res = GetResult(cmd).Convert<etrade.entities.OtpResponse>();
            if (res.Count > 0)
                return res[0];
            return null;
        }
        //sample1
        public void Verify(string otpValue, IdentityType IdType, string identity) //IdentyType may be Email or Mobile
        {
                var cmd = NewCommand("etrade.get_verify_OtpOnServer");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@otpVal", otpValue.ReplaceDbNull());
                cmd.Parameters.AddWithValue("@identity", identity.ReplaceDbNull());
                cmd.Parameters.AddWithValue("@IdentyType", IdType);
                ExecuteNonQuery(cmd);
        }
       
    }
}
