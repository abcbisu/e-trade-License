using System;
using etrade;
using etrade.dal;
using etrade.models;

namespace etrade
{
    public class AuthenticatorBase<T>  : IAuthentication<T> where T : UserMin
    {
        string Idntity; string Password; IdentityType IdType;
        public AuthenticatorBase(string Idntity, string Password, IdentityType IdType)
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