using Past.Common.Utils;
using Past.Protocol;
using Past.Protocol.IO;
using Past.Protocol.Messages;
using System;

namespace Past.Login.Network
{
    public class Client
    {
        private Common.Network.Client LoginClient { get; set; }
        public string Ticket { get; set; }
        //public Account Account { get; set; }

        public Client(Common.Network.Client client)
        {
            LoginClient = client;
            LoginClient_OnClientSocketConnected();
            LoginClient.OnClientSocketClosed += LoginClient_OnClientSocketClosed;
            LoginClient.OnClientReceivedData += LoginClient_OnClientReceivedData;
        }

        private void LoginClient_OnClientSocketConnected()
        {
            Ticket = Functions.RandomString(32, true);
            Send(new ProtocolRequired(1165, 1165));
            Send(new HelloConnectMessage(Ticket));
        }

        private void LoginClient_OnClientSocketClosed()
        {
            Disconnect();
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, "Client {0}:{1} disconnected from Login server ...", LoginClient.Ip, LoginClient.Port);
        }

        public void Disconnect()
        {
            Server.Clients.Remove(this);
            //Account = null;
            LoginClient.Close();
        }

        private void LoginClient_OnClientReceivedData(byte[] data)
        {
            using (BigEndianReader reader = new BigEndianReader(data))
            {
                MessagePart messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    dynamic message = MessageReceiver.BuildMessage((uint)messagePart.MessageId, reader);
                    if (Config.Debug)
                    {
                        ConsoleUtils.Write(ConsoleUtils.Type.RECEIV, "{0} Id {1} Length {2} ...", message, messagePart.MessageId, messagePart.Length);
                    }
                    MessageHandlerManager<Client>.InvokeHandler(this, message);
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
                    LoginClient.Send(writer.Data);
                }
                if (Config.Debug)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.SEND, "{0} to client {1}:{2} ...", message, LoginClient.Ip, LoginClient.Port);
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.Type.ERROR, ex.ToString());
            }
        }
    }
}