
using etrade.services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etrade.services
{
    public interface ITokenServices
    {
        UserToken GenerateToken(long userId);

        /// <summary>
        /// Function to validate token againt expiry and existance in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        UserMin ValidateToken(string tokenId, long userId);


        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId"></param>
        void Kill(string tokenId, long userId);


    }
    public class TokenServices: ITokenServices
    {
        #region Public member methods.
        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserToken GenerateToken(long userId)
        {
            //string token = Guid.NewGuid().ToString();
            //DateTime curDateTime = DateTime.Now;
            ////expire previous token
            //var user = _db.UserTokens.FirstOrDefault(t => t.UserId == userId);
            //user = (user == null ? _db.UserTokens.Add(new Database.Entities.UserToken()
            //{
            //    ExpiredOn = curDateTime.AddYears(1),
            //    IssuedOn = curDateTime,
            //    Token = token
            //}) : user);

            //user.Token = token;
            //user.IssuedOn = curDateTime;
            //user.ExpiredOn = curDateTime.AddYears(1);

            ////add new token
            //_db.SaveChanges();
            //return new UserToken()
            //{
            //    Token = user.Token,
            //    UserId = user.UserId,
            //    ExpiredOn = user.ExpiredOn
            //};
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public UserMin ValidateToken(string tokenId, long userId)
        {
            //var token = _db.UserTokens.FirstOrDefault(t => t.UserId == userId && t.Token == tokenId && t.ExpiredOn >= DateTime.Now);
            //if (token != null)
            //{
            //    var user = _db.Users.FirstOrDefault(t => t.UserId == token.UserId);
            //    return new UserMin()
            //    {
            //        UserId = user.UserId,
            //        UserType = user.UserType
            //    };
            //}
            //return null;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public void Kill(string tokenId, long userId)
        {
            //var token = _db.UserTokens.FirstOrDefault(t => t.UserId == userId && t.Token == tokenId);
            //if (token != null)
            //{
            //    _db.UserTokens.Remove(token);
            //}

            throw new NotImplementedException();
        }
        public void Kill(long userId)
        {
            //var curDtTime = DateTime.Now;
            //_db.UserTokens.Where(t => t.UserId == userId && t.ExpiredOn >= curDtTime).ToList().ForEach(f =>
            //{
            //    f.ExpiredOn = curDtTime.AddSeconds(-1);
            //});
            //_db.SaveChanges();
            throw new NotImplementedException();
        }
        #endregion
    }
}
