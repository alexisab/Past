using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using System;
using System.Linq;

namespace Past.Login.Network.Handlers
{
    public class ConnectionHandler
    {
        public static void HandleIdentificationMessage(LoginClient client, IdentificationMessage message)
        {
            Login(client, message);
        }

        public static void HandleIdentificationMessageWithServerIdMessage(LoginClient client, IdentificationMessageWithServerIdMessage message)
        {
            LoginWithServerId(client, message);
        }

        public static void HandleServerSelectionMessage(LoginClient client, ServerSelectionMessage message)
        {
            SendSelectedServerDataMessage(client, message.serverId);
        }

        public static void SendSelectedServerDataMessage(LoginClient client, short serverId)
        {
            client.Account.Ticket = client.Ticket;
            client.Account.Update();
            client.Send(new SelectedServerDataMessage(serverId, "127.0.0.1", 5555, true, client.Ticket));
            client.Disconnect();
        }

        public static void Login(LoginClient client, IdentificationMessage message)
        {
            if (message.version.ToString() != "2.0.0.0")
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, new Protocol.Types.Version(2, 0, 0, 0)));
            }
            AccountRecord account = AccountRecord.ReturnAccount(message.login);
            if (account == null || Functions.CipherPassword(account.Password, client.Ticket) != message.password)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
            }
            else
            {
                if (account.BannedUntil > DateTime.Now)
                {
                    client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                }
                if (LoginServer.Clients.Count(x => x.Account == account) > 1)
                {
                    LoginServer.Clients.FirstOrDefault(x => x.Account == account)?.Disconnect();
                }
                client.Account = account;
                client.Send(new IdentificationSuccessMessage(account.HasRights, false, account.Nickname, 0, account.SecretQuestion, 42195168000000));
                client.Send(new ServersListMessage(new[]
                {
                    new GameServerInformations(111, (sbyte)ServerStatusEnum.ONLINE, 0, true, (sbyte)CharacterRecord.ReturnCharacters(account.Id).Count())
                }));
            }
        }

        public static void LoginWithServerId(LoginClient client, IdentificationMessageWithServerIdMessage message)
        {
            if (message.version.ToString() != "2.0.0.0")
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, new Protocol.Types.Version(2, 0, 0, 0)));
            }
            AccountRecord account = AccountRecord.ReturnAccount(message.login);
            if (account == null || Functions.CipherPassword(account.Password, client.Ticket) != message.password)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
            }
            else
            {
                if (account.BannedUntil > DateTime.Now)
                {
                    client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                }
                if (LoginServer.Clients.Count(x => x.Account == account) > 1)
                {
                    LoginServer.Clients.FirstOrDefault(x => x.Account == account)?.Disconnect();
                }
                client.Account = account;
                client.Send(new IdentificationSuccessMessage(account.HasRights, false, account.Nickname, 0, account.SecretQuestion, 42195168000000));
                SendSelectedServerDataMessage(client, message.serverId);
            }
        }
    }
}
