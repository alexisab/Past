using Past.Game.Network.Handlers.Basic;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System;
using System.Linq;

namespace Past.Game.Network.Handlers.Context
{
    public class ContextHandler
    {
        public static void HandleGameContextCreateRequestMessage(GameClient client, GameContextCreateRequestMessage message)
        {
            SendGameContextDestroyMessage(client);
            SendGameContextCreateMessage(client, GameContextEnum.ROLE_PLAY);
            SendCurrentMapMessage(client, client.Character.CurrentMapId);
        }

        public static void HandleGameMapMovementRequestMessage(GameClient client, GameMapMovementRequestMessage message)
        {
            client.Character.CellId = (short)(message.keyMovements.Last() & 4095);
            client.Character.Direction = (DirectionsEnum)(message.keyMovements.Last() >> 12);
            client.Character.CurrentMap.Send(new GameMapMovementMessage(client.Character.Id, message.keyMovements));
        }

        public static void HandleGameMapMovementConfirmMessage(GameClient client, GameMapMovementConfirmMessage message)
        {
            BasicHandler.SendBasicNoOperationMessage(client);
        }

        public static void HandleGameMapChangeOrientationRequestMessage(GameClient client, GameMapChangeOrientationRequestMessage message)
        {
            if (Enum.IsDefined(typeof(DirectionsEnum), message.direction))
            {
                client.Character.Direction = (DirectionsEnum)message.direction;
                client.Character.CurrentMap.Send(new GameMapChangeOrientationMessage(client.Character.Id, message.direction));
            }
        }

        public static void SendGameContextCreateMessage(GameClient client, GameContextEnum context)
        {
            client.Send(new GameContextCreateMessage((sbyte)context));
        }

        public static void SendGameContextDestroyMessage(GameClient client)
        {
            client.Send(new GameContextDestroyMessage());
        }

        public static void SendCurrentMapMessage(GameClient client, int mapId)
        {
            client.Send(new CurrentMapMessage(mapId));
        }
    }
}
