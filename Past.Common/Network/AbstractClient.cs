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

        public int Port
            => ((IPEndPoint)Socket.RemoteEndPoint).Port;

        public AccountRecord Account { get; set; }
        public string Ticket { get; set; }

        private readonly CancellationTokenSource _receiveSource;

        public event SocketDisconnected OnDisconnect;
        public delegate void SocketDisconnected();

        protected AbstractClient()
        {
            _receiveSource = new CancellationTokenSource();
        }

        public abstract void OnReceive(byte[] data);
        //=> ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"{data.Length} bytes received from client {Socket.RemoteEndPoint} ...");

        public virtual void OnCreate()
            => ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Client {Socket.RemoteEndPoint} created ...");

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
                            byte[] buffer = new byte[1024];
                            int readBytes = await Socket.ReceiveAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                            if (readBytes < 1)
                                Disconnect();
                            else
                            {
                                byte[] data = new byte[readBytes];
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
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    message.Pack(writer);
                    Socket.Send(writer.Data);
                }
                ConsoleUtils.Write(ConsoleUtils.Type.SEND, $"{message} to client {Socket.RemoteEndPoint} ...");
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
            OnDisconnect?.Invoke();
            _receiveSource.Cancel();
            Socket.Close();
            Server._clients.Remove((TC)this);
        }
    }
}
