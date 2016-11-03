using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Chat
{
    public class ChatHandler
    {
        public static void SendEnabledChannelsMessage(Client client, sbyte[] channels)
        {
            client.Send(new EnabledChannelsMessage(channels, new sbyte[0]));
        }
    }
}
