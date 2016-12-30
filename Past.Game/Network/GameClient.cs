using Past.Common.Network;
using Past.Common.Utils;
using Past.Game.Engine;
using Past.Protocol;
using Past.Protocol.IO;
using Past.Protocol.Messages;

namespace Past.Game.Network
{
    public class GameClient : AbstractClient<GameClient, GameServer>
    {
        public CharacterEngine Character { get; set; }

        public override void OnCreate()
        {
            base.OnCreate();
            base.OnDisconnect += OnDisconnect;
            Send(new HelloGameMessage());
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
                    MessageHandlerManager<GameClient>.InvokeHandler(this, message);
                }
            }
        }

        public new void OnDisconnect()
        {
            Account?.Update();
            Character?.Disconnect();
            Account = null;
            Character = null;
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Client {Ip}:{Port} disconnected from game server ...");
        }
    }
}
