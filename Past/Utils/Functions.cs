using System;
using System.Security.Cryptography;
using System.Text;

namespace Past.Utils
{
    public class Functions
    {
        private static Random random = new Random();

        public static string ByteArrayToString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", " ");
        }

        public static string ByteArrayToString(byte[] data, int length)
        {
            return BitConverter.ToString(data, 0, length).Replace("-", " ");
        }

        public static string RandomString(int lenght, bool upper)
        {
            string str = string.Empty;
            for (int i = 1; i <= lenght; i++)
            {
                int num = random.Next(0, 26);
                str += (char)('a' + num);
            }
            if (upper)
                return str.ToUpper();
            return str;
        }

        public static string GetMd5Hash(string input)
        {
            byte[] data = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string CipherString(string hashedPassword, string ticket)
        {
            return GetMd5Hash(hashedPassword + ticket);
        }
    }
}
