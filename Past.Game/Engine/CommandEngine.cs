using System.Linq;
using Past.Common.Data;
using Past.Game.Network;
using Past.Game.Network.Handlers.Basic;
using Past.Protocol.Enums;

namespace Past.Game.Engine
{
    public class CommandEngine
    {
        public static void Handle(GameClient client, string content)
        {
            string[] command = content.Split(' ');
            switch (command[0])
            {
                case ".help":
                    Command.Commands.Where(cmd => cmd.Role <= client.Account.Role).ToList().ForEach(cmd => BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new[] { $"{cmd.Name}", $"{cmd.Description}" }));
                    break;
                case ".save":
                    client.Character.Save();
                    break;
                case ".start":
                    client.Character.Teleport(client.Character.GetSpawnMap, client.Character.BreedData.StartDisposition.cellId);
                    break;
                case ".goname":
                    if (client.Account.Role >= GameHierarchyEnum.MODERATOR)
                    {
                        if (string.IsNullOrEmpty(command[1]))
                        {
                            BasicHandler.SendTextInformationMessage(client,
                                TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                new[] {"Error", "Enter a name first !"});
                        }
                        else
                        {
                            GameClient targetClient =
                                GameServer.Clients.FirstOrDefault(target => target.Character.Name == command[1]);
                            if (targetClient != null && targetClient != client)
                            {
                                client.Character.Teleport(targetClient.Character.CurrentMapId,
                                    targetClient.Character.CellId);
                            }
                            else
                            {
                                BasicHandler.SendTextInformationMessage(client,
                                    TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                    new[] {"Error", $"Can't found the character {command[1]} !"});
                            }
                        }
                    }
                    break;
                case ".go":
                    if (client.Account.Role >= GameHierarchyEnum.MODERATOR)
                    {
                        if (string.IsNullOrEmpty(command[1]))
                        {
                            BasicHandler.SendTextInformationMessage(client,
                                    TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                    new[] { "Error", "Enter a map id first !" });
                        }
                        else
                        {
                            Map map = Map.Maps.FirstOrDefault(findMap => findMap.Id == int.Parse(command[1]));
                            if (map != null && map.Id != client.Character.CurrentMapId)
                            {
                                client.Character.Teleport(map.Id, client.Character.CellId);
                            }
                            else
                            {
                                BasicHandler.SendTextInformationMessage(client,
                                    TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                    new[] {"Error", $"Can't found the map {command[1]} !"});
                            }
                        }
                    }
                    break;
                case ".levelup":
                    if (client.Account.Role >= GameHierarchyEnum.GAMEMASTER_PADAWAN)
                    {
                        if (string.IsNullOrEmpty(command[1]))
                        {
                            client.Character.LevelUp();
                        }
                        else
                        {
                            GameClient targetClient = GameServer.Clients.FirstOrDefault(target => target.Character.Name == command[1]);
                            if (targetClient != null)
                            {
                                targetClient.Character.LevelUp();
                            }
                            else
                            {
                                BasicHandler.SendTextInformationMessage(client,
                                    TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                    new[] {"Error", $"Can't found the character {command[1]} !"});
                            }
                        }
                    }
                    break;
                case ".addkamas":
                    if (client.Account.Role >= GameHierarchyEnum.GAMEMASTER_PADAWAN)
                    {
                        if (string.IsNullOrEmpty(command[1]))
                        {
                            BasicHandler.SendTextInformationMessage(client,
                                TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                new[] { "Error", "Enter a name first !" });
                        }
                        else
                        {
                            GameClient targetClient = GameServer.Clients.FirstOrDefault(target => target.Character.Name == command[1]);
                            if (targetClient != null)
                            {
                                int amount;
                                if (int.TryParse(command[2], out amount))
                                {
                                    targetClient.Character.AddKamas(amount);
                                }
                                else
                                {
                                    BasicHandler.SendTextInformationMessage(client,
                                        TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                        new[] {"Error", $"{command[2]} is not a valid amount !"});
                                }
                            }
                            else
                            {
                                BasicHandler.SendTextInformationMessage(client,
                                    TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                    new[] {"Error", $"Can't found the character {command[1]} !"});
                            }
                        }
                    }
                    break;
                case ".kick":
                    if (client.Account.Role >= GameHierarchyEnum.GAMEMASTER)
                    {
                        if (string.IsNullOrEmpty(command[1]))
                        {
                            BasicHandler.SendTextInformationMessage(client,
                                TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                new[] {"Error", "Enter a name first !"});
                        }
                        else
                        {
                            GameClient targetClient = GameServer.Clients.FirstOrDefault(target => target.Character.Name == command[1]);
                            if (targetClient != null)
                            {
                                BasicHandler.SendSystemMessageDisplayMessage(targetClient, true, 18,
                                    new[] {client.Character.Name, ""});
                                targetClient.Disconnect();
                            }
                            else
                            {
                                BasicHandler.SendTextInformationMessage(client,
                                    TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16,
                                    new[] {"Error", $"Can't found the character {command[1]} !"});
                            }
                        }
                    }
                    break;
                default:
                    BasicHandler.SendTextInformationMessage(client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 16, new[] { "Error", $"Command {command[0]} not found !" });
                    break;
            }
        }
    }
}
