using Past.Common.Utils;
using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Authorized
{
    public class AuthorizedHandler
    {
        public static void HandleAdminQuietCommandMessage(GameClient client, AdminQuietCommandMessage message)
        {
            if(message.content.Contains("move"))
            {
                string[] pos = message.content.Remove(0, 7).Split(',');
                client.Send(new CurrentMapMessage(Functions.GetMapIdFromCoord(0, int.Parse(pos[0]), int.Parse(pos[1]))));
            }
        }
    }
}
