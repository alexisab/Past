using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class PrismFightSwapRequestMessage : NetworkMessage
	{
        public int targetId;
        public override uint Id
        {
        	get { return 5901; }
        }
        public PrismFightSwapRequestMessage()
        {
        }
        public PrismFightSwapRequestMessage(int targetId)
        {
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            targetId = reader.ReadInt();
            if (targetId < 0)
                throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < 0");
		}
	}
}
