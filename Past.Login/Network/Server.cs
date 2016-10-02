using Past.Common.Utils;
using System;
using System.Collections.Generic;

namespace Past.Login.Network
{
    public class Server
    {
        private static Common.Network.Server LoginServer { get; set; }
        public static List<Client> Clients = new List<Client>();

        public static void Start()
        {
            LoginServer = new Common.Network.Server(Config.LoginServer_Address, Config.LoginServer_Port);
            LoginServer.OnServerStarted += Login_OnServerStarted;
            LoginServer.OnServerAcceptedSocket += Login_OnServerAcceptedSocket;
            LoginServer.OnServerFailedToStart += Login_OnServerFailedToStart;
            LoginServer.Start();
        }

        private static void Login_OnServerFailedToStart(Exception ex)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.ERROR, ex.ToString());
        }

        private static void Login_OnServerStarted()
        {
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, "Login server waiting for new connection ...");
        }

        private static void Login_OnServerAcceptedSocket(Common.Network.Client socket)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, "New client trying to connect to Login server ...");
            Clients.Add(new Client(socket));
        }
    }
}
