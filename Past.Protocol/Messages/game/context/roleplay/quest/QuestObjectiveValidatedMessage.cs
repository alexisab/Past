using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class QuestObjectiveValidatedMessage : NetworkMessage
	{
        public ushort questId;
        public ushort objectiveId;
        public override uint Id
        {
        	get { return 6098; }
        }
        public QuestObjectiveValidatedMessage()
        {
        }
        public QuestObjectiveValidatedMessage(ushort questId, ushort objectiveId)
        {
            this.questId = questId;
            this.objectiveId = objectiveId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(questId);
            writer.WriteUShort(objectiveId);
        }
        public override void Deserialize(IDataReader reader)
        {
            questId = reader.ReadUShort();
            if (questId < 0 || questId > 65535)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0 || questId > 65535");
            objectiveId = reader.ReadUShort();
            if (objectiveId < 0 || objectiveId > 65535)
                throw new Exception("Forbidden value on objectiveId = " + objectiveId + ", it doesn't respect the following condition : objectiveId < 0 || objectiveId > 65535");
		}
	}
}
