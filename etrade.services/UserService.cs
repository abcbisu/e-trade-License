
using etrade.dal;
using etrade.models;
using System.Linq;
using System.Security;
using System.Data;
using etrade.Dal;

namespace etrade.services
{
    public class UserService : ServiceBase
    {
        long UserId;
        public UserService(long UserId)
        {
            this.UserId = UserId;
        }
        public UserToken GetNewAuthToken(string identity, IdentityType idType, string otp)
        {
            var _otps = new OtpServer(UserId);
            var _ud = new Userdal(UserId);
            if (idType == IdentityType.email)
                etrade.Validate.Email(identity);
            else if (idType == IdentityType.mobile)
                etrade.Validate.Mobile(identity);
            
            //implementation required
            var user = _otps.GetUserValidatingOtp(identity, idType, otp);
            _ud.ValidateUserAccLockedStatus(user.UserId);
            var tokenSrv = new TokenServices(user.UserId);
           return tokenSrv.GenerateToken();
        }
        
        public T validateUserCredential<T>(IAuthentication<T> authentication)
        {
            var user= authentication.Authenticate();
            if (user == null)
            {
                throw new AuthenticationRequiredException("Invalid Credential Provided");
            }
            return user;
        }
        public bool RecoverPassword(string Idntity, IdentityType IdType,string Password)
        {
            using (var _rcPass = new Userdal(UserId))
            {
                var successFlag = _rcPass.RecoverUserPassword(Idntity, IdType, Password);
                return successFlag;
            }
        }
        public UserMin GetUserByIdentity(string Identifire, IdentityType? IdType)
        {
            using (var _rcPass = new Userdal(UserId))
            {
                var obj = _rcPass.GetUserByIdentity(Identifire, IdType);
                if (obj == null)
                    throw new System.NullReferenceException("User not found");
                return obj;
            }
        }
    }
}
