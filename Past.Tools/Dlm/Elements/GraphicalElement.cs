using Past.Protocol.IO;
using System.Drawing;

namespace Past.Tools.Dlm.Elements
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
            GraphicalElement element = new GraphicalElement(cell);
            element.ElementId = raw.ReadInt();
            element.Hue = new ColorMultiplicator(raw.ReadByte(), raw.ReadByte(), raw.ReadByte());
            element.Shadow = new ColorMultiplicator(raw.ReadByte(), raw.ReadByte(), raw.ReadByte());
            element.Offset = new Point(raw.ReadByte(), raw.ReadByte());
            element.Altitude = raw.ReadByte();
            element.Identifier = raw.ReadUInt();
            //CalculateFinalTeint();
            return element;
        }

        public void CalculateFinalTeint()
        {
            var r = Hue.Red + Shadow.Red;
            var g = Hue.Green + Shadow.Green;
            var b = Hue.Blue + Shadow.Blue;
            r = ColorMultiplicator.Clamp((r + 128) * 2, 0, 512);
            g = ColorMultiplicator.Clamp((g + 128) * 2, 0, 512);
            b = ColorMultiplicator.Clamp((b + 128) * 2, 0, 512);
            FinalTeint = new ColorMultiplicator(r, g, b, true);
        }
    }
}
