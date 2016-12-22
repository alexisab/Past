using Past.Common.Extensions;
using Past.Common.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Past.Common.Database.Record;
using Past.Protocol;
using Past.Protocol.IO;

namespace Past.Common.Network
{
    public abstract class AbstractClient<TC, TS> : IDisposable
        where TS : AbstractServer<TC, TS>
        where TC : AbstractClient<TC, TS>, new()
    {
        public TS Server { get; set; }
        public Socket Socket { get; internal set; }

        public string Ip
            => ((IPEndPoint)Socket.RemoteEndPoint).Address.ToString();


        public AccountRecord Account { get; set; }
        public CharacterRecord CharacterRecord { get; set; } 
        public string Ticket { get; set; }

        private readonly CancellationTokenSource _receiveSource;

        protected AbstractClient()
        {
            _receiveSource = new CancellationTokenSource();
        }

        public virtual void OnReceive(byte[] data)
            => ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"{data.Length} bytes received from client ...");

        public virtual void OnCreate()
            => ConsoleUtils.Write(ConsoleUtils.Type.INFO, "Client created ...");

        internal void Init()
        {
            OnCreate();
            ReceiveLoop();
        }

        private void ReceiveLoop()
        {
            Task.Factory.StartNew(
                async () =>
                {
                    try
                    {
                        for (;;)
                        {
                            var buffer = new byte[1024];
                            var readBytes = await Socket.ReceiveAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                            if (readBytes < 1)
                                Disconnect();
                            else
                            {
                                var data = new byte[readBytes];
                                Array.Copy(buffer, data, data.Length);
                                OnReceive(data);
                            }
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        
                    }
                }, _receiveSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Send(NetworkMessage message)
        {
            try
            {
                using (var writer = new BigEndianWriter())
                {
                    message.Pack(writer);
                    Socket.Send(writer.Data);
                }
                if (Config.Debug)
                {
                    ConsoleUtils.Write(ConsoleUtils.Type.SEND, $"{message} to client {Socket.RemoteEndPoint} ...");
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Write(ConsoleUtils.Type.ERROR, $"{ex}");
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        public void Disconnect()
        {
            _receiveSource.Cancel();
            Socket.Close();
            Server.Clients.Remove((TC)this);
        }
    }
}
