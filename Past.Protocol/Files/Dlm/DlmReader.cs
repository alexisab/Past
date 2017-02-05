using System;
using System.IO;
using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm
{
    public class DlmReader
    {
        public Map ReadDlm(string filePath)
        {
            Stream dlm = File.OpenRead(filePath);
            BigEndianReader reader = new BigEndianReader(dlm);
            int header = reader.ReadByte();
            if (header != 77)
            {
                reader.Seek(0, SeekOrigin.Begin);
                byte[] ba = null;
                try
                {
                    MemoryStream output = new MemoryStream();
                    Utils.Uncompress(dlm, output);
                    ba = output.ToArray();
                }
                catch
                {
                    throw new Exception("Wrong header and non-compressed.");
                }
                reader = new BigEndianReader(ba);
                header = reader.ReadByte();
                if (header != 77)
                {
                    throw new Exception("Wrong header file.");
                }
            }
            reader.Seek(0, SeekOrigin.Begin);
            Map map = new Map();
            map.FromRaw(reader);
            return map;
        }
    }
}
