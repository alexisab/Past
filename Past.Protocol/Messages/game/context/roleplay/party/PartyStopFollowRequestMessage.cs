using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class PartyStopFollowRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5574; }
        }
        public PartyStopFollowRequestMessage()
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
