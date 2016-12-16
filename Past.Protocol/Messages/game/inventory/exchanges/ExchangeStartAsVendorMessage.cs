using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ExchangeStartAsVendorMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5775; }
        }
        public ExchangeStartAsVendorMessage()
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
