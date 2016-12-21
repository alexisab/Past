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

            Send(new HelloGameMessage());
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
                    MessageHandlerManager<GameClient>.InvokeHandler(this, message);
                }
            }
        }
    }
}
