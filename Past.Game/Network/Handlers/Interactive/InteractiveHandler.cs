using System;
using System.Collections.Generic;
using System.Linq;
using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Interactive
{
    public class InteractiveHandler
    {
        public static void HandleInteractiveUseRequestMessage(GameClient client, InteractiveUseRequestMessage message)
        {
            client.Character.CurrentMap.Send(new InteractiveUsedMessage(client.Character.Id, message.elemId, message.skillId, 0));
            switch (message.skillId)
            {
                case 44:
                    client.Character.SetSpawnMap(client.Character.Map.Id);
                    break;
                case 114:
                    IEnumerable<int> zaaps = from interactive in Common.Data.Interactive.Interactives
                        from interactiveElement in interactive.Value
                        where interactiveElement.enabledSkillIds.SequenceEqual(new short[] {44, 114})
                        select interactive.Key;
                    IEnumerable<short> zaapsSubAreaIds = from interactive in Common.Data.Interactive.Interactives
                        join map in Common.Data.Map.Maps on interactive.Key equals map.Id
                        select (short) map.SubAreaId;
                    client.Send(new ZaapListMessage(0, zaaps.ToArray(), zaapsSubAreaIds.ToArray(), new short[zaaps.Count()], client.Character.GetSpawnMap()));
                    break;
            }
            client.Send(new InteractiveUseEndedMessage(message.elemId, message.skillId));
        }

        public static void HandleTeleportRequestMessage(GameClient client, TeleportRequestMessage message)
        {
        }
    }
}
