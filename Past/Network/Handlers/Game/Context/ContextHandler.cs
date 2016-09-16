using Past.Network.Game;
using Past.Network.Handlers.Game.Basic;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System.Linq;

namespace Past.Network.Handlers.Game.Context
{
    public class ContextHandler
    {
        public static void HandleGameContextCreateRequestMessage(GameClient client, GameContextCreateRequestMessage message)
        {
            client.Send(new GameContextDestroyMessage());
            client.Send(new GameContextCreateMessage((sbyte)GameContextEnum.ROLE_PLAY));

            client.Send(new CurrentMapMessage(client.Character.Map.Id));
        }

        public static void HandleGameMapMovementRequestMessage(GameClient client, GameMapMovementRequestMessage message)
        {
            client.Character.CellId = (short)(message.keyMovements.Last() & 4095);
            client.Character.Direction = (DirectionsEnum)(message.keyMovements.Last() >> 12);
            client.Character.Map.CurrentMap.Send(new GameMapMovementMessage(client.Character.Id, message.keyMovements));
        }

        public static void HandleGameMapMovementConfirmMessage(GameClient client, GameMapMovementConfirmMessage message)
        {
            BasicHandler.SendBasicNoOperationMessage(client);
        }

        public static void HandleGameMapChangeOrientationRequestMessage(GameClient client, GameMapChangeOrientationRequestMessage message)
        {
            if (message.direction >= 0 && message.direction <= 7)
            {
                client.Character.Direction = (DirectionsEnum)message.direction;
                client.Character.Map.CurrentMap.Send(new GameMapChangeOrientationMessage(client.Character.Id, message.direction));
            }
        }
    }
}
