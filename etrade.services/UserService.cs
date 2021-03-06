﻿
using etrade.dal;
using etrade.models;
using System;

namespace etrade.services
{
    public class UserService : ServiceBase
    {
        long? UserId;
        public UserService(long? UserId)
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

        public T ValidateUserCredential<T>(IAuthentication<T> authentication)
        {
            var user = authentication.Authenticate();
            if (user == null)
            {
                throw new AuthenticationRequiredException("Invalid Credential Provided");
            }

            return user;
        }
        public object TryToLogin(Credenetial credenetial)
        {
            using (var usr = new Userdal(UserId))
            {
                var resp = usr.GetLoginBehaviourValidatingUser(credenetial.Idntity, credenetial.Password, credenetial.IdType);
                if (resp == null)
                {
                    throw new AuthenticationRequiredException("Invalid Credential Provided");
                }
                if (resp.HasPendingCommand)
                {
                    if (string.Equals(resp.PendingCommnad, "vrf-otp", StringComparison.OrdinalIgnoreCase))
                    {
                        var _otpDrv = new OtpServer(Convert.ToInt64(UserId));
                        var _otp = _otpDrv.RequerstOTP(credenetial.Idntity, credenetial.IdType, "lgn");
                        return new RspResult<OtpResponse>()
                        {
                            CommanDescription = resp.CommanDescription,
                            Exec = resp.Exec,
                            HasPendingCommand = resp.HasPendingCommand,
                            Params = resp.Params,
                            PendingCommnad = resp.PendingCommnad,
                            Result = _otp
                        };
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("Invalid command or configuration not supproted"));
                    }
                }
                else
                {
                    //this never will executed till database side not written
                    //login complete
                    var token = new TokenServices(resp.Result.UserId).GenerateToken();
                    return new RspResult<UserToken>()
                    {
                        CommanDescription = resp.CommanDescription,
                        Exec = resp.Exec,
                        HasPendingCommand = resp.HasPendingCommand,
                        Params = resp.Params,
                        PendingCommnad = resp.PendingCommnad,
                        Result = token
                    };
                }
            }
        }
        public bool RecoverPassword(string Idntity, IdentityType IdType, string Password)
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
