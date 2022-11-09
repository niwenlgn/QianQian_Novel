using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.MyUtility
{
    public static class UserEncodeUtil
    {
        private static string machineKey = null!;

        public static void Init(string _machineKey)
        {
            machineKey = _machineKey;
        }

        public static string EncodePassword(this string password)
        {
            HMACSHA1 hash = new()
            {
                Key = HexToByte(machineKey)
            };
            string encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
            return encodedPassword;
        }

        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    }
}
