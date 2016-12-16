using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class AdminQuietCommandMessage : AdminCommandMessage
	{
        public override uint Id
        {
        	get { return 5662; }
        }
        public AdminQuietCommandMessage()
        {
        }
        public AdminQuietCommandMessage(string content) : base(content)
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
