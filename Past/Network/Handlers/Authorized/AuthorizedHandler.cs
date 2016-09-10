using Past.Network.Game;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Utils;
using System;

namespace Past.Network.Handlers.Authorized
{
    public class AuthorizedHandler
    {
        public static void HandleAdminQuietCommandMessage(GameClient client, AdminQuietCommandMessage message)
        {
            client.Send(new ChatServerMessage((sbyte)ChatActivableChannelsEnum.CHANNEL_ADMIN, message.content, Functions.ReturnUnixTimeStamp(DateTime.Now), "", client.Character.Id, client.Character.Name));
        }
    }
}
