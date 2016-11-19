using Past.Common.Database.Record;
using Past.Protocol.Types;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Past.Common.Utils
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
            {
                return str.ToUpper();
            }
            return str;
        }

        public static string RandomName()
        {
            string str = string.Empty;
            string vowels = "aeiouy";
            string consonants = "bcdfghjklmnpqrstvwxz";
            for (int i = 0; i <= random.Next(5, 10); i++)
            {
                str += i % 2 == 0 ? vowels[random.Next(vowels.Length - 1)] : consonants[random.Next(consonants.Length - 1)];
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ReturnMd5Hash(string input)
        {
            byte[] data = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string CipherPassword(string hashedPassword, string ticket)
        {
            return ReturnMd5Hash(hashedPassword + ticket);
        }

        public static string CipherSecretAnswer(int characterId, string answer)
        {
            return ReturnMd5Hash(characterId + "~" + answer);
        }

        public static int ReturnUnixTimeStamp(DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds);
        }

        public static EntityLook BuildEntityLook(string entityLook) //TODO SubEntity
        {
            string[] look_string = entityLook.Replace("{", "").Replace("}", "").Split('|');
            short bonesId = short.Parse(look_string[0]);
            short[] skins;
            if (look_string[1].Contains(","))
            {
                string[] skins_string = look_string[1].Split(',');
                skins = new short[skins_string.Length];
                for (int i = 0; i < skins_string.Length; i++)
                {
                    skins[i] = short.Parse(skins_string[i]);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(look_string[1]))
                {
                    skins = new short[0];
                }
                else
                {
                    skins = new short[] { short.Parse(look_string[1]) };
                }
            }
            int[] colors;
            if (look_string[2].Contains(","))
            {
                string[] colors_string = look_string[2].Split(',');
                colors = new int[colors_string.Length];
                for (int i = 0; i < colors_string.Length; i++)
                {
                    int color = int.Parse(colors_string[i].Remove(0, 2));
                    if (color != -1)
                    {
                        colors[i] = (i + 1 & 255) << 24 | color & 16777215;
                    }
                }
            }
            else
            {
                colors = new int[0];
            }
            short[] size = new short[] { short.Parse(look_string[3]) };
            return new EntityLook(bonesId, skins, colors, size, new SubEntity[0]);
        }
    }
}
