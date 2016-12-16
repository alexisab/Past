using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class GameActionAcknowledgementMessage : NetworkMessage
	{
        public bool valid;
        public sbyte actionId;
        public override uint Id
        {
        	get { return 957; }
        }
        public GameActionAcknowledgementMessage()
        {
        }
        public GameActionAcknowledgementMessage(bool valid, sbyte actionId)
        {
            this.valid = valid;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(valid);
            writer.WriteSByte(actionId);
        }
        public override void Deserialize(IDataReader reader)
        {
            valid = reader.ReadBoolean();
            actionId = reader.ReadSByte();
		}
	}
}
