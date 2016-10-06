using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Protocol;
using Past.Protocol.IO;
using Past.Protocol.Messages;
using System;

namespace Past.Game.Network
{
    public class Client
    {
        private Common.Network.Client GameClient { get; set; }
        public AccountRecord Account { get; set; }
        public CharacterRecord Character { get; set; }

        public Client(Common.Network.Client client)
        {
            GameClient = client;
            GameClient_OnClientSocketConnected();
            GameClient.OnClientSocketClosed += GameClient_OnClientSocketClosed;
            GameClient.OnClientReceivedData += GameClient_OnClientReceivedData;
        }

        private void GameClient_OnClientSocketConnected()
        {
            Send(new HelloGameMessage());
        }

        private void GameClient_OnClientSocketClosed()
        {
            Disconnect();
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Client {GameClient.Ip}:{GameClient.Port} disconnected from game server ...");
        }

        public void Disconnect()
        {
            Server.Clients.Remove(this);
            Account = null;
            /*if (Character != null)
            {
                Character.Map.CurrentMap.RemoveClient(this);
                Database.Character.Update(Character);
            }*/
            Character = null;
            GameClient.Close();
        }

        private void GameClient_OnClientReceivedData(byte[] data)
        {
            using (BigEndianReader reader = new BigEndianReader(data))
            {
                MessagePart messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    dynamic message = MessageReceiver.BuildMessage((uint)messagePart.Id, reader);
                    if (Config.Debug)
                    {
                        ConsoleUtils.Write(ConsoleUtils.Type.RECEIV, $"{message} Id {messagePart.Id} Length {messagePart.Length} ...");
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
                    GameClient.Send(writer.Data);
                }
                if (Config.Debug)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.SEND, $"{message} to client {GameClient.Ip}:{GameClient.Port} ...");
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
            }
        }
    }
}
