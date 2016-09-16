using Past.Network.Game;
using Past.Protocol.Messages;

namespace Past.Network.Handlers.Game.Chat
{
    public class ChatHandler
    {
        public static void HandleChatSmileyRequestMessage(GameClient client, ChatSmileyRequestMessage message)
        {
            client.Character.Map.CurrentMap.Send(new ChatSmileyMessage(client.Character.Id, message.smileyId));
        }
    }
}
