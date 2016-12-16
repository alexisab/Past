using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameContextReadyMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 6071; }
        }
        public GameContextReadyMessage()
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
