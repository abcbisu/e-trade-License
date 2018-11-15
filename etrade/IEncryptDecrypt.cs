using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace etrade
{
   public interface IEncryptDecrypt
    {
        string Encrypt(string text);
        string Decrypt(string cipherText);
    }
}
