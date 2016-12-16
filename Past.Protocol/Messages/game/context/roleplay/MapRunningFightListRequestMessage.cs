using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class MapRunningFightListRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5742; }
        }
        public MapRunningFightListRequestMessage()
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
