using System;
using etrade;
using etrade.dal;
using etrade.models;

namespace etrade_server.App_Codes.Authenticator
{
    public class GeneralUserAuthenticator<T>  : IAuthentication<T> where T : UserMin
    {
        string Idntity; string Password; IdentityType IdType;
        public GeneralUserAuthenticator(string Idntity, string Password, IdentityType IdType)
        {
            this.IdType = IdType;
            this.Password = Password;
            this.Idntity = Idntity;

        }
        public T Authenticate()
        {
            using (var _ud = new Userdal(1))
            {
                return (T)_ud.GetUserValidatingCredential(Idntity, Password, IdType);
            }
                
        }
    }
}