using Past.Protocol.Types;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Past.Common.Utils
{
    public class Functions
    {
        private static readonly Random Random = new Random();

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
                int num = Random.Next(0, 26);
                str += (char) ('a' + num);
            }
            return upper ? str.ToUpper() : str;
        }

        public static string RandomName()
        {
            string str = string.Empty;
            const string vowels = "aeiouy";
            const string consonants = "bcdfghjklmnpqrstvwxz";
            for (int i = 0; i <= Random.Next(5, 10); i++)
            {
                str += i%2 == 0
                    ? vowels[Random.Next(vowels.Length - 1)]
                    : consonants[Random.Next(consonants.Length - 1)];
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
            return (int) (date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds);
        }

        /// 0 = Continent Amaknien
        /// 1 = Debug
        /// 2 = Test
        /// 3 = Zone de départ
        public static int GetMapIdFromCoord(int worldId, int x, int y)
        {
            const int worldIdMax = 2 << 12;
            const int mapCoordMax = 2 << 8;
            if (x > mapCoordMax || y > mapCoordMax || worldId > worldIdMax)
            {
                return -1;
            }
            int newWorldId = worldId & 4095;
            int newX = Math.Abs(x) & 255;
            if (x < 0)
            {
                newX = newX | 256;
            }
            int newY = Math.Abs(y) & 255;
            if (y < 0)
            {
                newY = newY | 256;
            }
            return newWorldId << 18 | (newX << 9 | newY);
        }

        public static EntityLook BuildEntityLook(string entityLook)
        {
            string[] lookStringSplit = entityLook.Replace("{", "").Replace("}", "").Split('|');
            short bonesId = short.Parse(lookStringSplit[0]);
            short[] skins;
            if (lookStringSplit[1].Contains(","))
            {
                string[] skinsString = lookStringSplit[1].Split(',');
                skins = new short[skinsString.Length];
                for (int i = 0; i < skinsString.Length; i++)
                {
                    skins[i] = short.Parse(skinsString[i]);
                }
            }
            else
            {
                skins = string.IsNullOrEmpty(lookStringSplit[1])
                    ? new short[0]
                    : new short[] {short.Parse(lookStringSplit[1])};
            }
            int[] colors;
            if (lookStringSplit[2].Contains(","))
            {
                string[] colorsString = lookStringSplit[2].Split(',');
                colors = new int[colorsString.Length];
                for (int i = 0; i < colorsString.Length; i++)
                {
                    int color = int.Parse(colorsString[i].Remove(0, 2));
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
            short[] size = new short[] {short.Parse(lookStringSplit[3])};
            SubEntity[] subEntity;
            if (lookStringSplit.Length > 4) //if contains subEntity
            {
                string subEntitiesString = entityLook.Substring(entityLook.IndexOf('@') - 1);
                if (subEntitiesString.Count(@char => @char == '@') > 1) //more than one subEntity
                {
                    string[] subEntityString = subEntitiesString.Split(new string[] {"},"},
                        StringSplitOptions.RemoveEmptyEntries);
                    subEntity = new SubEntity[subEntityString.Length];
                    for (int i = 0; i < subEntityString.Length; i++)
                    {
                        string[] strings = subEntityString[i].Replace("{", "").Replace("}", "").Split('|');
                        short subEntityBonesId = short.Parse(strings[0].Substring((4)));
                        short[] subEntitySkins;
                        if (strings[1].Contains(","))
                        {
                            string[] subEntitySkinString = strings[1].Split(',');
                            subEntitySkins = new short[subEntitySkinString.Length];
                            for (int j = 0; j < subEntitySkinString.Length; j++)
                            {
                                subEntitySkins[j] = short.Parse(subEntitySkinString[j]);
                            }
                        }
                        else
                        {
                            subEntitySkins = string.IsNullOrEmpty(strings[1])
                                ? new short[0]
                                : new short[] {short.Parse(strings[1])};
                        }
                        int[] subEntityColors;
                        if (strings[2].Contains(","))
                        {
                            string[] subEntityColorsString = strings[2].Split(',');
                            subEntityColors = new int[subEntityColorsString.Length];
                            for (int k = 0; k < subEntityColorsString.Length; k++)
                            {
                                int color = int.Parse(subEntityColorsString[k].Remove(0, 2));
                                if (color != -1)
                                {
                                    subEntityColors[k] = (k + 1 & 255) << 24 | color & 16777215;
                                }
                            }
                        }
                        else
                        {
                            subEntityColors = new int[0];
                        }
                        short[] subEntitySize = new short[] {short.Parse(strings[3])};
                        subEntity[i] = new SubEntity(sbyte.Parse(strings[0][0].ToString()),
                            sbyte.Parse(strings[0][2].ToString()),
                            new EntityLook(subEntityBonesId, subEntitySkins, subEntityColors,
                                subEntitySize, new SubEntity[0]));
                    }
                }
                else //one subEntity
                {
                    string[] oneSubEntityString = subEntitiesString.Replace("{", "").Replace("}", "").Split('|');
                    short[] oneSubEntitySkins;
                    if (oneSubEntityString[1].Contains(","))
                    {
                        string[] oneSubEntitySkinString = oneSubEntityString[1].Split(',');
                        oneSubEntitySkins = new short[oneSubEntitySkinString.Length];
                        for (int i = 0; i < oneSubEntitySkinString.Length; i++)
                        {
                            oneSubEntitySkins[i] = short.Parse(oneSubEntitySkinString[i]);
                        }
                    }
                    else
                    {
                        oneSubEntitySkins = string.IsNullOrEmpty(oneSubEntityString[1])
                            ? new short[0]
                            : new short[] { short.Parse(oneSubEntityString[1]) };
                    }
                    int[] oneSubEntityColors;
                    if (oneSubEntityString[2].Contains(","))
                    {
                        string[] oneSubEntityColorsString = oneSubEntityString[2].Split(',');
                        oneSubEntityColors = new int[oneSubEntityColorsString.Length];
                        for (int j = 0; j < oneSubEntityColorsString.Length; j++)
                        {
                            int color = int.Parse(oneSubEntityColorsString[j].Remove(0, 2));
                            if (color != -1)
                            {
                                oneSubEntityColors[j] = (j + 1 & 255) << 24 | color & 16777215;
                            }
                        }
                    }
                    else
                    {
                        oneSubEntityColors = new int[0];
                    }
                    short[] oneSubEntitySize = new short[] { short.Parse(oneSubEntityString[3]) };
                    subEntity = new SubEntity[]
                    {
                        new SubEntity(sbyte.Parse(oneSubEntityString[0][0].ToString()),
                            sbyte.Parse(oneSubEntityString[0][2].ToString()),
                            new EntityLook( short.Parse(oneSubEntityString[0].Substring((4))), oneSubEntitySkins, oneSubEntityColors,
                                oneSubEntitySize, new SubEntity[0]))
                    };
                }
            }
            else //no subEntity
            {
                subEntity = new SubEntity[0];
            }
            return new EntityLook(bonesId, skins, colors, size, subEntity);
        }
    }
}