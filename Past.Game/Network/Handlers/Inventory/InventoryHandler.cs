using Past.Protocol.Messages;
using Past.Protocol.Types;

namespace Past.Game.Network.Handlers.Inventory
{
    public class InventoryHandler
    {
        public static void SendInventoryContentMessage(GameClient client, ObjectItem[] objects, int kamas)
        {
            client.Send(new InventoryContentMessage(objects, kamas));
        }

        public static void SendInventoryWeightMessage(GameClient client, int weight, int weightMax)
        {
            client.Send(new InventoryWeightMessage(weight, weightMax));
        }

        public static void SendSpellListMessage(GameClient client)
        {
            client.Send(new SpellListMessage(true, client.Character.Spells.ConvertAll(spell => new SpellItem(spell.Position, spell.SpellId, spell.Level)).ToArray()));
        }
    }
}
