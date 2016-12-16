using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class PrismBalanceRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5839; }
        }
        public PrismBalanceRequestMessage()
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
