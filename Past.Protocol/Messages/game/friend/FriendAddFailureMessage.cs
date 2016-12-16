using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class FriendAddFailureMessage : NetworkMessage
	{
        public sbyte reason;
        public override uint Id
        {
        	get { return 5600; }
        }
        public FriendAddFailureMessage()
        {
        }
        public FriendAddFailureMessage(sbyte reason)
        {
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(reason);
        }
        public override void Deserialize(IDataReader reader)
        {
            reason = reader.ReadSByte();
            if (reason < 0)
                throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
		}
	}
}
