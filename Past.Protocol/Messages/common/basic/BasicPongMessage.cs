using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class BasicPongMessage : NetworkMessage
	{
        public bool quiet;
        public override uint Id
        {
        	get { return 183; }
        }
        public BasicPongMessage()
        {
        }
        public BasicPongMessage(bool quiet)
        {
            this.quiet = quiet;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(quiet);
        }
        public override void Deserialize(IDataReader reader)
        {
            quiet = reader.ReadBoolean();
		}
	}
}
