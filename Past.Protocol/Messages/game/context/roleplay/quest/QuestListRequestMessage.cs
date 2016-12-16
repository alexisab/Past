using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class QuestListRequestMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 5623; }
        }
        public QuestListRequestMessage()
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
