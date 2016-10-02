using Past.Common.Utils;
using Past.Protocol.Messages;

namespace Past.Login.Network.Handlers
{
    public class ConnectionHandler
    {
        public static void HandleIdentificationMessage(Client client, IdentificationMessage message)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.DEBUG, $"{message.login}");
        }
    }
}
