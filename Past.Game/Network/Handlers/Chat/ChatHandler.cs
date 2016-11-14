using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.Chat
{
    public class ChatHandler
    {
        public static void HandleChatSmileyRequestMessage(Client client, ChatSmileyRequestMessage message)
        {
            client.Character.CurrentMap.Send(new ChatSmileyMessage(client.Character.Id, message.smileyId));
        }

        public static void SendEnabledChannelsMessage(Client client, sbyte[] channels)
        {
            client.Send(new EnabledChannelsMessage(channels, new sbyte[0]));
        }
    }
}
