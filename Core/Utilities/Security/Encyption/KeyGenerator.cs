using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encyption
{
    public static class KeyGenerator
    {
        public static byte[] GenerateHmacKey(string secretKey)
        {
            // Convert the secret key to bytes
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            // Use a Key Derivation Function to generate a key with the required size
            using (var sha512 = new SHA512Managed())
            {
                return sha512.ComputeHash(keyBytes);
            }
        }
    }
}
