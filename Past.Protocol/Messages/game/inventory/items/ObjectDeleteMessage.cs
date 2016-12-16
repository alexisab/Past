using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class ObjectDeleteMessage : NetworkMessage
	{
        public int objectUID;
        public int quantity;
        public override uint Id
        {
        	get { return 3022; }
        }
        public ObjectDeleteMessage()
        {
        }
        public ObjectDeleteMessage(int objectUID, int quantity)
        {
            this.objectUID = objectUID;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(objectUID);
            writer.WriteInt(quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = reader.ReadInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
            quantity = reader.ReadInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
		}
	}
}
