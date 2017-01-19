using System.Collections.Generic;
using Past.Protocol.Types;

namespace Past.Common.Data
{
    public class Interactive
    {
        public static Dictionary<int, InteractiveElement[]> Interactives = new Dictionary<int, InteractiveElement[]>();

        public static void Initialize() //Only zaaps & zaapis for the moment
        {
            Interactives.Add(2323, new[] { new InteractiveElement(433973, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(139265, new[] { new InteractiveElement(433974, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(21761028, new[] { new InteractiveElement(433975, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(21760512, new[] { new InteractiveElement(435662, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(16778242, new[] { new InteractiveElement(437119, new short[] { 44, 114 }, new short[0]) }); //duty free
            Interactives.Add(154642, new[] { new InteractiveElement(443686, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(800, new[] { new InteractiveElement(57531, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(138543, new[] { new InteractiveElement(57534, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(147768, new[] { new InteractiveElement(260872, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(141588, new[] { new InteractiveElement(437118, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(143372, new[] { new InteractiveElement(437117, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(144419, new[] { new InteractiveElement(58211, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(133896, new[] { new InteractiveElement(443366, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(17932, new[] { new InteractiveElement(57573, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(13060, new[] { new InteractiveElement(1254, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(12054, new[] { new InteractiveElement(149940, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(8991, new[] { new InteractiveElement(1246, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(13605, new[] { new InteractiveElement(1261, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(15654, new[] { new InteractiveElement(60101, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(15153, new[] { new InteractiveElement(159227, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(14419207, new[] { new InteractiveElement(52836, new short[] { 44, 114 }, new short[0]) });
            //Interactives.Add(20973313, new[] { new InteractiveElement(437121, new short[] { 44, 114 }, new short[0]) }); bug idk why
            Interactives.Add(28050436, new[] { new InteractiveElement(435664, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(131608, new[] { new InteractiveElement(57527, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(5142, new[] { new InteractiveElement(57554, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(2567, new[] { new InteractiveElement(57272, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(148744, new[] { new InteractiveElement(406477, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(1797, new[] { new InteractiveElement(1202, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(3844, new[] { new InteractiveElement(57539, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(132096, new[] { new InteractiveElement(77916, new short[] { 44, 114 }, new short[0]) });
            Interactives.Add(131597, new[] { new InteractiveElement(57590, new short[] { 44, 114 }, new short[0]) });
        }

        public static short GetZaapCellId(int mapId)
        {
            short cellId;
            switch (mapId)
            {
                case 2323:
                case 139265: cellId = 314; break;
                case 21761028: cellId = 217; break;
                case 21760512: cellId = 273; break;
                case 16778242: cellId = 314; break;
                case 154642: cellId = 217; break;
                case 800: cellId = 300; break;
                case 138543: cellId = 215; break;
                case 147768: cellId = 242; break;
                case 141588: cellId = 313; break;
                case 143372: cellId = 257; break;
                case 144419: cellId = 216; break;
                case 133896: cellId = 235; break;
                case 17932: cellId = 116; break;
                case 13060: cellId = 173; break;
                case 12054: cellId = 329; break;
                case 8991: cellId = 228; break;
                case 13605: cellId = 227; break;
                case 15654: cellId = 259; break;
                case 15153: cellId = 327; break;
                case 14419207: cellId = 299; break;
                //case 20973313: cellId = 343; break;
                case 28050436: cellId = 387; break;
                case 131608: cellId = 381; break;
                case 5142: cellId = 467; break;
                case 2567: cellId = 235; break;
                case 148744: cellId = 143; break;
                case 1797: cellId = 287; break;
                case 3844: cellId = 254; break;
                case 132096: cellId = 206; break;
                case 131597: cellId = 355; break;
                default: cellId = 0; break;
            }
            return cellId;
        }
    }
}