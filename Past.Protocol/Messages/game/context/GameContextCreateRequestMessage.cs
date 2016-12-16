using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameContextCreateRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 250; }
        }
        public GameContextCreateRequestMessage()
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
