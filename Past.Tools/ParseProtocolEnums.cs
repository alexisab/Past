using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Past.Tools
{
    public class ParseProtocolEnums
    {
        private static readonly Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();

        public static void ParseEnums(string @source)
        {
            foreach (string file in Directory.GetFiles(source, "*", SearchOption.AllDirectories).Where(x => x.Contains("as")))
            {
                string name = Path.GetFileName(file).Replace(".as", "");
                foreach (string line in File.ReadAllLines(file).Where(x => x.Contains("public static const")))
                {
                    string _enum = line.Replace("public static const", "").Replace(":uint", "").Replace(":int", "").Replace(";", ",").Trim();
                    if (!Data.ContainsKey(Path.GetFileName(file).Replace(".as", "")))
                        Data.Add(name, new List<string>());
                    Data[name].Add(_enum);
                }
            }
            foreach (KeyValuePair<string, List<string>> shit in Data)
            {
                using (StreamWriter writer = new StreamWriter(
                    $"{AppDomain.CurrentDomain.BaseDirectory}/Enums/{shit.Key + ".cs"}"))
                {
                    writer.WriteLine("namespace Past.Protocol.Enums");
                    writer.WriteLine("{");
                    writer.WriteLine("".PadLeft(4) + "public enum " + shit.Key);
                    writer.WriteLine("".PadLeft(4) + "{");
                    foreach (string shit2 in shit.Value)
                    {
                        writer.WriteLine("".PadLeft(8) + "{0}", shit2);

                    };
                    writer.WriteLine("".PadLeft(4) + "}");
                    writer.WriteLine("}");
                    writer.Close();
                }
            }
        }
    }
}
