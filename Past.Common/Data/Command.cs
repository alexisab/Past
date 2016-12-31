﻿using Past.Protocol.Enums;
using System.Collections.Generic;

namespace Past.Common.Data
{
    public class Command
    {
        public GameHierarchyEnum Role { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static Dictionary<int, Command> Commands = new Dictionary<int, Command>();

        public Command(GameHierarchyEnum role, string name, string description)
        {
            Role = role;
            Name = name;
            Description = description;
        }

        public static void Initialize()
        {
            Commands.Add(0, new Command(GameHierarchyEnum.PLAYER, "help", "display all commands available"));
            Commands.Add(1, new Command(GameHierarchyEnum.PLAYER, "save", "save the current character"));
            Commands.Add(2, new Command(GameHierarchyEnum.PLAYER, "start", "teleport the current character to his start map"));
            Commands.Add(3, new Command(GameHierarchyEnum.MODERATOR, "goname [target]", "teleport the current character to the target"));
            Commands.Add(4, new Command(GameHierarchyEnum.MODERATOR, "go [mapid]", "teleport the current character to the desired map"));
            Commands.Add(5, new Command(GameHierarchyEnum.GAMEMASTER_PADAWAN, "levelup [target]", "levelup the target"));
            Commands.Add(6, new Command(GameHierarchyEnum.GAMEMASTER_PADAWAN, "addkamas [target] [amount]", "add kamas to the target"));
            Commands.Add(7, new Command(GameHierarchyEnum.GAMEMASTER, "kick [target]", "kick the target"));
        }
    }
}
