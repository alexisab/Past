using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameActionFightLifePointsVariationMessage : AbstractGameActionMessage
	{
        public int targetId;
        public short delta;
        public override uint Id
        {
        	get { return 5598; }
        }
        public GameActionFightLifePointsVariationMessage()
        {
        }
        public GameActionFightLifePointsVariationMessage(short actionId, int sourceId, int targetId, short delta) : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.delta = delta;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(targetId);
            writer.WriteShort(delta);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadInt();
            delta = reader.ReadShort();
		}
	}
}
