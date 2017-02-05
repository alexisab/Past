using System.Drawing;
using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm.Elements
{
    public class GraphicalElement : BasicElement
    {
        public int ElementId { get; set; }
        public ColorMultiplicator Hue { get; set; }
        public ColorMultiplicator Shadow { get; set; }
        public ColorMultiplicator FinalTeint { get; set; }
        public Point Offset { get; set; }
        public byte Altitude { get; set; }
        public uint Identifier { get; set; }

        public GraphicalElement(Cell cell) : base(cell)
        {
        }

        public GraphicalElement FromRaw(Cell cell, BigEndianReader raw)
        {
            GraphicalElement element = new GraphicalElement(cell)
            {
                ElementId = raw.ReadInt(),
                Hue = new ColorMultiplicator(raw.ReadByte(), raw.ReadByte(), raw.ReadByte()),
                Shadow = new ColorMultiplicator(raw.ReadByte(), raw.ReadByte(), raw.ReadByte()),
                Offset = new Point(raw.ReadByte(), raw.ReadByte()),
                Altitude = raw.ReadByte(),
                Identifier = raw.ReadUInt()
            };
            //CalculateFinalTeint();
            return element;
        }

        public void CalculateFinalTeint()
        {
            int r = Hue.Red + Shadow.Red;
            int g = Hue.Green + Shadow.Green;
            int b = Hue.Blue + Shadow.Blue;
            r = ColorMultiplicator.Clamp((r + 128) * 2, 0, 512);
            g = ColorMultiplicator.Clamp((g + 128) * 2, 0, 512);
            b = ColorMultiplicator.Clamp((b + 128) * 2, 0, 512);
            FinalTeint = new ColorMultiplicator(r, g, b, true);
        }
    }
}
