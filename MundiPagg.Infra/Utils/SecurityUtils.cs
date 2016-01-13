using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Infra.Utils
{
    public class SecurityUtils
    {
        
        public static String MakePassword(int lenght)
        {
            return System.Web.Security.Membership.GeneratePassword(lenght, 1);
        }

        public static String HashSHA1(String plainText)
        {
            return GetSHA1HashData(plainText);
        }

        public static bool ValidateSHA1HashData(string inputData, string storedHashData)
        {
            string getHashInputData = GetSHA1HashData(inputData);

            if (string.Compare(getHashInputData, storedHashData) == 0)
                return true;
            else
                return false;
        }
        
        public static String HashHMAC256(String plainText, string secretKey)
        {
            return GetHMACSHA256HashData(plainText, secretKey);
        }
        
        public static string GenerateUniqueKey()
        {
            var cryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] data = new byte[128];
            cryptoServiceProvider.GetBytes(data);
            return Convert.ToBase64String(data);
        }

        public static bool ValidateHMAC256HashData(string inputData, string signature, string secretKey)
        {
            string getHashInputData = GetHMACSHA256HashData(inputData, secretKey);

            if (string.Compare(getHashInputData, signature) == 0)
                return true;
            else
                return false;
        }

        public static string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }

        public static bool ValidateToken(string token)
        {
            try
            {
                byte[] data = Convert.FromBase64String(token);
                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                if (when < DateTime.UtcNow.AddHours(-24))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }


        }
              

        #region Private Methods
        private static string GetSHA1HashData(string data)
        {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] byteV = System.Text.Encoding.UTF8.GetBytes(data);
            byte[] byteH = SHA1.ComputeHash(byteV);

            SHA1.Clear();

            return Convert.ToBase64String(byteH);
        }

       
        private static string GetHMACSHA256HashData(string plainText,string secretKey)
        {
            var byteKey = Encoding.UTF8.GetBytes(secretKey);
            string hashString;

            using (var hmac = new HMACSHA256(byteKey))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }

     
        #endregion




    }
}
