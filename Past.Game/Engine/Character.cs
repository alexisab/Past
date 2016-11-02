using Past.Common.Data;
using Past.Common.Database.Record;
using Past.Protocol.Enums;
using Past.Protocol.Types;

namespace Past.Game.Engine
{
    public class Character
    {
        private CharacterRecord Record { get; set; }
        private Network.Client Client { get; set; }
        public int Id { get { return Record.Id; } }
        public string Name { get { return Record.Name; } }
        public byte Level { get { return Record.Level; } }
        public EntityLook Look { get { return Record.EntityLook; } }
        public BreedEnum Breed { get { return Record.Breed; } }
        public bool Sex { get { return Record.Sex; } }

        public Character(CharacterRecord record, Network.Client client)
        {
            Record = record;
            Client = client;
        }

        public void Save()
        {

        }

        public CharacterBaseInformations GetCharacterBaseInformations() => new CharacterBaseInformations(Record.Id, Record.Name, Record.Level, Record.EntityLook, (sbyte)Record.Breed, Record.Sex);

        public ActorExtendedAlignmentInformations GetActorExtendedAlignmentInformations() => new ActorExtendedAlignmentInformations((sbyte)Record.AlignementSide, 0, (sbyte)Experience.GetCharacterGrade(Record.Honor), 0, Record.Honor, Record.Dishonor, Record.PvPEnabled);
    }
}
