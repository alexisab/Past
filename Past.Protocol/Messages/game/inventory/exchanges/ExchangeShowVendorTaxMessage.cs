using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ExchangeShowVendorTaxMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5783; }
        }
        public ExchangeShowVendorTaxMessage()
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
