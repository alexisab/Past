using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ExchangeAcceptMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5508; }
        }
        public ExchangeAcceptMessage()
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
