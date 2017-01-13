using Past.Protocol.Messages;
using Past.Protocol.Types;

namespace Past.Game.Network.Handlers.Friend
{
    public class FriendHandler
    {
        public static void HandleFriendsGetListMessage(GameClient client, FriendsGetListMessage message)
        {
            client.Send(new FriendsListMessage(new FriendInformations[0]));
        }

        public static void HandleIgnoredGetListMessage(GameClient client, IgnoredGetListMessage message)
        {
            client.Send(new IgnoredListMessage(new IgnoredInformations[0]));
        }
    }
}
