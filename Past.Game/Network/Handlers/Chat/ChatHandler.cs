using Past.Common.Utils;
using Past.Game.Engine;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System;
using System.Linq;

namespace Past.Game.Network.Handlers.Chat
{
    public class ChatHandler
    {
        public static void HandleChatClientMultiMessage(GameClient client, ChatClientMultiMessage message) //TODO
        {
            if (!message.content.StartsWith("."))
            {
                switch (message.channel)
                {
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_TEAM:
                        /*if (client.Character.Team != null)
                        {
                        }*/
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_GUILD:
                        /*if (client.Character.Guild != null)
                        {
                        }*/
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_ALIGN:
                        if (client.Character.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
                        {
                        }
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_PARTY:
                        client.Character.Party?.SendPartyChatServerMessage(message.content, client.Character.Id, client.Character.Name);
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_SALES:
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_SEEK:
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_NOOB:
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_ADMIN:
                        if (client.Account.Role == GameHierarchyEnum.ADMIN)
                        {
                            SendAdminChatServerMessage(client, message.content, client.Character.Id, client.Character.Name);
                        }
                        break;
                    default:
                        SendChatServerMessage(client, message.channel, message.content, client.Character.Id, client.Character.Name);
                        break;
                }
            }
            else
            {
                CommandEngine.Handle(client, message.content);
            }
        }

        public static void HandleChatClientPrivateMessage(GameClient client, ChatClientPrivateMessage message)
        {
            var targetClient = client.Server.Clients.FirstOrDefault(target => target.Character.Name == message.receiver);
            if (targetClient != null && targetClient != client)
            {
                targetClient.Send(new ChatServerMessage(9, message.content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", client.Character.Id, client.Character.Name));
            }
        }

        public static void HandleChatSmileyRequestMessage(GameClient client, ChatSmileyRequestMessage message)
        {
            client.Character.CurrentMap.Send(new ChatSmileyMessage(client.Character.Id, message.smileyId));
        }

        public static void SendChatServerMessage(GameClient client, sbyte channel, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content) && Enum.IsDefined(typeof(ChatChannelsMultiEnum), channel))
            {
                client.Character.CurrentMap.Send(new ChatServerMessage(channel, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
            }
        }

        public static void SendAdminChatServerMessage(GameClient client, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content))
            {
                foreach (var c in client.Server.Clients.Where(c => c.Character != null))
                {
                    c.Send(new ChatServerMessage(8, content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", senderId, senderName));
                }
            }
        }

        public static void SendEnabledChannelsMessage(GameClient client, sbyte[] channels)
        {
            client.Send(new EnabledChannelsMessage(channels, new sbyte[0]));
        }
    }
}
