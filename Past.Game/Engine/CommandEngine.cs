using Past.Game.Network;
using Past.Game.Network.Handlers.Basic;
using Past.Protocol.Enums;

namespace Past.Game.Engine
{
    public class CommandEngine
    {
        public static void Handle(Client client, string content)
        {
            string[] command = content.Split(' ');
            switch (command[0])
            {
                case ".save":
                    client.Character.Save();
                    break;
                default:
                    BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { "Error", $"Command {command[0]} not found !" });
                    break;
            }
        }
    }
}
