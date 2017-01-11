using System;
using System.IO;
using System.Linq;

namespace Past.Tools
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (StreamWriter writer = new StreamWriter("mapsDatas"))
            {
                foreach (string file in Directory.GetFiles(@"C:\Users\skeee\Desktop\Dofus 2\content\maps", "*", SearchOption.AllDirectories).Where(x => x.Contains("dlm")))
                {
                    var map = new Dlm.DlmReader().ReadDLM(file);
                    bool outdoor = map.MapType == 1;
                    writer.WriteLine($"Maps.Add({map.Id}, new Map({map.Id}, {outdoor}, {map.SubareaId}, {map.TopNeighbourId}, {map.BottomNeighbourId}, {map.LeftNeighbourId}, {map.RightNeighbourId}));");
                }
                Console.WriteLine("Done ...");
                writer.Close();
            }
            while (true)
            {
                Console.Read();
            }
        }
    }
}
