using Past.Protocol.IO;
using System;
using System.IO;
using ZLibNet;

namespace Past.Tools.Ele
{
    public class ElementsReader
    {

        public Elements ReadEle(string filePath)
        {
            Stream eleFile = File.OpenRead(filePath);
            BigEndianReader reader = new BigEndianReader(eleFile);
            int header = reader.ReadByte();
            if (header != 69)
            {
                reader.Seek(0, SeekOrigin.Begin);
                byte[] ba = null;
                try
                {
                    MemoryStream output = new MemoryStream();

                    Uncompress(eleFile, output);
                    ba = output.ToArray();
                }
                catch
                {
                    throw new Exception("Wrong header and non-compressed.");
                }
                reader = new BigEndianReader(ba);
                header = reader.ReadByte();
                if (header != 69)
                {
                    throw new Exception("Wrong header file.");
                }
            }
            reader.Seek(0, SeekOrigin.Begin);
            Elements ele = new Elements();
            ele.FromRaw(reader);
            return ele;
        }

        private void Uncompress(Stream input, Stream output)
        {
            ZLibStream outZStream = new ZLibStream(output, CompressionMode.Decompress);
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
