using Past.Network.Game;
using Past.Network.Handlers.Game.Basic;
using Past.Protocol.Enums;
using Past.Protocol.Messages;

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
            client.Send(new GameMapMovementMessage(client.Character.Id, message.keyMovements));
        }

        public static void HandleGameMapMovementConfirmMessage(GameClient client, GameMapMovementConfirmMessage message)
        {
            BasicHandler.SendBasicNoOperationMessage(client);
        }
    }
}
