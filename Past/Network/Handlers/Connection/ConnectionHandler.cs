using Past.Database;
using Past.Network.Login;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using Past.Utils;
using System;
using System.Linq;

namespace Past.Network.Handlers.Connection
{
    public class ConnectionHandler
    {
        [MessageHandler(4)]
        public static void HandleIdentificationMessage(LoginClient client, IdentificationMessage message)
        {
            Account account = DatabaseManager.ReturnAccount(message.login);
            if (account == null)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.UNKNOWN_AUTH_ERROR));
                return;
            }
            string version = message.version.major + "." + message.version.minor + "." + message.version.revision + "." + message.version.buildType;
            if (version != "2.0.0.0")
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, new Protocol.Types.Version(2, 0, 0, 0)));
                return;
            }
            if (account.BannedUntil > DateTime.Now)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                return;
            }
            if (Functions.CipherString(account.Password, client.Ticket) != message.password)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
                return;
            }
            else
            {
                client.Account = account;
                if (client.Account.Nickname == string.Empty)
                {
                    client.Send(new NicknameRegistrationMessage());
                    return;
                }
                if (LoginServer.Clients.Count(x => x.Account.Login == account.Login) > 1)
                {
                    LoginServer.Clients.First(x => x.Account.Login == account.Login).Disconnect();
                }
                client.Send(new IdentificationSuccessMessage(account.HasRights, false, account.Nickname, 0, account.SecretQuestion, 42195168000000));
                client.Send(new ServersListMessage(new GameServerInformations[]
                {
                    new GameServerInformations(111, 3, 0, true, 0),
                }));
            }
        }

        [MessageHandler(6104)]
        public static void HandleIdentificationMessageWithServerIdMessage(LoginClient client, IdentificationMessageWithServerIdMessage message)
        {

        }

        [MessageHandler(40)]
        public static void HandleServerSelectionMessage(LoginClient client, ServerSelectionMessage message)
        {
            client.Send(new SelectedServerDataMessage(message.serverId, "127.0.0.1", 5555, true, client.Ticket));
            client.Disconnect();
        }

        [MessageHandler(5639)]
        public static void HandleNicknameChoiceRequestMessage(LoginClient client, NicknameChoiceRequestMessage message)
        {
            
        }
    }
}
