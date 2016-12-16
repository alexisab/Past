using Past.Protocol.IO;

namespace Past.Protocol.Messages
{
    public class EmoteListMessage : NetworkMessage
	{
        public sbyte[] emoteIds;
        public override uint Id
        {
        	get { return 5689; }
        }
        public EmoteListMessage()
        {
        }
        public EmoteListMessage(sbyte[] emoteIds)
        {
            this.emoteIds = emoteIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)emoteIds.Length);
            foreach (var entry in emoteIds)
            {
                 writer.WriteSByte(entry);
            }
        }
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            emoteIds = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 emoteIds[i] = reader.ReadSByte();
            }
		}
	}
}
