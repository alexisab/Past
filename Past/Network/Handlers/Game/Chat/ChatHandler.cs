using Past.Network.Game;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Utils;
using System;
using System.Linq;

namespace Past.Network.Handlers.Game.Chat
{
    public class ChatHandler
    {

        public static void HandleChatClientMultiMessage(GameClient client, ChatClientMultiMessage message)
        {
            SendChatServerMessage(client, message.channel, message.content, client.Character.Id, client.Character.Name);
        }

        public static void HandleChatSmileyRequestMessage(GameClient client, ChatSmileyRequestMessage message)
        {
            if (message.smileyId >= 0 && message.smileyId <= 24)
                client.Character.Map.CurrentMap.Send(new ChatSmileyMessage(client.Character.Id, message.smileyId));
        }

        public static void HandleChatClientPrivateMessage(GameClient client, ChatClientPrivateMessage message)
        {
            var targetClient = GameServer.Clients.FirstOrDefault(x => x.Character.Name == message.receiver);
            if (!string.IsNullOrEmpty(message.content))
            {
                if (targetClient != null && targetClient != client)
                    targetClient.Send(new ChatServerMessage((sbyte)ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", client.Character.Id, client.Character.Name));
            }
        }

        public static void SendChatServerMessage(GameClient client, sbyte channel, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content))
            {
                if (channel >= 0 && channel <= 11)
                    client.Character.Map.CurrentMap.Send(new ChatServerMessage(channel, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
            }
        }
    }
}
