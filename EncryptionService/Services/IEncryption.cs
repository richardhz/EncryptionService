using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncryptionService.Services
{
    public interface IEncryption
    {
        string Encrypt(string dataToEncrypt, string keyToken, string ivToken = null);
        string Decrypt(string dataToDecrypt, string keyToken, string ivToken = null);
    }
}
