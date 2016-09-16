using Past.Database;
using Past.Network.Handlers;
using Past.Protocol;
using Past.Protocol.IO;
using Past.Protocol.Messages;
using Past.Utils;
using System;

namespace Past.Network.Game
{
    public class GameClient
    {
        private Client Game { get; set; }
        public Account Account { get; set; }
        public Character Character { get; set; }
        public string Ip { get; set; }

        public GameClient(Client client)
        {
            Game = client;
            Ip = client.Ip;
            Game_OnClientSocketConnected();
            Game.OnClientSocketClosed += Game_OnClientSocketClosed;
            Game.OnClientReceivedData += Game_OnClientReceivedData;
        }

        private void Game_OnClientSocketConnected()
        {
            Send(new HelloGameMessage());
        }

        private void Game_OnClientSocketClosed()
        {
            Disconnect();
            ConsoleUtils.Write(ConsoleUtils.type.INFO, "Client {0}:{1} disconnected from GameServer ...", Game.Ip, Game.Port);
        }

        public void Disconnect()
        {
            GameServer.Clients.Remove(this);
            Account = null;
            if (Character != null)
            {
                Character.Map.CurrentMap.RemoveClient(this);
                Database.Character.Update(Character);
            }
            Character = null;
            Game.Close();
        }

        private void Game_OnClientReceivedData(byte[] data)
        {
            using (BigEndianReader reader = new BigEndianReader(data))
            {
                MessagePart messagePart = new MessagePart(false);
                if (messagePart.Build(reader))
                {
                    dynamic message = MessageReceiver.BuildMessage((uint)messagePart.MessageId, reader);
                    if (Config.Debug)
                        ConsoleUtils.Write(ConsoleUtils.type.RECEIV, "{0} Id {1} Length {2} ...", message, messagePart.MessageId, messagePart.Length);
                    MessageHandlerManager<GameClient>.InvokeHandler(this, message);
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
                    Game.Send(writer.Data);
                }
                if (Config.Debug)
                    ConsoleUtils.Write(ConsoleUtils.type.SEND, "{0} to client {1}:{2} ...", message, Game.Ip, Game.Port);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
