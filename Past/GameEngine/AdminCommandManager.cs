using Past.Database;
using Past.Network.Game;
using Past.Network.Handlers.Game.Chat;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using System.Drawing;
using System.Linq;

namespace Past.GameEngine
{
    public class AdminCommandManager
    {
        private static Color[] Colors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Black, Color.Cyan, Color.Fuchsia, Color.White };

        public static void ParseAdminCommand(GameClient client, string rawCommand)
        {
            var command = rawCommand.Split(' ');
            switch (command[0])
            {
                case ".interactive":
                    int i = 0;
                    foreach (var interactive in MapInteractive.MapInteractives.Where(x => x.MapId == client.Character.Map.Id))
                    {
                        client.Send(new DebugHighlightCellsMessage(Colors[i].ToArgb() & 16777215, new short[] { interactive.CellId }));
                        ChatHandler.SendChatServerMessage(client, (sbyte)ChatActivableChannelsEnum.PSEUDO_CHANNEL_INFO, string.Format("{0} : ElementId : {1} CellId : {2} Type : {3}", Colors[i].Name, interactive.ElementId, interactive.CellId, interactive.Type), client.Character.Id, "");
                        i++;
                    }
                    break;
                case ".go":
                    var mapId = int.Parse(command[1]);
                    if (Map.Maps.ContainsKey(mapId))
                    {
                        client.Character.MapId = mapId;
                        client.Character.Map.CurrentMap.RemoveClient(client);
                        client.Send(new CurrentMapMessage(mapId));
                    }
                    else
                    {
                        client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { "Error", "Can't found the map " + mapId + " !" }));
                    }
                    break;
                case ".goname":
                    var targetClient = GameServer.Clients.FirstOrDefault(x => x.Character.Name == command[1]);
                    if (targetClient != null && targetClient != client)
                    {
                        client.Character.MapId = targetClient.Character.Map.Id;
                        client.Character.CellId = targetClient.Character.CellId;
                        client.Character.Map.CurrentMap.RemoveClient(client);
                        client.Send(new CurrentMapMessage(targetClient.Character.Map.Id));
                    }
                    else
                    {
                        client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { "Error", "Can't found the character " + command[1] + " !" }));
                    }
                    break;
                default:
                    client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new string[] { "Error", "Command not found !" }));
                    break;
            }
        }
    }
}
