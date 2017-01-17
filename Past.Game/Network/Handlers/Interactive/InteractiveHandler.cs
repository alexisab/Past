using System;
using System.Collections.Generic;
using System.Linq;
using Past.Protocol.Enums;
using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Interactive
{
    public class InteractiveHandler //InteractiveUseRequestMessage
    {
        public static void HandleInteractiveUseRequestMessage(GameClient client, InteractiveUseRequestMessage message)
        {
            client.Character.CurrentMap.Send(new InteractiveUsedMessage(client.Character.Id, message.elemId, message.skillId, 0));
            client.Send(new InteractiveUseEndedMessage(message.elemId, message.skillId));
            var shit = from entry in Common.Data.Interactive.Interactives
                       join xxx in Common.Data.Map.Maps on entry.Key equals xxx.Id
                       select (short)xxx.SubAreaId;

            client.Send(new ZaapListMessage(0, Common.Data.Interactive.Interactives.Keys.ToArray(), shit.ToArray(), new short[Common.Data.Interactive.Interactives.Count], 2323));
           
        }
    }
}
