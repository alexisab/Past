using Past.Protocol.IO;
using Past.Protocol.Types;

namespace Past.Protocol.Messages
{
    public class ChatServerWithObjectMessage : ChatServerMessage
	{
        public ObjectItem[] objects;
        public override uint Id
        {
        	get { return 883; }
        }
        public ChatServerWithObjectMessage()
        {
        }
        public ChatServerWithObjectMessage(sbyte channel, string content, int timestamp, string fingerprint, int senderId, string senderName, ObjectItem[] objects) : base(channel, content, timestamp, fingerprint, senderId, senderName)
        {
            this.objects = objects;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUShort((ushort)objects.Length);
            foreach (var entry in objects)
            {
                 entry.Serialize(writer);
            }
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var limit = reader.ReadUShort();
            objects = new ObjectItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 objects[i] = new ObjectItem();
                 objects[i].Deserialize(reader);
            }
		}
	}
}
