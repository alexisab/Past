using Past.Common.Database.Record;
using Past.Game.Network.Handlers.Basic;
using Past.Protocol.Messages;
using System.Linq;

namespace Past.Game.Network.Handlers.Approach
{
    public class ApproachHandler
    {
        public static void HandleAuthenticationTicketMessage(GameClient client, AuthenticationTicketMessage message)
        {
            if (message.ticket != string.Empty)
            {
                AccountRecord account = AccountRecord.ReturnAccountWithTicket(message.ticket);
                if (account != null)
                {
                    client.Account = account;
                    if (GameServer.Clients.Count(x => x.Account.Login == account.Login) > 1)
                    {
                        GameServer.Clients.First(x => x.Account.Login == account.Login).Disconnect();
                    }
                    client.Account.Characters = CharacterRecord.ReturnCharacters(account.Id);
                    client.Send(new AuthenticationTicketAcceptedMessage());
                    BasicHandler.SendBasicTimeMessage(client);
                }
                else
                {
                    client.Send(new AuthenticationTicketRefusedMessage());
                }
            }
            else
            {
                client.Send(new AuthenticationTicketRefusedMessage());
            }
        }
    }
}
