using Past.Network.Game;
using Past.Protocol.Messages;
using Past.Utils;
using System;

namespace Past.Network.Handlers.Game.Chat
{
    public class ChatHandler
    {

        public static void HandleChatClientMultiMessage(GameClient client, ChatClientMultiMessage message)
        {
            client.Character.Map.CurrentMap.Send(new ChatServerMessage(message.channel, message.content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", client.Character.Id, client.Character.Name));
        }

        public static void HandleChatSmileyRequestMessage(GameClient client, ChatSmileyRequestMessage message)
        {
            client.Character.Map.CurrentMap.Send(new ChatSmileyMessage(client.Character.Id, message.smileyId));
        }
    }
}
