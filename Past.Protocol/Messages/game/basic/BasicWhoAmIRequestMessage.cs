using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class BasicWhoAmIRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5664; }
        }
        public BasicWhoAmIRequestMessage()
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
