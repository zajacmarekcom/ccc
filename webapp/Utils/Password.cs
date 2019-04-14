using System;
using System.Security.Cryptography;
using System.Text;

namespace webapp.Utils
{
    public class Password
    {
        public static string EncodePassword(string pass)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] dst = new byte[bytes.Length];
            Buffer.BlockCopy(bytes, 0, dst, 0, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
    }
}