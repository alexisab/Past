using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ExchangeSellOkMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5792; }
        }
        public ExchangeSellOkMessage()
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
