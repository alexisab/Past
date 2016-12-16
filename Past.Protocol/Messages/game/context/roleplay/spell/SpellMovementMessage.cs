using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class SpellMovementMessage : NetworkMessage
	{
        public short spellId;
        public byte position;
        public override uint Id
        {
        	get { return 5746; }
        }
        public SpellMovementMessage()
        {
        }
        public SpellMovementMessage(short spellId, byte position)
        {
            this.spellId = spellId;
            this.position = position;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(spellId);
            writer.WriteByte(position);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellId = reader.ReadShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            position = reader.ReadByte();
            if (position < 63 || position > 255)
                throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 63 || position > 255");
		}
	}
}
