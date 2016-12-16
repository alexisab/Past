using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class BasicWhoIsRequestMessage : NetworkMessage
	{
        public string search;
        public override uint Id
        {
        	get { return 181; }
        }
        public BasicWhoIsRequestMessage()
        {
        }
        public BasicWhoIsRequestMessage(string search)
        {
            this.search = search;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(search);
        }
        public override void Deserialize(IDataReader reader)
        {
            search = reader.ReadUTF();
		}
	}
}
