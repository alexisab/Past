using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Past.Common.Network;
using Past.Common.Utils;

namespace Past.Login.Network
{
    public class LoginServer : AbstractServer<LoginClient, LoginServer>
    {
        public LoginServer() : base("Auth", IPAddress.Parse(Config.LoginServerAddress), Config.LoginServerPort, 100)
        {
            ConsoleUtils.Write(ConsoleUtils.Type.INFO, $"Login server started on {Config.LoginServerAddress}:{Config.LoginServerPort} ...");
        }
    }
}
