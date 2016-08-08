using Past.Network.Game;
using Past.Protocol.Messages;

namespace Past.Network.Handlers.Game.Approach
{
    public class ApproachHandler
    {
        [MessageHandler(110)]
        public static void HandleAuthenticationTicketMessage(GameClient client, AuthenticationTicketMessage message)
        {
            client.Send(new AuthenticationTicketAcceptedMessage());
            //ConsoleUtils.Write(ConsoleUtils.type.DEBUG, "Ticket {0} ...", message.ticket);
        }
    }
}