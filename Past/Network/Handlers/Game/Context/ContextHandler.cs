using Past.Network.Game;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using Past.Utils;

namespace Past.Network.Handlers.Game.Character
{
    public class ContextHandler
    {
        public static void HandleGameContextCreateRequestMessage(GameClient client, GameContextCreateRequestMessage message)
        {
            client.Send(new GameContextDestroyMessage());
            client.Send(new GameContextCreateMessage((sbyte)GameContextEnum.ROLE_PLAY));
        }
    }
}
