using Past.Protocol.IO;
using System;

namespace Past.Tools.Dlm.Elements
{
    public class BasicElement
    {
        public Cell Cell { get; set; }

        public BasicElement(Cell cell)
        {
            Cell = cell;
        }

        public BasicElement GetElementFromType(Cell cell, BigEndianReader raw)
        {
            byte type = raw.ReadByte();
            switch (type)
            {
                case 2:
                    return new GraphicalElement(cell).FromRaw(cell, raw);
                case 33:
                    return new SoundElement(cell).FromRaw(cell, raw);
                default:
                    throw new Exception($"Un élément de type inconnu {type} a été trouvé sur la cellule {cell.CellId} !");
            }
        }
    }
}
