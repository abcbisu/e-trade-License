using etrade.models;
using System;
using etrade.Dal;
using System.Data.SqlClient;
using etrade.dal;

namespace etrade.services
{
    public class OtpServer
    {
        long? userId;
        public OtpServer(long? userId)
        {
            this.userId = userId;
        }
        public etrade.models.OtpResponse RequerstOTP(string identity, IdentityType IdType, string forWhatItsCalled) // lgn=Login, RecvPass=Recover Password
        {
            using (var _otp = new OtpDal(userId))
            {
                var otpDT = _otp.RequerstOTP(identity,IdType, forWhatItsCalled);
                if(otpDT==null)
                    throw new InvalidProgramException("OTP can not be send, unknown error");
                return new OtpResponse() {
                    ExpiredOn=otpDT.ExpiredOn,
                    NextOtpSendOn=otpDT.NextOtpSendOn
                };
            }
            
        }
        public void VerifyOtp(string Idntity, IdentityType IdType, string Otp)
        {
            try
            {
                using (var otpDal = new OtpDal(userId))
                {
                    otpDal.Verify(Otp, IdType, Idntity);
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOTPorIdentity(ex.Message);
            }
        }
        public UserMin GetUserValidatingOtp(string identity,IdentityType identityType, string otp)
        {
            using (var otpDal=new OtpDal(userId))
            {
               otpDal.Verify(otp, identityType, identity);
                using (Userdal dal = new Userdal(userId))
                {
                  return  dal.GetUserByIdentity(identity, null);
                }
            }
            
        }
        
    }
}
