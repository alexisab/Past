using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class BasicNoOperationMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 176; }
        }
        public BasicNoOperationMessage()
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
