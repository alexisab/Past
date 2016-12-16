using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GuidedModeQuitRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 6092; }
        }
        public GuidedModeQuitRequestMessage()
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
