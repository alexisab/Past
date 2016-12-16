using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ExchangeStartedMessage : NetworkMessage
	{
        public sbyte exchangeType;
        public override uint Id
        {
        	get { return 5512; }
        }
        public ExchangeStartedMessage()
        {
        }
        public ExchangeStartedMessage(sbyte exchangeType)
        {
            this.exchangeType = exchangeType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(exchangeType);
        }
        public override void Deserialize(IDataReader reader)
        {
            exchangeType = reader.ReadSByte();
		}
	}
}
