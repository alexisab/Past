using Past.Protocol.IO;

namespace Past.Tools.Ele.Subtypes
{
    public class ParticlesGraphicalElementData : GraphicalElementData
    {
        public short ScriptId { get; set; }

        public ParticlesGraphicalElementData(int elementId, int elementType) : base(elementId, elementType)
        {
        }

        public override void FromRaw(BigEndianReader raw)
        {
            ScriptId = raw.ReadShort();
        }
    }
}
