using Past.Protocol.IO;
using System.Drawing;

namespace Past.Tools.Ele.Subtypes
{
    public class NormalGraphicalElementData : GraphicalElementData
    {
        public int GfxId { get; set; }
        public byte Height { get; set; }
        public bool HorizontalSymmetry { get; set; }
        public Point Origin { get; set; }
        public Point Size { get; set; }

        public NormalGraphicalElementData(int elementId, int elementType) : base (elementId, elementType)
        {
        }

        public override void FromRaw(BigEndianReader raw)
        {
            GfxId = raw.ReadInt();
            Height = raw.ReadByte();
            HorizontalSymmetry = raw.ReadBoolean();
            Origin = new Point(raw.ReadShort(), raw.ReadShort());
            Size = new Point(raw.ReadShort(), raw.ReadShort());
        }
    }
}
