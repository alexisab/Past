using System.Collections.Generic;
using Past.Protocol.Enums;
using Past.Protocol.Types;

namespace Past.Common.Data
{
    public class Breed
    {
        public BreedEnum BreedId { get; set; }
        public int StartMapId { get; set; }
        public EntityDispositionInformations StartDisposition { get; set; }
        public string MaleLook { get; set; }
        public string FemaleLook { get; set; }
        public string StatsPointsForStrength { get; set; }
        public string StatsPointsForIntelligence { get; set; }
        public string StatsPointsForChance { get; set; }
        public string StatsPointsForAgility { get; set; }
        public string StatsPointsForVitality { get; set; }
        public string StatsPointsForWisdom { get; set; }
        public int[] BreedSpellsId { get; set; }
        public static List<Breed> Breeds = new List<Breed>();

        public Breed(BreedEnum breedId, int startMapId, EntityDispositionInformations startDisposition, string maleLook, string femaleLook, string statsPointsForStrength, string statsPointsForIntelligence, string statsPointsForChance, string statsPointsForAgility, string statsPointsForVitality, string statsPointsForWisdom, int[] breedSpellsId)
        {
            BreedId = breedId;
            StartMapId = startMapId;
            StartDisposition = startDisposition;
            MaleLook = maleLook;
            FemaleLook = femaleLook;
            StatsPointsForStrength = statsPointsForStrength;
            StatsPointsForIntelligence = statsPointsForIntelligence;
            StatsPointsForChance = statsPointsForChance;
            StatsPointsForAgility = statsPointsForAgility;
            StatsPointsForVitality = statsPointsForVitality;
            StatsPointsForWisdom = statsPointsForWisdom;
            BreedSpellsId = breedSpellsId;
        }

        public static void Initialize()
        {
            Breeds.Add(new Breed(BreedEnum.Feca, 21893123, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|10||125}", "{1|11||125}", "0,2|50,3|150,4|250,5", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 17, 6, 3, 4, 2, 1, 9, 18, 20, 14, 19, 5, 16, 8, 12, 11, 10, 7, 15, 13, 1901 }));
            Breeds.Add(new Breed(BreedEnum.Osamodas, 21891589, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|20||130}", "{1|21||125}", "0,2|50,3|150,4|250,5", "0,1|100,2|200,3|300,4|400,5", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 21, 23, 34, 26, 22, 35, 28, 37, 30, 27, 24, 33, 25, 38, 36, 32, 29, 39, 40, 31, 1902 }));
            Breeds.Add(new Breed(BreedEnum.Enutrof, 21893121, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|30||120}", "{1|31||95}", "0,1|50,2|150,3|250,4|350,5", "0,1|20,2|60,3|100,4|150,5", "0,1|100,2|150,3|230,4|330,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 51, 41, 43, 49, 42, 47, 48, 45, 53, 46, 52, 44, 50, 54, 55, 56, 58, 59, 57, 60, 1903 }));
            Breeds.Add(new Breed(BreedEnum.Sram, 21894663, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|40||140}", "{1|41||155}", "0,1|100,2|200,3|300,4|400,5", "0,2|50,3|150,4|250,5", "0,1|20,2|40,3|60,4|80,5", "0,1|100,2|200,3|300,4|400,5", "0,1", "0,3", new[] { 65, 61, 72, 66, 68, 63, 74, 64, 79, 78, 71, 62, 69, 77, 73, 67, 70, 75, 76, 80, 1904 }));
            Breeds.Add(new Breed(BreedEnum.Xelor, 21893127, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|50||110}", "{1|51||110}", "0,2|50,3|150,4|250,5", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 81, 82, 83, 84, 100, 92, 88, 93, 85, 96, 98, 86, 89, 90, 87, 94, 99, 95, 91, 97, 1905 }));
            Breeds.Add(new Breed(BreedEnum.Ecaflip, 21891841, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|60||150}", "{1|61||150}", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1|20,2|40,3|60,4|80,5", "0,1|50,2|100,3|150,4|200,5", "0,1", "0,3", new[] { 103, 102, 105, 109, 113, 111, 104, 119, 101, 107, 116, 106, 117, 108, 115, 118, 110, 112, 114, 120, 1906 }));
            Breeds.Add(new Breed(BreedEnum.Eniripsa, 21891585, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|70||110}", "{1|71||125}", "0,2|50,3|150,4|250,5", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 125, 121, 128, 124, 122, 126, 127, 123, 130, 131, 132, 133, 134, 135, 129, 136, 137, 138, 139, 140, 1907 }));
            Breeds.Add(new Breed(BreedEnum.Iop, 21891587, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|80||140}", "{1|81||140}", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1|20,2|40,3|60,4|80,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 143, 142, 141, 144, 145, 146, 147, 148, 154, 150, 151, 155, 152, 153, 149, 156, 157, 158, 160, 159, 1908 }));
            Breeds.Add(new Breed(BreedEnum.Cra, 21893377, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|90||140}", "{1|91||135}", "0,1|50,2|150,3|250,4|350,5", "0,1|50,2|150,3|250,4|350,5", "0,1|20,2|40,3|60,4|80,5", "0,1|50,2|100,3|150,4|200,5", "0,1", "0,3", new[] { 161, 169, 164, 163, 165, 172, 167, 168, 162, 170, 171, 166, 173, 174, 176, 175, 178, 177, 179, 180, 1909 }));
            Breeds.Add(new Breed(BreedEnum.Sadida, 21893125, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|100||150}", "{1|101||145}", "0,1|50,2|250,3|300,4|400,5", "0,1|100,2|200,3|300,4|400,5", "0,1|100,2|200,3|300,4|400,5", "0,1|20,2|40,3|60,4|80,5", "0,1", "0,3", new[] { 183, 193, 200, 198, 195, 182, 192, 197, 189, 181, 199, 191, 186, 196, 190, 194, 185, 184, 188, 187, 1910 }));
            Breeds.Add(new Breed(BreedEnum.Sacrieur, 21894913, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|110||145}", "{1|111||140}", "0,3|100,4|150,5", "0,3|100,4|150,5", "0,3|100,4|150,5", "0,3|100,4|150,5", "0,1,2", "", new[] { 434, 431, 432, 444, 449, 436, 437, 439, 433, 443, 440, 442, 441, 445, 438, 446, 447, 448, 435, 450, 1911 }));
            Breeds.Add(new Breed(BreedEnum.Pandawa, 21891591, new EntityDispositionInformations(242, (sbyte)DirectionsEnum.DIRECTION_SOUTH_EAST), "{1|120||160}", "{1|121||145}", "0,1|50,2|200,3", "0,1|50,2|200,3", "0,1|50,2|200,3", "0,1|50,2|200,3", "0,1", "0,3", new[] { 686, 692, 687, 689, 690, 691, 688, 693, 694, 695, 696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 1912 }));
        }
    }
}
