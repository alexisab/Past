using Past.Common.Utils;
using Past.Game.Engine;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System;
using System.Linq;
using Past.Game.Network.Handlers.Basic;

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
                            SendAlignChatServerMessage(client, message.content, client.Character.Id, client.Character.Name);
                        }
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_PARTY:
                        client.Character.Party?.SendPartyChatServerMessage(message.content, client.Character.Id, client.Character.Name);
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_SALES:
                        SendSalesChatServerMessage(client, message.content, client.Character.Id, client.Character.Name);
                        break;
                    case (sbyte)ChatChannelsMultiEnum.CHANNEL_SEEK:
                        SendSeekChatServerMessage(client, message.content, client.Character.Id, client.Character.Name);
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
            GameClient targetClient = GameServer.Clients.FirstOrDefault(target => target.Character.Name == message.receiver);
            if (targetClient != null && targetClient != client)
            {
                SendChatServerMessage(targetClient, 9, message.content, client.Character.Id, client.Character.Name);
            }
            else
            {
                BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 211, new string[0]);
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

        #region Send Chat
        public static void SendSalesChatServerMessage(GameClient client, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content))
            {
                foreach (GameClient gameClient in GameServer.Clients.Where(gameClient => gameClient.Character != null && gameClient.Character.Map.SubAreaId == client.Character.Map.SubAreaId))
                {
                    SendChatServerMessage(gameClient, 5, content, senderId, senderName);
                }
            }
        }

        public static void SendSeekChatServerMessage(GameClient client, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content))
            {
                foreach (GameClient gameClient in GameServer.Clients.Where(gameClient => gameClient.Character != null && gameClient.Character.Map.SubAreaId == client.Character.Map.SubAreaId))
                {
                    SendChatServerMessage(gameClient, 6, content, senderId, senderName);
                }
            }
        }

        public static void SendAlignChatServerMessage(GameClient client, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content))
            {
                foreach (GameClient gameClient in GameServer.Clients.Where(gameClient => gameClient.Character != null && gameClient.Character.AlignmentSide == client.Character.AlignmentSide))
                {
                    SendChatServerMessage(gameClient, 3, content, senderId, senderName);
                }
            }
        }

        public static void SendAdminChatServerMessage(GameClient client, string content, int senderId, string senderName)
        {
            if (!string.IsNullOrEmpty(content))
            {
                foreach (GameClient gameClient in GameServer.Clients.Where(gameClient => gameClient.Character != null))
                {
                    SendChatServerMessage(gameClient, 8, content, senderId, senderName);
                }
            }
        }
        #endregion

        public static void SendEnabledChannelsMessage(GameClient client, sbyte[] channels)
        {
            client.Send(new EnabledChannelsMessage(channels, new sbyte[0]));
        }
    }
}
