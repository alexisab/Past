using Past.Common.Data;
using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Protocol.Enums;
using Past.Protocol.Types;

namespace Past.Game.Engine
{
    public class CharacterEngine
    {
        private CharacterRecord Record { get; set; }
        private Network.Client Client { get; set; }
        public int Id { get { return Record.Id; } }
        public string Name { get { return Record.Name; } }
        public byte Level { get { return Record.Level; } }
        public EntityLook Look { get { return Functions.BuildEntityLook(Record); } }
        public BreedEnum Breed { get { return Record.Breed; } }
        public bool Sex { get { return Record.Sex; } }
        public Map Map { get { return Map.Maps[Record.MapId]; } }
        public MapEngine CurrentMap { get { return (MapEngine)Map.Maps[Record.MapId].Instance; } }
        public int CurrentMapId { get { return Record.MapId; } set { Record.MapId = value; } }
        public AlignmentSideEnum AlignmentSide { get { return Record.AlignementSide; } }
        public ushort Honor { get { return Record.Honor; } }
        public ushort Dishonor { get { return Record.Dishonor; } }
        public bool PvPEnabled { get { return Record.PvPEnabled; } }
        public long Experience { get { return Record.Experience; } }
        public long ExperienceLevelFloor { get { return Common.Data.Experience.GetExperienceLevelFloor(Record.Level).CharacterXp; } }
        public long ExperienceNextLevelFloor { get { return Record.Level == 200 ? Record.Experience : Common.Data.Experience.GetNextExperienceLevelFloor(Record.Level).CharacterXp; } }
        public int Kamas { get { return Record.Kamas; } set { Record.Kamas = value; } }
        public short StatsPoints { get { return Record.StatsPoints; } set { Record.StatsPoints = value; } }
        public short SpellsPoints { get { return Record.SpellsPoints; } set { Record.SpellsPoints = value; } }
        public int Health { get { return Record.Health; } set { Record.Health = value; } }
        public short Energy { get { return Record.Energy; } set { Record.Energy = value; } }
        public short CellId { get { return Record.CellId; } set { Record.CellId = value; } }
        public DirectionsEnum Direction { get { return Record.Direction; } set { Record.Direction = value; } }
        public EntityDispositionInformations Disposition { get { return new EntityDispositionInformations(Record.CellId, (sbyte)Record.Direction); } }

        public CharacterEngine(CharacterRecord record, Network.Client client)
        {
            Record = record;
            Client = client;
        }

        public void Save()
        {
            //Record.Update();
        }

        public GameRolePlayCharacterInformations GetGameRolePlayCharacterInformations() => new GameRolePlayCharacterInformations(Id, Look, Disposition, Name, new HumanInformations(new EntityLook[0], 0, 0, new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, false, false, false, false, false), 0), GetActorAlignmentInformations());

        public CharacterBaseInformations GetCharacterBaseInformations() => new CharacterBaseInformations(Id, Name, Level, Look, (sbyte)Breed, Sex);

        public ActorAlignmentInformations GetActorAlignmentInformations() => new ActorAlignmentInformations((sbyte)AlignmentSide, 0, (sbyte)Common.Data.Experience.GetCharacterGrade(Honor), 0);

        public ActorExtendedAlignmentInformations GetActorExtendedAlignmentInformations() => new ActorExtendedAlignmentInformations((sbyte)AlignmentSide, 0, (sbyte)Common.Data.Experience.GetCharacterGrade(Honor), 0, Honor, Dishonor, PvPEnabled);
    }
}