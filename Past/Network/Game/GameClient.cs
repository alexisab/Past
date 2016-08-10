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

        public GameClient(Client client)
        {
            Game = client;
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
            GameServer.Clients.Remove(this);
            Game.Close();
            ConsoleUtils.Write(ConsoleUtils.type.INFO, "Client disconnected from GameServer ...");
        }

        public void Disconnect()
        {
            GameServer.Clients.Remove(this);
            Game.Close();
        }

        private void Game_OnClientReceivedData(byte[] data)
        {
            using (BigEndianReader reader = new BigEndianReader(data))
            {
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
                    Game.Send(writer.Data);
                }
                ConsoleUtils.Write(ConsoleUtils.type.SEND, "{0} to client {1}:{2} ...", message, Game.Ip, Game.Port);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
