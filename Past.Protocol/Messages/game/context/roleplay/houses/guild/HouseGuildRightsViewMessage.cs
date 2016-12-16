using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class HouseGuildRightsViewMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5700; }
        }
        public HouseGuildRightsViewMessage()
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
