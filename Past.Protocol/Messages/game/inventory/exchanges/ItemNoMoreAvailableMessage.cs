using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ItemNoMoreAvailableMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5769; }
        }
        public ItemNoMoreAvailableMessage()
        {
        }
        public override void Serialize(IDataWriter writer)
        {
        }
        public override void Deserialize(IDataReader reader)
        {
		}
	}
}
