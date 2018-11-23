using etrade.models;
using System;
using etrade.dal;
using System.Configuration;

namespace etrade.services
{
    public interface ITokenServices
    {
        UserToken GenerateToken();

        /// <summary>
        /// Function to validate token againt expiry and existance in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        UserMin ValidateTokenAndGetUser(string token);


        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId"></param>
        void Kill(string tokenId);


    }
    public class TokenServices : ITokenServices
    {
        long actorId;
        IEncryptDecrypt _crypt;
        public TokenServices(long actorId)
        {
            this.actorId = actorId;
            _crypt = new AESAlgorithm(ConfigurationManager.AppSettings["chiperKey"]);
        }
        #region Public member methods.
        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserToken GenerateToken()
        {
            using (Tokendal _td = new Tokendal(actorId))
            {
                string token = Guid.NewGuid().ToString();

                var tokendt = _td.GetToken();
                if (tokendt == null)
                {
                    _td.AddToken(token);
                }
                return new UserToken()
                {
                    Token = _crypt.Encrypt(string.Format("{0}:{1}", actorId, tokendt.Token)),
                    ExpiredOn = tokendt.ExpiredOn
                };
            }
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public UserMin ValidateTokenAndGetUser(string token)
        {
            if (token != null)
            {
                try
                {
                    var dToken = _crypt.Decrypt(token);
                    var arr = dToken.Split(':');
                    using (Tokendal _td = new Tokendal(Convert.ToInt64(arr[0])))
                    {
                        var user = _td.GetUserByValidatingToken(arr[1]);
                        if (user == null)
                        {
                            throw new AuthenticationRequiredException("Invalid Token Provided");
                        }
                        return user;
                    }
                }
                catch (FormatException)
                {
                    throw new AuthenticationRequiredException("Token Malfunctioned");
                }

            }
            throw new AuthenticationRequiredException("Invalid Token Provided");
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public void Kill(string token)
        {
            if (token != null)
            {
                try
                {
                    var dToken = _crypt.Decrypt(token);
                    var arr = dToken.Split(':');
                    using (Tokendal _td = new Tokendal(Convert.ToInt64(arr[0])))
                    {
                        _td.KillToken();
                    }
                }
                catch (FormatException)
                {
                    throw new AuthenticationRequiredException("Token Malfunctioned");
                }
            }
            throw new AuthenticationRequiredException("Invalid Token Provided");
        }
        public void Kill()
        {
                    using (Tokendal _td = new Tokendal(actorId))
                    {
                        _td.KillToken();
                    }
        }
        #endregion
    }
}
