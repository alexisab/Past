using System;
using System.Net;
using System.Threading;
using Past.Common.Utils;
using System.Net.Sockets;

namespace Past.Common.Network
{
    public class Server
    {
        private Socket Socket { get; set; }
        public string Address { get; private set; }
        public int Port { get; private set; }
        public bool IsRunning { get; private set; }
        public event Action OnServerStopped;
        public event Action OnServerStarted;
        public event ServerFailedToStart OnServerFailedToStart;
        public event ServerAcceptedSocket OnServerAcceptedSocket;
        private readonly object Object = new object();

        public Server(string address, int port)
        {
            Address = address;
            Port = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (!IsRunning)
            {
                try
                {
                    IsRunning = true;
                    Socket.Bind(new IPEndPoint(IPAddress.Parse(Address), Port));
                    Socket.Listen(100);
                    new Thread(new ThreadStart(this.AcceptThread)).Start();
                    ServerStart();
                }
                catch (Exception ex)
                {
                    ServerFailToStart(ex);
                }
            }
            else
            {
                ConsoleUtils.Write(ConsoleUtils.Type.ERROR, "Server is already running on {0}:{1} ...", Address, Port);
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Socket.Shutdown(SocketShutdown.Both);
                ServerStop();
            }
            else
            {
                ConsoleUtils.Write(ConsoleUtils.Type.ERROR, "Can't close the server is not running ...");
            }
        }

        private void AcceptThread()
        {
            lock (Object)
            {
                try
                {
                    Socket.BeginAccept(new AsyncCallback(this.AcceptCallBack), Socket);
                }
                catch (Exception)
                {
                    AcceptThread();
                }
            }
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            lock (Object)
            {
                try
                {
                    Client socket = new Client(Socket.EndAccept(ar));
                    ServerAcceptSocket(socket);
                    AcceptThread();
                }
                catch (Exception)
                {
                    AcceptThread();
                }
            }
        }

        private void ServerStart()
        {
            OnServerStarted?.Invoke();
        }

        private void ServerStop()
        {
            OnServerStopped?.Invoke();
        }

        private void ServerFailToStart(Exception ex)
        {
            OnServerFailedToStart?.Invoke(ex);
        }

        private void ServerAcceptSocket(Client socket)
        {
            OnServerAcceptedSocket?.Invoke(socket);
        }

        public delegate void ServerFailedToStart(Exception ex);

        public delegate void ServerAcceptedSocket(Client socket);
    }
}