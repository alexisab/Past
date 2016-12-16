using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ExchangeStartOkNpcTradeMessage : NetworkMessage
	{
        public int npcId;
        public override uint Id
        {
        	get { return 5785; }
        }
        public ExchangeStartOkNpcTradeMessage()
        {
        }
        public ExchangeStartOkNpcTradeMessage(int npcId)
        {
            this.npcId = npcId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(npcId);
        }
        public override void Deserialize(IDataReader reader)
        {
            npcId = reader.ReadInt();
		}
	}
}
