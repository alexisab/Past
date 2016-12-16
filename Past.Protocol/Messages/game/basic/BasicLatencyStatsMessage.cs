using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class BasicLatencyStatsMessage : NetworkMessage
	{
        public ushort latency;
        public short sampleCount;
        public short max;
        public override uint Id
        {
        	get { return 5663; }
        }
        public BasicLatencyStatsMessage()
        {
        }
        public BasicLatencyStatsMessage(ushort latency, short sampleCount, short max)
        {
            this.latency = latency;
            this.sampleCount = sampleCount;
            this.max = max;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(latency);
            writer.WriteShort(sampleCount);
            writer.WriteShort(max);
        }
        public override void Deserialize(IDataReader reader)
        {
            latency = reader.ReadUShort();
            if (latency < 0 || latency > 65535)
                throw new Exception("Forbidden value on latency = " + latency + ", it doesn't respect the following condition : latency < 0 || latency > 65535");
            sampleCount = reader.ReadShort();
            if (sampleCount < 0)
                throw new Exception("Forbidden value on sampleCount = " + sampleCount + ", it doesn't respect the following condition : sampleCount < 0");
            max = reader.ReadShort();
            if (max < 0)
                throw new Exception("Forbidden value on max = " + max + ", it doesn't respect the following condition : max < 0");
		}
	}
}
