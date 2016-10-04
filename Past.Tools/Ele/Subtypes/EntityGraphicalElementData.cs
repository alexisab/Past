using Past.Protocol.IO;

namespace Past.Tools.Ele.Subtypes
{
    public class EntityGraphicalElementData : GraphicalElementData
    {
        public string EntityLook { get; set; }
        public bool HorizontalSymmetry { get; set; }

        public EntityGraphicalElementData(int elementId, int elementType) : base(elementId, elementType)
        {
        }

        public override void FromRaw(BigEndianReader raw)
        {
            int entityLookLength = raw.ReadInt();
            EntityLook = raw.ReadUTFBytes((ushort)entityLookLength);
            HorizontalSymmetry = raw.ReadBoolean();
        }
    }
}
