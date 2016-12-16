using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class LockableCodeResultMessage : NetworkMessage
	{
        public bool success;
        public override uint Id
        {
        	get { return 5672; }
        }
        public LockableCodeResultMessage()
        {
        }
        public LockableCodeResultMessage(bool success)
        {
            this.success = success;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(success);
        }
        public override void Deserialize(IDataReader reader)
        {
            success = reader.ReadBoolean();
		}
	}
}
