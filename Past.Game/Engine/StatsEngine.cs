using Past.Protocol.Enums;
using Past.Protocol.Types;
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

        private Dictionary<StatEnum, CharacterBaseCharacteristic> Stats;

        public StatsEngine(CharacterEngine character)
        {
            Stats = new Dictionary<StatEnum, CharacterBaseCharacteristic>();
            foreach (var @enum in typeof(StatEnum).GetEnumValues())
            {
                Stats.Add((StatEnum)@enum, new CharacterBaseCharacteristic());
            }
            this[StatEnum.MOVEMENT_POINTS].@base = 3;
            this[StatEnum.SUMMONABLE_CREATURES_BOOST].@base = 1;
            this[StatEnum.ACTION_POINTS].@base = character.Level >= 100 ? (short)7 : (short)6;
            this[StatEnum.STRENGTH].@base = (short)character.Strength;
            this[StatEnum.VITALITY].@base = (short)character.Vitality;
            this[StatEnum.WISDOM].@base = (short)character.Wisdom;
            this[StatEnum.CHANCE].@base = (short)character.Chance;
            this[StatEnum.AGILITY].@base = (short)character.Agility;
            this[StatEnum.INTELLIGENCE].@base = (short)character.Intelligence;
            this[StatEnum.PROSPECTING].@base = character.Breed == BreedEnum.Enutrof ? (short)120 : (short)100;
            int MaxHealth = 50 + (character.Level * 5) + Total(StatEnum.VITALITY);
            this[StatEnum.INITIATIVE].objectsAndMountBonus = (short)(Total(StatEnum.STRENGTH) + Total(StatEnum.CHANCE) + Total(StatEnum.AGILITY) + Total(StatEnum.INTELLIGENCE) + Total(StatEnum.INITIATIVE) * (character.Health / MaxHealth));

    }

    public short Total(StatEnum @enum)
        {
            return (short)(this[@enum].@base + this[@enum].objectsAndMountBonus + this[@enum].alignGiftBonus + this[@enum].contextModif);
        }
    }
}
