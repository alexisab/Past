using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameMapNoMovementMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 954; }
        }
        public GameMapNoMovementMessage()
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
