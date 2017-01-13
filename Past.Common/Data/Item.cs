using System.Collections.Generic;

namespace Past.Common.Data
{
    public class Item
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int DescriptionId { get; set; }
        public int IconId { get; set; }
        public int Level { get; set; }
        public int Weight { get; set; }
        public bool Cursed { get; set; }
        public int UseAnimationId { get; set; }
        public bool Usable { get; set; }
        public bool Targetable { get; set; }
        public int Price { get; set; }
        public bool TwoHanded { get; set; }
        public bool Etheral { get; set; }
        public int ItemSetId { get; set; }
        public string Criteria { get; set; }
        public int AppearanceId { get; set; }
        public string Effects { get; set; }
        public int[] RecipeIds { get; set; }
        public static List<Item> Items = new List<Item>();
        
        public Item(int id, int nameId, string name, int typeId, int descriptionId, int iconId, int level, int weight, bool cursed, int useAnimationId, bool usable, bool targetable, int price, bool twoHanded, bool etheral, int itemSetId, string criteria, int appearanceId, string effects, int[] recipesIds)
        {
            Id = id;
            NameId = nameId;
            Name = name;
            TypeId = typeId;
            DescriptionId = descriptionId;
            IconId = iconId;
            Level = level;
            Weight = weight;
            Cursed = cursed;
            UseAnimationId = useAnimationId;
            Usable = usable;
            Targetable = targetable;
            Price = price;
            TwoHanded = twoHanded;
            Etheral = etheral;
            ItemSetId = itemSetId;
            Criteria = criteria;
            AppearanceId = appearanceId;
            Effects = effects;
            RecipeIds = recipesIds;
        }

        public static void Initialize()
        {
            
        }
    }
}
