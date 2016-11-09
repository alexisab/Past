using Past.Protocol.Enums;
using Past.Protocol.Types;
using System.Collections.Generic;

namespace Past.Game.Engine
{
    public class StatsEngine
    {
        public CharacterBaseCharacteristic this[StatsEnum @enum]
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

        private Dictionary<StatsEnum, CharacterBaseCharacteristic> Stats;

        public StatsEngine(CharacterEngine character)
        {
            Stats = new Dictionary<StatsEnum, CharacterBaseCharacteristic>();
            foreach (var @enum in typeof(StatsEnum).GetEnumValues())
            {
                Stats.Add((StatsEnum)@enum, new CharacterBaseCharacteristic());
            }
            this[StatsEnum.MOVEMENT_POINTS].@base = 3;
            this[StatsEnum.SUMMONABLE_CREATURES_BOOST].@base = 1;
            this[StatsEnum.ACTION_POINTS].@base = character.Level >= 100 ? (short)7 : (short)6;
            this[StatsEnum.STRENGTH].@base = (short)character.Strength;
            this[StatsEnum.VITALITY].@base = (short)character.Vitality;
            this[StatsEnum.WISDOM].@base = (short)character.Wisdom;
            this[StatsEnum.CHANCE].@base = (short)character.Chance;
            this[StatsEnum.AGILITY].@base = (short)character.Agility;
            this[StatsEnum.INTELLIGENCE].@base = (short)character.Intelligence;
            this[StatsEnum.PROSPECTING].@base = character.Breed == BreedEnum.Enutrof ? (short)120 : (short)100;
        }

        public short Total(StatsEnum @enum)
        {
            return (short)(this[@enum].@base + this[@enum].objectsAndMountBonus + this[@enum].alignGiftBonus + this[@enum].contextModif);
        }
    }
}
