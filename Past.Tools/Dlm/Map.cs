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
        public Fixture[] BackgroundFixtures { get; set; }
        public byte ForegroundsCount { get; set; }
        public Fixture[] ForegroundFixtures { get; set; }
        public int GroundCRC { get; set; }
        public byte LayersCount { get; set; }
        public Layer[] Layers { get; set; }
        public int CellsCount { get { return 560; } }
        public CellData[] Cells { get; set; }

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
                BackgroundFixtures = new Fixture[BackgroundsCount];
                for (int i = 0; i < BackgroundsCount; i++)
                {
                    Fixture bg = new Fixture();
                    BackgroundFixtures[i] = bg.FromRaw(raw);
                }
                ForegroundsCount = raw.ReadByte();
                ForegroundFixtures = new Fixture[ForegroundsCount];
                for (int i = 0; i < ForegroundsCount; i++)
                {
                    Fixture fg = new Fixture();
                    ForegroundFixtures[i] = fg.FromRaw(raw);
                }
                GroundCRC = raw.ReadInt();
                raw.ReadInt();
                LayersCount = raw.ReadByte();
                Layers = new Layer[LayersCount];
                for (int i = 0; i < LayersCount; i++)
                {
                    Layer la = new Layer();
                    Layers[i] = la.FromRaw(raw);
                }
                Cells = new CellData[CellsCount];
                for (int i = 0; i < CellsCount; i++)
                {
                    CellData cd = new CellData();
                    Cells[i] = cd.FromRaw(raw);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
