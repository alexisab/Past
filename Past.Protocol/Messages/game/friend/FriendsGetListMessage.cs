using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class FriendsGetListMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 4001; }
        }
        public FriendsGetListMessage()
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
