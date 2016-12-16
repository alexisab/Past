using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class ObjectFoundWhileRecoltingMessage : NetworkMessage
	{
        public int genericId;
        public int quantity;
        public int ressourceGenericId;
        public override uint Id
        {
        	get { return 6017; }
        }
        public ObjectFoundWhileRecoltingMessage()
        {
        }
        public ObjectFoundWhileRecoltingMessage(int genericId, int quantity, int ressourceGenericId)
        {
            this.genericId = genericId;
            this.quantity = quantity;
            this.ressourceGenericId = ressourceGenericId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(genericId);
            writer.WriteInt(quantity);
            writer.WriteInt(ressourceGenericId);
        }
        public override void Deserialize(IDataReader reader)
        {
            genericId = reader.ReadInt();
            if (genericId < 0)
                throw new Exception("Forbidden value on genericId = " + genericId + ", it doesn't respect the following condition : genericId < 0");
            quantity = reader.ReadInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
            ressourceGenericId = reader.ReadInt();
            if (ressourceGenericId < 0)
                throw new Exception("Forbidden value on ressourceGenericId = " + ressourceGenericId + ", it doesn't respect the following condition : ressourceGenericId < 0");
		}
	}
}
