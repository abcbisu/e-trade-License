using etrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Valuation_Board.Utils.EncryptionDecrytion;

namespace etrade.services.Models
{
    public class UserMin
    {
        public long UserId { get; set; }
        public UserType UserType { get; set; }
    }
    public class UserToken
    {
        //encrypted(userid:token)
        public string Token { get; private set; }
        public DateTime ExpiredOn { get; set; }

        private IEncryptDecrypt _encryptDecrypt;
        public long GetUserId() {
            return Convert.ToInt64(Dycript()[0]);
        }
        public string GetToken() {
            return Dycript()[1];
        }
        public void SetDecryptor(IEncryptDecrypt encryptDecrypt)
        {
            this._encryptDecrypt = encryptDecrypt;
        }
        public IEncryptDecrypt CryptographyRef()
        {
            return _encryptDecrypt;
        }
        public string SetToken(long userId,string token)
        {
            this.Token = _encryptDecrypt.Encrypt(string.Format("{0}:{1}", userId, token));
            return this.Token;
        }
        private string[] Dycript()
        {
            var x = _encryptDecrypt.Decrypt(this.Token).Split(':');
            return x;
        }
    }
}