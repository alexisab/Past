using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class NicknameAcceptedMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5641; }
        }
        public NicknameAcceptedMessage()
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
