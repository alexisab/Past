using Past.Protocol.Enums;
using Past.Protocol.Types;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class StatEngine
    {
        public CharacterBaseCharacteristic this[StatEnum @enum] => _stats.ContainsKey(@enum) ? _stats[@enum] : new CharacterBaseCharacteristic();
        private CharacterEngine Character { get; set; }
        private readonly Dictionary<StatEnum, CharacterBaseCharacteristic> _stats;

        public StatEngine(CharacterEngine character)
        {
            Character = character;
            _stats = new Dictionary<StatEnum, CharacterBaseCharacteristic>();
            foreach (var @enum in typeof(StatEnum).GetEnumValues())
            {
                _stats.Add((StatEnum)@enum, new CharacterBaseCharacteristic());
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

