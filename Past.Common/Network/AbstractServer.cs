using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using Past.Common.Extensions;
using static System.Console;
using System.Collections.Generic;

namespace Past.Common.Network
{
    public abstract class AbstractServer<TC, TS>
        where TC : AbstractClient<TC, TS>, new()
        where TS : AbstractServer<TC, TS>
    {
        private readonly Socket _socket;
        private readonly CancellationTokenSource _acceptCts;
        public readonly List<TC> _clients;

        public string Name { get; }

        protected AbstractServer(string name, IPAddress ip, int port, int backlog)
        {
            Name = name;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _socket.Bind(new IPEndPoint(ip, port));
            _socket.Listen(backlog);

            _acceptCts = new CancellationTokenSource();

            _clients = new List<TC>();
            AcceptLoop();
        }

        private void AcceptLoop()
        {
            Task.Factory.StartNew(
                async () =>
                {
                    try
                    {
                        for (;;)
                        {
                            Socket s = await _socket.AcceptAsync();
                            if (!OnAccept(s)) continue;
                            TC tc = new TC { Socket = s, Server = (TS)this };
                            tc.Init();
                            _clients.Add(tc);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLine(ex);
                    }
                }, _acceptCts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            using (_acceptCts)
            {
                _acceptCts.Cancel();
                //disable all clients.
                foreach (TC c in _clients)
                {
                    c.Dispose();
                }
            }
            OnStop();
        }

        public virtual void OnStop() => WriteHelper("Server down (stop requested).");
        //handle debug etc..
        protected void WriteHelper(string msg) => WriteLine($"<{Name}> {msg}");

        /// <summary>
        /// Guard clause to validate new sockets.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public virtual bool OnAccept(Socket client) => true;
    }
}