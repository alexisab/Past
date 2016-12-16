using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class GameMapMovementCancelMessage : NetworkMessage
	{
        public short cellId;
        public override uint Id
        {
        	get { return 953; }
        }
        public GameMapMovementCancelMessage()
        {
        }
        public GameMapMovementCancelMessage(short cellId)
        {
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(cellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            cellId = reader.ReadShort();
            if (cellId < 0 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
		}
	}
}
