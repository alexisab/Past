using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameFightTurnReadyMessage : NetworkMessage
	{
        public bool isReady;
        public override uint Id
        {
        	get { return 716; }
        }
        public GameFightTurnReadyMessage()
        {
        }
        public GameFightTurnReadyMessage(bool isReady)
        {
            this.isReady = isReady;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(isReady);
        }
        public override void Deserialize(IDataReader reader)
        {
            isReady = reader.ReadBoolean();
		}
	}
}
