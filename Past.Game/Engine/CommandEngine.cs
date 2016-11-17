using Past.Game.Network;
using Past.Game.Network.Handlers.Basic;
using Past.Game.Network.Handlers.Context;
using Past.Protocol.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Past.Game.Engine
{
    public class CommandEngine
    {
        public static Dictionary<string, string> Commands = new Dictionary<string, string>()
        {
            { "help", "Display all commands available" },
            { "save", "Save your character" },
            { "goname", "Go to the player" },
        };
        public static void Handle(Client client, string content)
        {
            string[] command = content.Split(' ');
            switch (command[0])
            {
                case ".help":
                    foreach (var cmd in Commands)
                    {
                        BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { $"{cmd.Key}", $"{cmd.Value}" });
                    }
                    break;
                case ".save":
                    client.Character.Save();
                    break;
                case ".goname":
                    Client targetClient = Server.Clients.FirstOrDefault(target => target.Character.Name == command[1]);
                    if (targetClient != null && targetClient != client)
                    {
                        client.Character.Teleport(targetClient.Character.CurrentMapId, targetClient.Character.CellId);
                    }
                    else
                    {
                        BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { "Error", $"Can't found the character {command[1]} !" });
                    }
                    break;
                default:
                    BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { "Error", $"Command {command[0]} not found !" });
                    break;
            }
        }
    }
}
