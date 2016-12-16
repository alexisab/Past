using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class CharactersListErrorMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5545; }
        }
        public CharactersListErrorMessage()
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
