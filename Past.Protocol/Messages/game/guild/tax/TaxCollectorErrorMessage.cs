using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class TaxCollectorErrorMessage : NetworkMessage
	{
        public sbyte reason;
        public override uint Id
        {
        	get { return 5634; }
        }
        public TaxCollectorErrorMessage()
        {
        }
        public TaxCollectorErrorMessage(sbyte reason)
        {
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(reason);
        }
        public override void Deserialize(IDataReader reader)
        {
            reason = reader.ReadSByte();
		}
	}
}
