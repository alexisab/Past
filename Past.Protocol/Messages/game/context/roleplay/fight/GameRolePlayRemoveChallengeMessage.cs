using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameRolePlayRemoveChallengeMessage : NetworkMessage
	{
        public int fightId;
        public override uint Id
        {
        	get { return 300; }
        }
        public GameRolePlayRemoveChallengeMessage()
        {
        }
        public GameRolePlayRemoveChallengeMessage(int fightId)
        {
            this.fightId = fightId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(fightId);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadInt();
		}
	}
}
