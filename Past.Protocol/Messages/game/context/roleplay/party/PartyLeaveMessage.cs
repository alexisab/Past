using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class PartyLeaveMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5594; }
        }
        public PartyLeaveMessage()
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
