using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class NicknameRegistrationMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5640; }
        }
        public NicknameRegistrationMessage()
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
