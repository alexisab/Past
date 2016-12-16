using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameRolePlayGameOverMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 746; }
        }
        public GameRolePlayGameOverMessage()
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
