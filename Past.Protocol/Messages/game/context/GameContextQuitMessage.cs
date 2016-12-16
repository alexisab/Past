using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameContextQuitMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 255; }
        }
        public GameContextQuitMessage()
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
