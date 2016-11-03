using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Context.Roleplay
{
    public class RoleplayHandler
    {
        public static void SendEmoteListMessage(Client client, sbyte[] emoteIds)
        {
            client.Send(new EmoteListMessage(emoteIds));
        }
    }
}
