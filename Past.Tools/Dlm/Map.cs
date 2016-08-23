using Past.Protocol.IO;
using System;

namespace Past.Tools.Dlm
{
    public class Map
    {
        public byte MapVersion { get; set; }
        public int Id { get; set; }
        public int RelativeId { get; set; }
        public byte MapType { get; set; }
        public int SubareaId { get; set; }
        public int TopNeighbourId { get; set; }
        public int BottomNeighbourId { get; set; }
        public int LeftNeighbourId { get; set; }
        public int RightNeighbourId { get; set; }
        public int ShadowBonusOnEntities { get; set; }
        public byte BackgroundsCount { get; set; }
        //public Array BackgroundFixtures { get; set; }
        public byte ForegroundsCount { get; set; }
        //public Array ForegroundFixtures { get; set; }
        public int GroundCRC { get; set; }
        public byte LayersCount { get; set; }
        //public Array Layers { get; set; }
        public int CellsCount { get; set; }
        //public Array Cells { get; set; }

        public void FromRaw(BigEndianReader raw)
        {
            int header;
            try
            {
                header = raw.ReadByte();
                if (header != 77)
                {
                    throw new Exception("Unknown file format");
                }
                MapVersion = raw.ReadByte();
                Id = raw.ReadInt();
                RelativeId = raw.ReadInt();
                MapType = raw.ReadByte(); //0 = Outdoor / 1 = Indoor
                SubareaId = raw.ReadInt();
                TopNeighbourId = raw.ReadInt();
                BottomNeighbourId = raw.ReadInt();
                LeftNeighbourId = raw.ReadInt();
                RightNeighbourId = raw.ReadInt();
                ShadowBonusOnEntities = raw.ReadInt();
                BackgroundsCount = raw.ReadByte();
                //TO-DO
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
