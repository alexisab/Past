using System.IO;
using zlib;

namespace Past.Protocol.Files
{
    public class Utils
    {
        public static void Uncompress(Stream input, Stream output)
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
