using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class SpellUpgradeFailureMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 1202; }
        }
        public SpellUpgradeFailureMessage()
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
