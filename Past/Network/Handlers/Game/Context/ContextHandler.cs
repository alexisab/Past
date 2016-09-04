using Past.Network.Game;
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

            client.Send(new CurrentMapMessage(21891589));
        }
    }
}
