using Past.Common.Data;
using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Game.Network.Handlers.Basic;
using Past.Protocol.Enums;
using Past.Protocol.Types;
using System;
using System.Collections.Generic;

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
                return Client.Account.HasRights == true ? $"[{Record.Name}]" : Record.Name;
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

        public Breed BreedData => Common.Data.Breed.Breeds[Breed];

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

        public short PvPActivationCost => (short)((Honor / 100) * 5);

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

        public int Health //Current
        {
            get
            {
                return Record.Health;
            }
            set
            {
                Record.Health = value;
            }
        }

        //public int BaseHealth => 50 + (Level * 5);
        public int MaxHealth => 50 + (Level * 5) + Stats.Total(StatEnum.VITALITY);

        public short Energy
        {
            get
            {
                return Record.Energy;
            }
            set
            {
                Record.Energy = value;
            }
        }

        public byte AP
        {
            get
            {
                return Record.AP;
            }
            set
            {
                Record.AP = value;
            }
        }

        public byte MP
        {
            get
            {
                return Record.MP;
            }
            set
            {
                Record.MP = value;
            }
        }

        public int Strength
        {
            get
            {
                return Record.Strength;
            }
            set
            {
                Record.Strength = value;
            }
        }

        public int Vitality
        {
            get
            {
                return Record.Vitality;
            }
            set
            {
                Record.Vitality = value;
            }
        }

        public int Wisdom
        {
            get
            {
                return Record.Wisdom;
            }
            set
            {
                Record.Wisdom = value;
            }
        }

        public int Chance
        {
            get
            {
                return Record.Chance;
            }
            set
            {
                Record.Chance = value;
            }
        }

        public int Agility
        {
            get
            {
                return Record.Agility;
            }
            set
            {
                Record.Agility = value;
            }
        }

        public int Intelligence
        {
            get
            {
                return Record.Intelligence;
            }
            set
            {
                Record.Intelligence = value;
            }
        }

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

        public StatsEngine Stats
        {
            get;
            set;
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

        public List<CharacterSpellRecord> Spells
        {
            get;
            set;
        }

        public CharacterEngine(CharacterRecord record, Network.Client client)
        {
            Record = record;
            Client = client;
            Stats = new StatsEngine(this);
            Spells = CharacterSpellRecord.ReturnCharacterSpells(Id);
        }

        public void Save()
        {
            Record.Update();
            Spells.ForEach(spell => spell.Update());
        }

        public void SendLoginMessage()
        {
            BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]);
            if (Client.Account.LastConnection.HasValue && !string.IsNullOrEmpty(Client.Account.LastIp))
            {
                BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 152, new string[] { $"{Client.Account.LastConnection.Value.Year}", $"{Client.Account.LastConnection.Value.Month}", $"{Client.Account.LastConnection.Value.Day}", $"{Client.Account.LastConnection.Value.Hour}", $"{Client.Account.LastConnection.Value.Minute}", Client.Account.LastIp });
            }
            BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 153, new string[] { Client.Ip });
            Client.Account.LastConnection = DateTime.Now;
            Client.Account.LastIp = Client.Ip;
            LastUsage = DateTime.Now;
            BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 0, new string[] { "Welcome to Past" });
        }

        public GameRolePlayCharacterInformations GetGameRolePlayCharacterInformations => new GameRolePlayCharacterInformations(Id, EntityLook, Disposition, Name, new HumanInformations(new EntityLook[0], 0, 0, new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, false, false, false, false, false), 0), GetActorAlignmentInformations);

        public CharacterBaseInformations GetCharacterBaseInformations => new CharacterBaseInformations(Id, Name, Level, EntityLook, (sbyte)Breed, Sex);

        public ActorAlignmentInformations GetActorAlignmentInformations => new ActorAlignmentInformations((sbyte)(PvPEnabled ? AlignmentSide : AlignmentSideEnum.ALIGNMENT_NEUTRAL), 0, AlignmentGrade, 0);

        public ActorExtendedAlignmentInformations GetActorExtendedAlignmentInformations => new ActorExtendedAlignmentInformations((sbyte)AlignmentSide, 0, AlignmentGrade, 0, Honor, Dishonor, PvPEnabled);

        public CharacterCharacteristicsInformations GetCharacterCharacteristicsInformations => new CharacterCharacteristicsInformations(Experience, ExperienceLevelFloor, ExperienceNextLevelFloor, Kamas, StatsPoints, SpellsPoints, GetActorExtendedAlignmentInformations, Health, MaxHealth, Energy, 10000, AP, MP, Stats[StatEnum.INITIATIVE], Stats[StatEnum.PROSPECTING], Stats[StatEnum.ACTION_POINTS], Stats[StatEnum.MOVEMENT_POINTS], Stats[StatEnum.STRENGTH], Stats[StatEnum.VITALITY], Stats[StatEnum.WISDOM], Stats[StatEnum.CHANCE], Stats[StatEnum.AGILITY], Stats[StatEnum.INTELLIGENCE], Stats[StatEnum.RANGE], Stats[StatEnum.SUMMONABLE_CREATURES_BOOST], Stats[StatEnum.REFLECT], Stats[StatEnum.CRITICAL_HIT], 0, Stats[StatEnum.CRITICAL_MISS], Stats[StatEnum.HEAL_BONUS], Stats[StatEnum.ALL_DAMAGES_BONUS], Stats[StatEnum.WEAPON_DAMAGES_BONUS_PERCENT], Stats[StatEnum.DAMAGES_BONUS_PERCENT], Stats[StatEnum.TRAP_BONUS], Stats[StatEnum.TRAP_BONUS_PERCENT], Stats[StatEnum.PERMANENT_DAMAGE_PERCENT], Stats[StatEnum.DODGE_PA_LOST_PROBABILITY], Stats[StatEnum.DODGE_PM_LOST_PROBABILITY], Stats[StatEnum.NEUTRAL_ELEMENT_REDUCTION], Stats[StatEnum.EARTH_ELEMENT_RESIST_PERCENT], Stats[StatEnum.WATER_ELEMENT_RESIST_PERCENT], Stats[StatEnum.AIR_ELEMENT_RESIST_PERCENT], Stats[StatEnum.FIRE_ELEMENT_RESIST_PERCENT], Stats[StatEnum.NEUTRAL_ELEMENT_REDUCTION], Stats[StatEnum.EARTH_ELEMENT_REDUCTION], Stats[StatEnum.WATER_ELEMENT_REDUCTION], Stats[StatEnum.AIR_ELEMENT_REDUCTION], Stats[StatEnum.FIRE_ELEMENT_REDUCTION], Stats[StatEnum.PVP_NEUTRAL_ELEMENT_RESIST_PERCENT], Stats[StatEnum.PVP_EARTH_ELEMENT_RESIST_PERCENT], Stats[StatEnum.PVP_WATER_ELEMENT_RESIST_PERCENT], Stats[StatEnum.PVP_AIR_ELEMENT_RESIST_PERCENT], Stats[StatEnum.PVP_FIRE_ELEMENT_RESIST_PERCENT], Stats[StatEnum.PVP_NEUTRAL_ELEMENT_REDUCTION], Stats[StatEnum.PVP_EARTH_ELEMENT_REDUCTION], Stats[StatEnum.PVP_WATER_ELEMENT_REDUCTION], Stats[StatEnum.PVP_AIR_ELEMENT_REDUCTION], Stats[StatEnum.PVP_FIRE_ELEMENT_REDUCTION], new CharacterSpellModification[0]);
    }
}