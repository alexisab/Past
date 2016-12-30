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
            base.OnCreate();
            base.OnDisconnect += OnDisconnect;
            Ticket = Functions.RandomString(32, true);
            Send(new ProtocolRequired(1165, 1165));
            Send(new HelloConnectMessage(Ticket));
        }

        public override void OnReceive(byte[] data)
        {
            using (BigEndianReader reader = new BigEndianReader(data))
            {
                MessagePart messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    dynamic message = MessageReceiver.BuildMessage((uint)messagePart.Id, reader);
                    ConsoleUtils.Write(ConsoleUtils.Type.RECEIV, $"{message} Id {messagePart.Id} Length {messagePart.Length} ...");
                    MessageHandlerManager<LoginClient>.InvokeHandler(this, message);
                }
            }
        }

        public new void OnDisconnect()
        {
            //Account?.Update();
            Account = null;
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Client {Ip}:{Port} disconnected from login server ...");
        }
    }
}
