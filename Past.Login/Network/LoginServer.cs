using System.Collections.Generic;
using System.Net;
using Past.Common.Network;
using Past.Common.Utils;

namespace Past.Login.Network
{
    public class LoginServer : AbstractServer<LoginClient, LoginServer>
    {
        public static List<LoginClient> Clients;

        public LoginServer() : base("Auth", IPAddress.Parse(Config.LoginServerAddress), Config.LoginServerPort, 100)
        {
            Clients = _clients;
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Login server started on {Config.LoginServerAddress}:{Config.LoginServerPort} ...");
        }
    }
}
