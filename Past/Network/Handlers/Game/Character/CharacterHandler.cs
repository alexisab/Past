﻿using Past.Database;
using Past.Network.Game;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using Past.Utils;
using System;
using System.Linq;

namespace Past.Network.Handlers.Game.Character
{
    public class CharacterHandler
    {
        public static void HandleCharactersListRequestMessage(GameClient client, CharactersListRequestMessage message)
        {
            if (client.Account.Characters.Count < 0)
                client.Send(new CharactersListMessage(false, true, new CharacterBaseInformations[0]));
            else
            {
                var array = new CharacterBaseInformations[client.Account.Characters.Count];
                for (int i = 0; i < client.Account.Characters.Count; i++)
                {
                    array[i] = new CharacterBaseInformations(client.Account.Characters[i].Id, client.Account.Characters[i].Name, client.Account.Characters[i].Level, client.Account.Characters[i].Look, client.Account.Characters[i].Breed, client.Account.Characters[i].Sex);
                }
                client.Send(new CharactersListMessage(false, false, array));
            }
        }

        public static void HandleCharacterSelectionMessage(GameClient client, CharacterSelectionMessage message)
        {
            var character = client.Account.Characters.FirstOrDefault(x => x.Id == message.id);

            if (character == null)
                client.Send(new CharacterSelectedErrorMessage());

            client.Character = character;

            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(character.Id, character.Name, character.Level, character.Look, character.Breed, character.Sex)));
            client.Send(new NotificationListMessage(new int[0]));
            client.Send(new EmoteListMessage(new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 19, 21, 22, 23, 24 }));
            client.Send(new EnabledChannelsMessage(new sbyte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new sbyte[0]));
            client.Send(new InventoryContentMessage(new ObjectItem[0], character.Kamas)); //TODO Get the characters items
            client.Send(new InventoryWeightMessage(0, 1000)); //TODO Get pods and max pods
            client.Send(new AlignmentRankUpdateMessage(1, false));
            client.Send(new AlignmentSubAreasListMessage(new short[0], new short[0]));

            client.Send(new CharacterStatsListMessage(new CharacterCharacteristicsInformations(
                character.Experience,
                Experience.GetLevelFloor(character.Level).CharacterXp,
                character.Level == 200 ? character.Experience : Experience.GetNextLevelFloor(character.Level).CharacterXp,
                character.Kamas,
                character.StatsPoints,
                character.SpellsPoints,
                new ActorExtendedAlignmentInformations((sbyte)character.AlignementSide, 0, (sbyte)Experience.GetCharacterGrade(character.Honor), 0, character.Honor, character.Dishonor, character.PvPEnabled),
                50 + (character.Level * 5),
                50 + (character.Level * 5),
                10000,
                10000,
                character.Level >= 100 ? (short)7 : (short)6,
                3,
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                0,
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterSpellModification[0])));

            client.Send(new SetCharacterRestrictionsMessage(new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, false, false, false, false, false)));
            client.Send(new LifePointsRegenBeginMessage(10));

            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]));

            if (client.Account.LastConnection.HasValue && !string.IsNullOrEmpty(client.Account.LastIp))
                client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 152, new string[] { client.Account.LastConnection.Value.Year.ToString(), client.Account.LastConnection.Value.Month.ToString(), client.Account.LastConnection.Value.Day.ToString(), client.Account.LastConnection.Value.Hour.ToString(), client.Account.LastConnection.Value.Minute.ToString("00"), client.Account.LastIp }));
            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 153, new string[] { client.Ip }));

            client.Account.LastConnection = DateTime.Now;
            client.Account.LastIp = client.Ip;
            character.LastUsage = DateTime.Now;

            Account.Update(client.Account);
            Database.Character.Update(character);
        }

        public static void HandleCharacterCreationRequestMessage(GameClient client, CharacterCreationRequestMessage message) //TODO Clean and add check
        {
            if (client.Account.Characters.Count >= 5)
            {
                client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS));
            }
            else
            {
                Database.Character character = new Database.Character();
                character.OwnerId = client.Account.Id;
                character.Name = message.name;
                character.Breed = message.breed;

                string breed_look = Breed.ReturnBaseLook((BreedEnum)message.breed, message.sex);

                if (message.colors.Distinct().Count() != 1) //Checking if the colors are set or not
                    character.EntityLookString = breed_look.Insert(breed_look.IndexOf("||") + 1, String.Format("1={0},2={1},3={2},4={3},5={4}", message.colors[0], message.colors[1], message.colors[2], message.colors[3], message.colors[4]));
                else
                    character.EntityLookString = breed_look;

                character.Sex = message.sex;
                character.MapId = Breed.ReturnStartMap((BreedEnum)message.breed);
                character.CellId = 242;
                character.Direction = DirectionsEnum.DIRECTION_SOUTH_EAST;
                character.LastUsage = DateTime.Now;

                Database.Character.Create(character);
                client.Account.Characters = Database.Character.ReturnCharacters(client.Account.Id);

                client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.OK));

                var array = new CharacterBaseInformations[client.Account.Characters.Count];
                for (int i = 0; i < client.Account.Characters.Count; i++)
                {
                    array[i] = new CharacterBaseInformations(client.Account.Characters[i].Id, client.Account.Characters[i].Name, client.Account.Characters[i].Level, client.Account.Characters[i].Look, client.Account.Characters[i].Breed, client.Account.Characters[i].Sex);
                }
                client.Send(new CharactersListMessage(false, true, array));
            }
        }

        public static void HandleCharacterNameSuggestionRequestMessage(GameClient client, CharacterNameSuggestionRequestMessage message)
        {
            client.Send(new CharacterNameSuggestionSuccessMessage(Functions.RandomName()));
        }
    }
}