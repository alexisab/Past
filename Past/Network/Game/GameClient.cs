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

        private void Game_OnClientReceivedData(byte[] data)
        {
            using (BigEndianReader reader = new BigEndianReader(data))
            {
                int header = reader.ReadShort();
                int id = header >> 2;
                int typeLen = header & 3;
                int length = 0;
                switch (typeLen)
                {
                    case 0:
                        break;
                    case 1:
                        length = reader.ReadByte();
                        break;
                    case 2:
                        length = reader.ReadUShort();
                        break;
                    case 3:
                        length = ((reader.ReadSByte() & 255) << 16) + ((reader.ReadByte() & 255) << 8) + (reader.ReadByte() & 255);
                        break;
                }
                ConsoleUtils.Write(ConsoleUtils.type.DEBUG, "Header {0} Id {1} TypeLen {2} Length {3} \n Content {4} ...", header, id, typeLen, length, Functions.ByteArrayToString(data));
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
                ConsoleUtils.Write(ConsoleUtils.type.SEND, "{0} to client {1}:{2} ...", message.ToString(), Game.Ip, Game.Port);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
            }
        }
    }
}
