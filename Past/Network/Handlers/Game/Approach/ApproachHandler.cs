using Past.Network.Game;
using Past.Protocol.Messages;

namespace Past.Network.Handlers.Game.Approach
{
    public class ApproachHandler
    {
        public static void HandleAuthenticationTicketMessage(GameClient client, AuthenticationTicketMessage message)
        {
            if (message.ticket != "")
            {
                var account = TransitionHelper.ReturnAccount(message.ticket);
                if (account != null)
                {
                    client.Account = account;
                    client.Account.Characters = Database.Character.ReturnCharacters(client.Account.Id);
                    client.Send(new AuthenticationTicketAcceptedMessage());
                }
                else
                    client.Send(new AuthenticationTicketRefusedMessage());
            }
            else
                client.Send(new AuthenticationTicketRefusedMessage());
        }
    }
}