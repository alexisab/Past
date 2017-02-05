using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm
{
    public class Layer
    {
        public int LayerId { get; set; }
        public short CellsCount { get; set; }
        public Cell[] Cells { get; set; }

        public Layer FromRaw(BigEndianReader raw)
        {
            Layer layer = new Layer
            {
                LayerId = raw.ReadInt(),
                CellsCount = raw.ReadShort()
            };
            layer.Cells = new Cell[layer.CellsCount];
            for (int i = 0; i < layer.CellsCount; i++)
            {
                Cell c = new Cell();
                layer.Cells[i] = c.FromRaw(raw);
            }
            return layer;
        }
    }
}
