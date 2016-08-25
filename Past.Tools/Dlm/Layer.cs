using Past.Protocol.IO;

namespace Past.Tools.Dlm
{
    public class Layer
    {
        /*public int LayerId { get; set; }
        public short CellsCount { get; set; }
        public Cell[] Cells { get; set; }

        public Layer FromRaw(BigEndianReader raw)
        {
            Layer layer = new Layer();
            layer.LayerId = raw.ReadInt();
            layer.CellsCount = raw.ReadShort();
            layer.Cells = new Cell[layer.CellsCount];
            for (int i = 0; i < layer.CellsCount; i++)
            {
                Cell c = new Cell();
                Cells[i] = c.FromRaw(raw);
            }
            return layer;
        }*/
    }
}
