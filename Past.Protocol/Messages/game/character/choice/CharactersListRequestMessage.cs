using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class CharactersListRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 150; }
        }
        public CharactersListRequestMessage()
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
