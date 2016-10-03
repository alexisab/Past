using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Login.Database;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System;
using System.Linq;

namespace Past.Login.Network.Handlers
{
    public class ConnectionHandler
    {
        public static void HandleIdentificationMessage(Client client, IdentificationMessage message)
        {
            if (message.version.ToString() != "2.0.0.0")
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, new Protocol.Types.Version(2, 0, 0, 0)));
            }
            AccountRecord account = Account.ReturnAccount(message.login);
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
                if (Server.Clients.Count(x => x.Account == account) > 1)
                {
                    Server.Clients.FirstOrDefault(x => x.Account == account).Disconnect();
                }
                client.Send(new IdentificationSuccessMessage(account.HasRights, false, account.Nickname, 0, account.SecretQuestion, 42195168000000));
            }
        }
    }
}
