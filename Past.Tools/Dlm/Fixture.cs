using Past.Protocol.IO;
using System.Drawing;

namespace Past.Tools.Dlm
{
    public class Fixture
    {
        public int FixtureId { get; set; }
        public Point Offset { get; set; }
        public short Rotation { get; set; }
        public short ScaleX { get; set; }
        public short ScaleY { get; set; }
        public byte RedMultiplier { get; set; }
        public byte GreenMultiplier { get; set; }
        public byte BlueMultiplier { get; set; }
        public int Hue { get { return (int)(RedMultiplier << 16 | GreenMultiplier << 8 | BlueMultiplier); } }
        public byte Alpha { get; set; }

        public Fixture FromRaw(BigEndianReader raw)
        {
            return new Fixture
            {
                FixtureId = raw.ReadInt(),
                Offset = new Point(raw.ReadShort(), raw.ReadShort()),
                Rotation = raw.ReadShort(),
                ScaleX = raw.ReadShort(),
                ScaleY = raw.ReadShort(),
                RedMultiplier = raw.ReadByte(),
                GreenMultiplier = raw.ReadByte(),
                BlueMultiplier = raw.ReadByte(),
                Alpha = raw.ReadByte()
            };
        }

        public override string ToString()
        {
            return string.Format("FixtureId : {0} Offset : {1} Rotation : {2} ScaleX : {3} ScaleY : {4} RedMultiplier : {5} GreenMultiplier : {6} BlueMultiplier : {7} Hue : {8} Alpha : {9}", FixtureId, Offset, Rotation, ScaleX, ScaleY, RedMultiplier, GreenMultiplier, BlueMultiplier, Hue, Alpha);
        }
    }
}
