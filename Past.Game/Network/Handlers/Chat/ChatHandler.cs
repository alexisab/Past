using Past.Common.Utils;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System;

namespace Past.Game.Network.Handlers.Chat
{
    public class ChatHandler
    {
        public static void HandleChatClientMultiMessage(Client client, ChatClientMultiMessage message) //need to add the condition for certain channel
        {
            SendChatServerMessage(client, message.channel, message.content, client.Character.Id, client.Character.Name);
        }

        public static void HandleChatSmileyRequestMessage(Client client, ChatSmileyRequestMessage message)
        {
            client.Character.CurrentMap.Send(new ChatSmileyMessage(client.Character.Id, message.smileyId));
        }

        public static void SendChatServerMessage(Client client, sbyte channel, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content) && Enum.IsDefined(typeof(ChatChannelsMultiEnum), channel))
            {
                client.Character.CurrentMap.Send(new ChatServerMessage(channel, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
            }
        }

        public static void SendEnabledChannelsMessage(Client client, sbyte[] channels)
        {
            client.Send(new EnabledChannelsMessage(channels, new sbyte[0]));
        }
    }
}
