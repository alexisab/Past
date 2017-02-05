using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm
{
    public class CellData
    {
        public int Floor { get; set; }
        public byte LosMov { get; set; }
        public byte Speed { get; set; }
        public byte MapChangeData { get; set; }

        public bool Los => (LosMov & 2) >> 1 == 1;

        public bool Mov => (LosMov & 1) == 1 && !NonWalkableDuringFight;

        public bool NonWalkableDuringFight => (LosMov & 3) >> 2 == 1;

        public bool Red => (LosMov & 4) >> 3 == 1;

        public bool Blue => (LosMov & 5) >> 4 == 1;

        public CellData FromRaw(BigEndianReader raw)
        {
            CellData cell = new CellData
            {
                Floor = raw.ReadByte() * 10
            };
            if (cell.Floor != -1280)
            {
                cell.LosMov = raw.ReadByte();
                cell.Speed = raw.ReadByte();
                cell.MapChangeData = raw.ReadByte();
            }
            return cell;
        }
    }
}
