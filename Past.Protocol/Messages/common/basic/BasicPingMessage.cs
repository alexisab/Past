using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class BasicPingMessage : NetworkMessage
	{
        public bool quiet;
        public override uint Id
        {
        	get { return 182; }
        }
        public BasicPingMessage()
        {
        }
        public BasicPingMessage(bool quiet)
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
