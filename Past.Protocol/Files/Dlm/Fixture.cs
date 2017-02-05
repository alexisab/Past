using System.Drawing;
using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm
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
        public int Hue => RedMultiplier << 16 | GreenMultiplier << 8 | BlueMultiplier;
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

        public override string ToString() => $"FixtureId : {FixtureId} Offset : {Offset} Rotation : {Rotation} ScaleX : {ScaleX} ScaleY : {ScaleY} RedMultiplier : {RedMultiplier} GreenMultiplier : {GreenMultiplier} BlueMultiplier : {BlueMultiplier} Hue : {Hue} Alpha : {Alpha}";
    }
}
