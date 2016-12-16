using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class EmoteRemoveMessage : NetworkMessage
	{
        public sbyte emoteId;
        public override uint Id
        {
        	get { return 5687; }
        }
        public EmoteRemoveMessage()
        {
        }
        public EmoteRemoveMessage(sbyte emoteId)
        {
            this.emoteId = emoteId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(emoteId);
        }
        public override void Deserialize(IDataReader reader)
        {
            emoteId = reader.ReadSByte();
            if (emoteId < 0)
                throw new Exception("Forbidden value on emoteId = " + emoteId + ", it doesn't respect the following condition : emoteId < 0");
		}
	}
}
