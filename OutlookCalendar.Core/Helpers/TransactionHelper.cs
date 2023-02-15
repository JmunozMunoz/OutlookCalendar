using System;
using System.Security.Cryptography;

namespace OutlookCalendar.Domain.Core.Helpers
{
    public class TransactionHelper
    {
        public static int GenerateUniqueRandomNumber()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                Int32 randomInteger = 0;
                var byteArray = new byte[9];

                while (randomInteger == 0)
                {
                    provider.GetBytes(byteArray);
                    randomInteger = BitConverter.ToInt32(byteArray, 0);
                }

                return randomInteger < 0 ? randomInteger : -randomInteger;
            }
        }

        public static string GetStatusCode(int statusCode)
        {
            return string.Format("{0:D2}", statusCode);
        }
    }
}
