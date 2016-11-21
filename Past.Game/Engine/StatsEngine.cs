using Past.Protocol.Enums;
using Past.Protocol.Types;
using System;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class StatsEngine
    {
        public CharacterBaseCharacteristic this[StatEnum @enum]
        {
            get
            {
                if (Stats.ContainsKey(@enum))
                {
                    return Stats[@enum];
                }
                return new CharacterBaseCharacteristic();
            }
        }

        private CharacterEngine Character { get; set; }
        private Dictionary<StatEnum, CharacterBaseCharacteristic> Stats;

        public StatsEngine(CharacterEngine character)
        {
            Character = character;
            Stats = new Dictionary<StatEnum, CharacterBaseCharacteristic>();
            foreach (var @enum in typeof(StatEnum).GetEnumValues())
            {
                Stats.Add((StatEnum)@enum, new CharacterBaseCharacteristic());
            }
            this[StatEnum.ACTION_POINTS].@base = character.Level >= 100 ? (short)7 : (short)6;
            this[StatEnum.MOVEMENT_POINTS].@base = 3;
            this[StatEnum.SUMMONABLE_CREATURES_BOOST].@base = 1;
            this[StatEnum.STRENGTH].@base = (short)character.Strength;
            this[StatEnum.VITALITY].@base = (short)character.Vitality;
            this[StatEnum.WISDOM].@base = (short)character.Wisdom;
            this[StatEnum.CHANCE].@base = (short)character.Chance;
            this[StatEnum.AGILITY].@base = (short)character.Agility;
            this[StatEnum.INTELLIGENCE].@base = (short)character.Intelligence;
            Refresh();
        }

        public void Refresh()
        {
            this[StatEnum.INITIATIVE].@base = (short)((this[StatEnum.STRENGTH].@base + this[StatEnum.CHANCE].@base + this[StatEnum.AGILITY].@base + this[StatEnum.INTELLIGENCE].@base) * (Character.Health / (50 + (Character.Level * 5) + Total(StatEnum.VITALITY))));
            short baseProspection = Character.Breed == BreedEnum.Enutrof ? (short)120 : (short)100;
            this[StatEnum.PROSPECTING].@base = (short)(baseProspection + (this[StatEnum.CHANCE].@base / 10));
            this[StatEnum.DODGE_PA_LOST_PROBABILITY].@base = (short)(this[StatEnum.WISDOM].@base / 10);
            this[StatEnum.DODGE_PM_LOST_PROBABILITY].@base = (short)(this[StatEnum.WISDOM].@base / 10);
        }

        public short Total(StatEnum @enum)
        {
            return (short)(this[@enum].@base + this[@enum].objectsAndMountBonus + this[@enum].alignGiftBonus + this[@enum].contextModif);
        }
    }
}

