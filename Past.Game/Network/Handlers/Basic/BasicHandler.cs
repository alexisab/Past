using Past.Common.Utils;
using Past.Protocol.Messages;
using System;

namespace Past.Game.Network.Handlers.Basic
{
    public class BasicHandler
    {
        public static void SendBasicTimeMessage(Client client)
        {
            client.Send(new BasicTimeMessage(Functions.ReturnUnixTimeStamp(DateTime.Now), 3600));
        }

        public static void SendBasicNoOperationMessage(Client client)
        {
            client.Send(new BasicNoOperationMessage());
        }
    }
}
