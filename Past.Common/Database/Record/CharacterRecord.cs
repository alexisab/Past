using MySql.Data.MySqlClient;
using Past.Protocol.Enums;
using System;

namespace Past.Common.Database.Record
{
    public class CharacterRecord
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long Experience { get; set; }
        public BreedEnum Breed { get; set; }
        public string EntityLookString { get; set; }
        public bool Sex { get; set; }
        public int MapId { get; set; }
        public short CellId { get; set; }
        public DirectionsEnum Direction { get; set; }
        public AlignmentSideEnum AlignementSide { get; set; }
        public ushort Honor { get; set; }
        public ushort Dishonor { get; set; }
        public bool PvPEnabled { get; set; }
        public int Kamas { get; set; }
        public short StatsPoints { get; set; }
        public short SpellsPoints { get; set; }
        public DateTime? LastUsage { get; set; }

        public CharacterRecord(MySqlDataReader reader)
        {
            Id = (int)reader["Id"];
            OwnerId = (int)reader["OwnerId"];
            Name = (string)reader["Name"];
            Level = (byte)reader["Level"];
            Experience = (long)reader["Experience"];
            Breed = (BreedEnum)reader["Breed"];
            EntityLookString = (string)reader["EntityLookString"];
            Sex = (bool)reader["Sex"];
            MapId = (int)reader["MapId"];
            CellId = (short)reader["CellId"];
            Direction = (DirectionsEnum)reader["Direction"];
            AlignementSide = (AlignmentSideEnum)reader["AlignementSide"];
            Honor = (ushort)reader["Honor"];
            Dishonor = (ushort)reader["Dishonor"];
            PvPEnabled = (bool)reader["PvPEnabled"];
            Kamas = (int)reader["Kamas"];
            StatsPoints = (short)reader["StatsPoints"];
            SpellsPoints = (short)reader["SpellsPoints"];
            LastUsage = reader["LastUsage"] as DateTime?;
        }
    }
}
