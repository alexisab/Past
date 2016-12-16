using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameFightTurnFinishMessage : NetworkMessage
	{
        public override uint Id
        {
        	get { return 718; }
        }
        public GameFightTurnFinishMessage()
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
