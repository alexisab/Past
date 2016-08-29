using System;

namespace Past.Tools.Dlm.Elements
{
    public class BasicElement
    {
        public BasicElement GetElementFromType(byte type, Cell cell)
        {
            switch (type)
            {
                case 2:
                    return new GraphicalElement(cell);
                case 33:
                    return new SoundElement(cell);
            };
            throw new Exception("Un élément de type inconnu " + type + " a été trouvé sur la cellule " + cell.CellId + "!");
        }
    }
}
