using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class PrismCurrentBonusRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5840; }
        }
        public PrismCurrentBonusRequestMessage()
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
