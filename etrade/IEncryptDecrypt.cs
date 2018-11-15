using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valuation_Board.Utils.EncryptionDecrytion
{
   public interface IEncryptDecrypt
    {
        string Encrypt(string text);
        string Decrypt(string cipherText);
    }
}
