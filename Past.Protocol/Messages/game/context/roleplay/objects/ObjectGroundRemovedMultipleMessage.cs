using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class ObjectGroundRemovedMultipleMessage : NetworkMessage
	{
        public short[] cells;
        public override uint Id
        {
        	get { return 5944; }
        }
        public ObjectGroundRemovedMultipleMessage()
        {
        }
        public ObjectGroundRemovedMultipleMessage(short[] cells)
        {
            this.cells = cells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)cells.Length);
            foreach (var entry in cells)
            {
                 writer.WriteShort(entry);
            }
        }
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            cells = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 cells[i] = reader.ReadShort();
            }
		}
	}
}
