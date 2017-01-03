using Past.Common.Utils;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System;
using System.Linq;

namespace Past.Game.Network.Handlers.Basic
{
    public class BasicHandler
    {
        public static void HandleBasicWhoIsRequestMessage(GameClient client, BasicWhoIsRequestMessage message)
        {
            GameClient targetClient = GameServer.Clients.FirstOrDefault(target => target.Character.Name == message.search);
            if (targetClient != null)
            {
                client.Send(new BasicWhoIsMessage(targetClient.Character.Name == client.Character.Name,
                    (sbyte) targetClient.Account.Role, targetClient.Account.Nickname, targetClient.Character.Name,
                    (short) targetClient.Character.Map.SubAreaId));
            }
            else
            {
                client.Send(new BasicWhoIsNoMatchMessage(message.search));
            }
        }

        public static void SendBasicTimeMessage(GameClient client)
        {
            client.Send(new BasicTimeMessage(Functions.ReturnUnixTimeStamp(DateTime.Now), 3600));
        }

        public static void SendBasicNoOperationMessage(GameClient client)
        {
            client.Send(new BasicNoOperationMessage());
        }

        public static void SendTextInformationMessage(GameClient client, TextInformationTypeEnum msgType, short msgId, string[] parameters)
        {
            client.Send(new TextInformationMessage((sbyte)msgType, msgId, parameters));
        }

        public static void SendSystemMessageDisplayMessage(GameClient client, bool hangUp, short msgId, string[] parameters)
        {
            client.Send(new SystemMessageDisplayMessage(hangUp, msgId, parameters));
        }
    }
}