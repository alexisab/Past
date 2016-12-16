using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class GameRolePlayPlayerLifeStatusMessage : NetworkMessage
	{
        public sbyte state;
        public override uint Id
        {
        	get { return 5996; }
        }
        public GameRolePlayPlayerLifeStatusMessage()
        {
        }
        public GameRolePlayPlayerLifeStatusMessage(sbyte state)
        {
            this.state = state;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(state);
        }
        public override void Deserialize(IDataReader reader)
        {
            state = reader.ReadSByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
		}
	}
}
