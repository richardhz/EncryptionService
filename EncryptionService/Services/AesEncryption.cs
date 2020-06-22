using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionService.Services
{
    public class AesEncryption : IEncryption
    {
        public string Decrypt(string dataToDecrypt, string keyToken, string ivToken = null)
        {
            byte[] iv = new byte[16]; 
            var dataBytes = Convert.FromBase64String(dataToDecrypt);
            var key = Convert.FromBase64String(keyToken);

            if (ivToken != null)
            {
                iv = Convert.FromBase64String(ivToken);
            }

            var result = AesDecrypt(dataBytes, key, iv);
            return Encoding.UTF8.GetString(result);
            throw new NotImplementedException();
        }

        public string Encrypt(string dataToEncrypt, string keyToken, string ivToken = null)
        {
            byte[] iv = new byte[16];
            var dataBytes = Encoding.UTF8.GetBytes(dataToEncrypt);
            var key = Convert.FromBase64String(keyToken);

            if (ivToken != null)
            {
                iv = Convert.FromBase64String(ivToken);
            }

            var result = AesEncrypt(dataBytes, key, iv);
            return Convert.ToBase64String(result);
        }


        private byte[] AesEncrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        private byte[] AesDecrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    var decryptBytes = memoryStream.ToArray();

                    return decryptBytes;
                }
            }
        }

    }
}
