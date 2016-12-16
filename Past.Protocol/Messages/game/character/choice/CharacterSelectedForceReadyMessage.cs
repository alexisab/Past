using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class CharacterSelectedForceReadyMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 6072; }
        }
        public CharacterSelectedForceReadyMessage()
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
