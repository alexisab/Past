using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class PrismUseRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 6041; }
        }
        public PrismUseRequestMessage()
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
