using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class HelloGameMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 101; }
        }
        public HelloGameMessage()
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
