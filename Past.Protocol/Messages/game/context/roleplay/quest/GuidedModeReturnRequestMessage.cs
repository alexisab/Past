using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GuidedModeReturnRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 6088; }
        }
        public GuidedModeReturnRequestMessage()
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
