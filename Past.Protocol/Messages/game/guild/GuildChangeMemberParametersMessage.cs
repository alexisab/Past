using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class GuildChangeMemberParametersMessage : NetworkMessage
	{
        public int memberId;
        public short rank;
        public sbyte experienceGivenPercent;
        public uint rights;
        public override uint Id
        {
        	get { return 5549; }
        }
        public GuildChangeMemberParametersMessage()
        {
        }
        public GuildChangeMemberParametersMessage(int memberId, short rank, sbyte experienceGivenPercent, uint rights)
        {
            this.memberId = memberId;
            this.rank = rank;
            this.experienceGivenPercent = experienceGivenPercent;
            this.rights = rights;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(memberId);
            writer.WriteShort(rank);
            writer.WriteSByte(experienceGivenPercent);
            writer.WriteUInt(rights);
        }
        public override void Deserialize(IDataReader reader)
        {
            memberId = reader.ReadInt();
            if (memberId < 0)
                throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0");
            rank = reader.ReadShort();
            if (rank < 0)
                throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0");
            experienceGivenPercent = reader.ReadSByte();
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
                throw new Exception("Forbidden value on experienceGivenPercent = " + experienceGivenPercent + ", it doesn't respect the following condition : experienceGivenPercent < 0 || experienceGivenPercent > 100");
            rights = reader.ReadUInt();
            if (rights < 0 || rights > 4294967295)
                throw new Exception("Forbidden value on rights = " + rights + ", it doesn't respect the following condition : rights < 0 || rights > 4294967295");
		}
	}
}
