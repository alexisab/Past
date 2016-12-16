using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class LeaveDialogMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5502; }
        }
        public LeaveDialogMessage()
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
