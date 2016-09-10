using Past.Database;
using Past.Network.Handlers;
using Past.Protocol;
using Past.Protocol.IO;
using Past.Protocol.Messages;
using Past.Utils;
using System;

namespace Past.Network.Login
{
    public class LoginClient
    {
        private Client Login { get; set; }
        public string Ticket { get; set; }
        public Account Account { get; set; }

        public LoginClient(Client client)
        {
            Login = client;
            Login_OnClientSocketConnected();
            Login.OnClientSocketClosed += Login_OnClientSocketClosed;
            Login.OnClientReceivedData += Login_OnClientReceivedData;
        }

        private void Login_OnClientSocketConnected()
        {
            Ticket = Functions.RandomString(32, true);
            Send(new ProtocolRequired(1165, 1165));
            Send(new HelloConnectMessage(Ticket));
        }

        private void Login_OnClientSocketClosed()
        {
            Disconnect();
            ConsoleUtils.Write(ConsoleUtils.type.INFO, "Client {0}:{1} disconnected from LoginServer ...", Login.Ip, Login.Port);
        }

        public void Disconnect()
        {
            LoginServer.Clients.Remove(this);
            Account = null;
            Login.Close();
        }

        private void Login_OnClientReceivedData(byte[] data)
        {
            using(BigEndianReader reader = new BigEndianReader(data))
            {
                MessagePart messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    dynamic message = MessageReceiver.BuildMessage((uint)messagePart.MessageId, reader);
                    if (Config.Debug)
                        ConsoleUtils.Write(ConsoleUtils.type.RECEIV, "{0} Id {1} Length {2} ...", message, messagePart.MessageId, messagePart.Length);
                    MessageHandlerManager<LoginClient>.InvokeHandler(this, message);
                }
            }   
        }

        public void Send(NetworkMessage message)
        {
            try
            {
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    message.Pack(writer);
                    Login.Send(writer.Data);
                }
                if (Config.Debug)
                    ConsoleUtils.Write(ConsoleUtils.type.SEND, "{0} to client {1}:{2} ...", message, Login.Ip, Login.Port);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
