using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameActionFightTackledMessage : AbstractGameActionMessage
	{
        public override uint Id
        {
        	get { return 1004; }
        }
        public GameActionFightTackledMessage()
        {
        }
        public GameActionFightTackledMessage(short actionId, int sourceId) : base(actionId, sourceId)
        {
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
		}
	}
}
