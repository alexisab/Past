using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class SubscriptionZoneMessage : NetworkMessage
	{
        public bool active;
        public override uint Id
        {
        	get { return 5573; }
        }
        public SubscriptionZoneMessage()
        {
        }
        public SubscriptionZoneMessage(bool active)
        {
            this.active = active;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(active);
        }
        public override void Deserialize(IDataReader reader)
        {
            active = reader.ReadBoolean();
		}
	}
}
