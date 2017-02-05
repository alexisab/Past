using Past.Protocol.Files.Dlm.Elements;
using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm
{
    public class Cell
    {
        public short CellId { get; set; }
        public short ElementsCount { get; set; }
        public BasicElement[] Elements { get; set; }

        public Cell FromRaw(BigEndianReader raw)
        {
            Cell cell = new Cell
            {
                CellId = raw.ReadShort(),
                ElementsCount = raw.ReadShort()
            };
            cell.Elements = new BasicElement[cell.ElementsCount];
            for (int i = 0; i < cell.ElementsCount; i++)
            {
                BasicElement be = new BasicElement(cell);
                cell.Elements[i] = be.GetElementFromType(cell, raw);
            }
            return cell;
        }
    }
}
