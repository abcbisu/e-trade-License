
using etrade.services.Models;
using System;
using System.Linq;

namespace etrade.services
{
    public class UserService : ServiceBase
    {
        public UserToken GetNewAuthToken(string mobile, string password)
        {
            if (string.IsNullOrEmpty(mobile) || mobile.Trim().Length < 10)
            {
                throw new ArgumentException("Invalid mobile no");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Invalid password");
            }
            var user = _db.UserCredentials.FirstOrDefault(t => t.MobileNo.Contains(mobile));
            if (user == null)
            {
                throw new SecurityException("Mobile number not found");
            }
            if (string.Equals(password, user.Password))
            {
                throw new Exception("Invalid Password");
            }
            return new TokenServices().GenerateToken(user.UserId);
        }
    }
}
