using System.Globalization;
using System.Security.Cryptography;

namespace OutlookCalendar.Domain.Core.Helpers
{
    public static class CommonHelpers
    {
        public static string GetUniqueToken(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_")
        {
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                // If chars.Length isn't a power of 2 then there is a bias if we simply use the modulus operator. The first characters of chars will be more probable than the last ones.
                // buffer used if we encounter an unusable random byte. We will regenerate it in this buffer
                byte[] buffer = null;
                // Maximum random number that can be used without introducing a bias
                int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);
                crypto.GetBytes(data);
                char[] result = new char[length];
                for (int i = 0; i < length; i++)
                {
                    byte value = data[i];
                    while (value > maxRandom)
                    {
                        if (buffer == null)
                        {
                            buffer = new byte[1];
                        }
                        crypto.GetBytes(buffer);
                        value = buffer[0];
                    }
                    result[i] = chars[value % chars.Length];
                }
                return new string(result);
            }
        }

        public static (string NumberPart, string DecimalPart) ToSeparateDecimalForm(this decimal @value)
        {
            var valueString = value.ToString("#.##", CultureInfo.InvariantCulture);
            var splitValues = valueString.Split('.');
            if (splitValues.Length == 1)
                return (splitValues[0], "00");
            if (splitValues.Length == 0)
                return ("0", "00");
            return (splitValues[0], splitValues[1]);
        }

        public static string PadRightWithNullCheck(this string str, int totalWith, char paddingChar)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty.PadRight(totalWith, paddingChar);
            return str.PadRight(totalWith, paddingChar);
        }

        public static string PadLeftWithNullCheck(this string str, int totalWith, char paddingChar)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty.PadLeft(totalWith, paddingChar);
            return str.PadLeft(totalWith, paddingChar);
        }

        public static string PadOrCropLeft(this string str, int totalLength, char paddingChar)
        {
            if (str == null)
                return string.Empty.PadLeftWithNullCheck(totalLength, paddingChar);
            if (str.Length > totalLength)
            {
                return str.Substring(str.Length - totalLength);
            }
            return str.PadLeftWithNullCheck(totalLength, paddingChar);
        }

        public static string PadOrCropLeft(this int number, int totalLength, char paddingChar)
        {
            var stringNumber = number.ToString();
            if (stringNumber.Length > totalLength)
            {
                return stringNumber.Substring(stringNumber.Length - totalLength);
            }
            return stringNumber.PadLeftWithNullCheck(totalLength, paddingChar);
        }

        public static string PadOrCropRight(this string str, int totalLength, char paddingChar)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty.PadRightWithNullCheck(totalLength, paddingChar);
            if (str.Length > totalLength)
            {
                return str.Substring(0, totalLength);
            }
            return str.PadRightWithNullCheck(totalLength, paddingChar);
        }
    }
}
