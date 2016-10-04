using Past.Protocol.IO;

namespace Past.Tools.Ele
{
    public class GraphicalElementData
    {
        public int Id { get; set; }
        public int Type { get; set; }

        public GraphicalElementData(int elementId, int elementType)
        {
            Id = elementId;
            Type = elementType;
        }

        public virtual void FromRaw(BigEndianReader raw)
        {
            throw new System.FieldAccessException();
        }
    }
}
