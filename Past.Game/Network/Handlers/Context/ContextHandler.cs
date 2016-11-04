using Past.Protocol.Enums;
using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Context
{
    public class ContextHandler
    {
        public static void HandleGameContextCreateRequestMessage(Client client, GameContextCreateRequestMessage message)
        {
            SendGameContextDestroyMessage(client);
            SendGameContextCreateMessage(client, GameContextEnum.ROLE_PLAY);
            SendCurrentMapMessage(client, client.Character.CurrentMapId);
        }

        public static void SendGameContextCreateMessage(Client client, GameContextEnum context)
        {
            client.Send(new GameContextCreateMessage((sbyte)context));
        }

        public static void SendGameContextDestroyMessage(Client client)
        {
            client.Send(new GameContextDestroyMessage());
        }

        public static void SendCurrentMapMessage(Client client, int mapId)
        {
            client.Send(new CurrentMapMessage(mapId));
        }
    }
}
