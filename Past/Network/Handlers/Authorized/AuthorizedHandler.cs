using Past.Network.Game;
using Past.Protocol.Messages;

namespace Past.Network.Handlers.Authorized
{
    public class AuthorizedHandler
    {
        public static void HandleAdminQuietCommandMessage(GameClient client, AdminQuietCommandMessage message)
        {
            if (message.content.Contains("move"))
            {
                var pos = message.content.Remove(0, 7).Split(',');
                int mapId = Database.Map.GetMapIdFromCoord(0, int.Parse(pos[0]), int.Parse(pos[1]));
                client.Character.MapId = mapId;
                client.Character.Map.CurrentMap.RemoveClient(client);
                client.Send(new CurrentMapMessage(mapId));
            }
        }
    }
}
