using Past.Common.Data;
using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Protocol.Enums;
using Past.Protocol.Types;
using System;

namespace Past.Game.Engine
{
    public class CharacterEngine
    {
        private CharacterRecord Record
        {
            get;
            set;
        }

        private Network.Client Client
        {
            get;
            set;
        }

        public int Id => Record.Id;

        public string Name
        {
            get
            {
                return Record.Name;
            }
            set
            {
                Record.Name = value;
            }
        }

        public byte Level
        {
            get
            {
                return Record.Level;
            }
            set
            {
                Record.Level = value;
            }
        }

        public long Experience
        {
            get
            {
                return Record.Experience;
            }
            set
            {
                Record.Experience = value;
            }
        }

        public long ExperienceLevelFloor => Common.Data.Experience.GetExperienceLevelFloor(Record.Level).CharacterXp;

        public long ExperienceNextLevelFloor => Record.Level == 200 ? Record.Experience : Common.Data.Experience.GetNextExperienceLevelFloor(Record.Level).CharacterXp;

        public BreedEnum Breed
        {
            get
            {
                return Record.Breed;
            }
            set
            {
                Record.Breed = value;
            }
        }

        public string EntityLookString
        {
            get
            {
                return Record.EntityLookString;
            }
            set
            {
                Record.EntityLookString = value;
            }
        }

        public EntityLook EntityLook => Functions.BuildEntityLook(EntityLookString);

        public bool Sex
        {
            get
            {
                return Record.Sex;
            }
            set
            {
                Record.Sex = value;
            }
        }

        public int CurrentMapId
        {
            get
            {
                return Record.MapId;
            }
            set
            {
                Record.MapId = value;
            }

        }

        public Map Map => Map.Maps[CurrentMapId];

        public MapEngine CurrentMap => (MapEngine)Map.Maps[CurrentMapId].Instance;

        public short CellId
        {
            get
            {
                return Record.CellId;
            }
            set
            {
                Record.CellId = value;
            }
        }

        public DirectionsEnum Direction
        {
            get
            {
                return Record.Direction;
            }
            set
            {
                Record.Direction = value;
            }
        }

        public EntityDispositionInformations Disposition => new EntityDispositionInformations(CellId, (sbyte)Direction);

        public AlignmentSideEnum AlignmentSide
        {
            get
            {
                return Record.AlignementSide;
            }
            set
            {
                Record.AlignementSide = value;
            }
        }

        public sbyte AlignmentGrade => (sbyte)Common.Data.Experience.GetCharacterGrade(Honor);

        public ushort Honor
        {
            get
            {
                return Record.Honor;
            }
            set
            {
                Record.Honor = value;
            }
        }

        public ushort Dishonor
        {
            get
            {
                return Record.Dishonor;
            }
            set
            {
                Record.Dishonor = value;
            }
        }

        public bool PvPEnabled
        {
            get
            {
                return Record.PvPEnabled;
            }
            set
            {
                Record.PvPEnabled = value;
            }
        }

        public int Kamas
        {
            get
            {
                return Record.Kamas;
            }
            set
            {
                Record.Kamas = value;
            }
        }

        public int Health { get { return Record.Health; } set { Record.Health = value; } }
        public short Energy { get { return Record.Energy; } set { Record.Energy = value; } }

        public short StatsPoints
        {
            get
            {
                return Record.StatsPoints;
            }
            set
            {
                Record.StatsPoints = value;
            }
        }

        public short SpellsPoints
        {
            get
            {
                return Record.SpellsPoints;
            }
            set
            {
                Record.SpellsPoints = value;
            }
        }

        public DateTime? LastUsage
        {
            get
            {
                return Record.LastUsage;
            }
            set
            {
                Record.LastUsage = value;
            }
        }

        public CharacterEngine(CharacterRecord record, Network.Client client)
        {
            Record = record;
            Client = client;
        }

        public void Save()
        {
            //Record.Update();
        }

        public GameRolePlayCharacterInformations GetGameRolePlayCharacterInformations() => new GameRolePlayCharacterInformations(Id, EntityLook, Disposition, Name, new HumanInformations(new EntityLook[0], 0, 0, new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, false, false, false, false, false), 0), GetActorAlignmentInformations());

        public CharacterBaseInformations GetCharacterBaseInformations() => new CharacterBaseInformations(Id, Name, Level, EntityLook, (sbyte)Breed, Sex);

        public ActorAlignmentInformations GetActorAlignmentInformations() => new ActorAlignmentInformations((sbyte)AlignmentSide, 0, AlignmentGrade, 0);

        public ActorExtendedAlignmentInformations GetActorExtendedAlignmentInformations() => new ActorExtendedAlignmentInformations((sbyte)AlignmentSide, 0, AlignmentGrade, 0, Honor, Dishonor, PvPEnabled);
    }
}