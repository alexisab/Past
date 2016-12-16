using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class ExchangeObjectRemovedFromBagMessage : ExchangeObjectMessage
	{
        public int objectUID;
        public override uint Id
        {
        	get { return 6010; }
        }
        public ExchangeObjectRemovedFromBagMessage()
        {
        }
        public ExchangeObjectRemovedFromBagMessage(bool remote, int objectUID) : base(remote)
        {
            this.objectUID = objectUID;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(objectUID);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            objectUID = reader.ReadInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
		}
	}
}
