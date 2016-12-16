using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameContextKickMessage : NetworkMessage
	{
        public int targetId;
        public override uint Id
        {
        	get { return 6081; }
        }
        public GameContextKickMessage()
        {
        }
        public GameContextKickMessage(int targetId)
        {
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            targetId = reader.ReadInt();
		}
	}
}
