using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class LivingObjectDissociateMessage : NetworkMessage
	{
        public int livingUID;
        public byte livingPosition;
        public override uint Id
        {
        	get { return 5723; }
        }
        public LivingObjectDissociateMessage()
        {
        }
        public LivingObjectDissociateMessage(int livingUID, byte livingPosition)
        {
            this.livingUID = livingUID;
            this.livingPosition = livingPosition;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(livingUID);
            writer.WriteByte(livingPosition);
        }
        public override void Deserialize(IDataReader reader)
        {
            livingUID = reader.ReadInt();
            if (livingUID < 0)
                throw new Exception("Forbidden value on livingUID = " + livingUID + ", it doesn't respect the following condition : livingUID < 0");
            livingPosition = reader.ReadByte();
            if (livingPosition < 0 || livingPosition > 255)
                throw new Exception("Forbidden value on livingPosition = " + livingPosition + ", it doesn't respect the following condition : livingPosition < 0 || livingPosition > 255");
		}
	}
}
