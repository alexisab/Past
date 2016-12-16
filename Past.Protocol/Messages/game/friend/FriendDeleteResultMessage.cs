using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class FriendDeleteResultMessage : NetworkMessage
	{
        public bool success;
        public string name;
        public override uint Id
        {
        	get { return 5601; }
        }
        public FriendDeleteResultMessage()
        {
        }
        public FriendDeleteResultMessage(bool success, string name)
        {
            this.success = success;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(success);
            writer.WriteUTF(name);
        }
        public override void Deserialize(IDataReader reader)
        {
            success = reader.ReadBoolean();
            name = reader.ReadUTF();
		}
	}
}
