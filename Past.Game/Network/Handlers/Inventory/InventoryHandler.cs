using Past.Protocol.Messages;
using Past.Protocol.Types;
using System.Collections.Generic;

namespace Past.Game.Network.Handlers.Inventory
{
    public class InventoryHandler
    {
        public static void SendInventoryContentMessage(Client client, ObjectItem[] objects, int kamas)
        {
            client.Send(new InventoryContentMessage(objects, kamas));
        }

        public static void SendInventoryWeightMessage(Client client, int weight, int weightMax)
        {
            client.Send(new InventoryWeightMessage(weight, weightMax));
        }

        public static void SendSpellListMessage(Client client, List<SpellItem> spells)
        {
            client.Send(new SpellListMessage(true, spells.ToArray()));
        }
    }
}
