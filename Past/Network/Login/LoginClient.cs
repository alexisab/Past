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

        public LoginClient(Client client)
        {
            Login = client;
            Login_OnClientSocketConnected();
            Login.OnClientSocketClosed += Login_OnClientSocketClosed;
            Login.OnClientReceivedData += Login_OnClientReceivedData;
        }

        private void Login_OnClientSocketConnected()
        {
            Ticket = Utils.Functions.RandomString(32, true);
            Send(new ProtocolRequired(1165, 1165));
            Send(new HelloConnectMessage(Ticket));
        }

        private void Login_OnClientSocketClosed()
        {
            LoginServer.Clients.Remove(this);
            Login.Close();
            ConsoleUtils.Write(ConsoleUtils.type.INFO, "Client disconnected from LoginServer ...");
        }

        public void Disconnect()
        {
            LoginServer.Clients.Remove(this);
            Login.Close();
        }

        private void Login_OnClientReceivedData(byte[] data)
        {
            using(BigEndianReader reader = new BigEndianReader(data))
            {
                //ConsoleUtils.Write(ConsoleUtils.type.DEBUG, "Header {0} Id {1} TypeLen {2} Length {3} \n Content {4} ...", header, id, typeLen, length, Functions.ByteArrayToString(data));*/
                MessagePart messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    NetworkMessage message = MessageReceiver.BuildMessage((uint)messagePart.MessageId, reader);
                    ConsoleUtils.Write(ConsoleUtils.type.RECEIV, "{0} Id {1} Length {2} ...", message, messagePart.MessageId, messagePart.Length);
                    MessageHandlerManager.InvokeHandler(this, message);
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
                ConsoleUtils.Write(ConsoleUtils.type.SEND, "{0} to client {1}:{2} ...", message.ToString(), Login.Ip, Login.Port);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
