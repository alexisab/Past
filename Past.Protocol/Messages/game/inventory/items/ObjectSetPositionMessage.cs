using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class ObjectSetPositionMessage : NetworkMessage
	{
        public int objectUID;
        public byte position;
        public int quantity;
        public override uint Id
        {
        	get { return 3021; }
        }
        public ObjectSetPositionMessage()
        {
        }
        public ObjectSetPositionMessage(int objectUID, byte position, int quantity)
        {
            this.objectUID = objectUID;
            this.position = position;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(objectUID);
            writer.WriteByte(position);
            writer.WriteInt(quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = reader.ReadInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
            position = reader.ReadByte();
            if (position < 0 || position > 255)
                throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 0 || position > 255");
            quantity = reader.ReadInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
		}
	}
}
