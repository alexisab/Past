using Past.Network.Game;
using Past.Protocol.Messages;

namespace Past.Network.Handlers.Authorized
{
    public class AuthorizedHandler
    {
        public static void HandleAdminQuietCommandMessage(GameClient client, AdminQuietCommandMessage message)
        {
            client.Send(new ChatServerMessage(1, message.content, 0, "", client.Character.Id, client.Character.Name));
        }
    }
}
