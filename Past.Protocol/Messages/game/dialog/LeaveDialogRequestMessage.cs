using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class LeaveDialogRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5501; }
        }
        public LeaveDialogRequestMessage()
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
