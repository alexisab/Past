using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Past.Common.Network;
using Past.Common.Utils;
using Past.Protocol;
using Past.Protocol.IO;
using Past.Protocol.Messages;

namespace Past.Login.Network
{
    public class LoginClient : AbstractClient<LoginClient, LoginServer>
    {
        public override void OnCreate()
        {
            Send(new ProtocolRequired(1165, 1165));
            Send(new HelloConnectMessage(Ticket));
        }

        public override void OnReceive(byte[] data)
        {
            base.OnReceive(data);

            using (var reader = new BigEndianReader(data))
            {
                var messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    dynamic message = MessageReceiver.BuildMessage((uint)messagePart.Id, reader);
                    if (Config.Debug)
                    {
                        ConsoleUtils.Write(ConsoleUtils.Type.RECEIV, $"{message} Id {messagePart.Id} Length {messagePart.Length} ...");
                    }
                    MessageHandlerManager<LoginClient>.InvokeHandler(this, message);
                }
            }
        }
    }
}
