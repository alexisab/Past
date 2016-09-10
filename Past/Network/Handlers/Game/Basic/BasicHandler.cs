using Past.Network.Game;
using Past.Protocol.Messages;
using Past.Utils;
using System;

namespace Past.Network.Handlers.Game.Basic
{
    public class BasicHandler
    {
        public static void SendBasicTimeMessage(GameClient client)
        {
            client.Send(new BasicTimeMessage(Functions.ReturnUnixTimeStamp(DateTime.Now), 3600));
        }

        public static void SendBasicNoOperationMessage(GameClient client)
        {
            client.Send(new BasicNoOperationMessage());
        }
    }
}
