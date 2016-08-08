using Past.Network.Login;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;

namespace Past.Network.Handlers.Connection
{
    public class ConnectionHandler
    {
        [MessageHandler(4)]
        public static void HandleIdentificationMessage(LoginClient client, IdentificationMessage message)
        {
            string version = message.version.major + "." + message.version.minor + "." + message.version.revision + "." + message.version.buildType;
            if (version != "2.0.0.0")
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, new Version(2, 0, 0, 0)));
            }
            client.Send(new IdentificationSuccessMessage(true, false, "admin", 0, "Delete ?", 42195168000000));
            client.Send(new ServersListMessage(new GameServerInformations[]
            {
                new GameServerInformations(111, 3, 0, true, 0),
            }));
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
