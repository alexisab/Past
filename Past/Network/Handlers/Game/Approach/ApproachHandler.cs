using Past.Network.Game;
using Past.Network.Handlers.Game.Basic;
using Past.Protocol.Messages;
using System;
using System.Linq;

namespace Past.Network.Handlers.Game.Approach
{
    public class ApproachHandler
    {
        public static void HandleAuthenticationTicketMessage(GameClient client, AuthenticationTicketMessage message)
        {
            if (message.ticket != string.Empty)
            {
                var account = TransitionHelper.ReturnAccount(message.ticket);
                if (account != null)
                {
                    client.Account = account;
                    if (GameServer.Clients.Count(x => x.Account.Login == account.Login) > 1)
                        GameServer.Clients.First(x => x.Account.Login == account.Login).Disconnect();
                    client.Send(new AuthenticationTicketAcceptedMessage());
                    BasicHandler.SendBasicTimeMessage(client);
                }
                else
                    client.Send(new AuthenticationTicketRefusedMessage());
            }
            else
                client.Send(new AuthenticationTicketRefusedMessage());
        }
    }
}