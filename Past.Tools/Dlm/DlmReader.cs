using Past.Protocol.IO;
using System;
using System.IO;
using zlib;

namespace Past.Tools.Dlm
{
    public class DlmReader
    {
        public Map ReadDLM(string filePath)
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
                    Uncompress(dlm, output);
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

        private void Uncompress(Stream input, Stream output)
        {
            ZOutputStream outZStream = new ZOutputStream(output);
            try
            {
                byte[] raw = new byte[(int)input.Length];
                for (int i = 0; i < (int)input.Length; i++)
                {
                    raw[i] = (byte)input.ReadByte();
                }
                outZStream.Write(raw, 0, raw.Length);
                output.Flush();
            }
            finally
            {
                outZStream.Close();
            }
        }
    }
}


