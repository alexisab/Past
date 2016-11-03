using Past.Common.Data;
using Past.Common.Database.Record;
using Past.Common.Utils;
using Past.Game.Engine;
using Past.Game.Network.Handlers.Chat;
using Past.Game.Network.Handlers.Context.Roleplay;
using Past.Game.Network.Handlers.Inventory;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using System;
using System.Linq;

namespace Past.Game.Network.Handlers.Character
{
    public class CharacterHandler
    {
        public static void HandleCharactersListRequestMessage(Client client, CharactersListRequestMessage message)
        {
            if (client.Account.Characters.Count < 0)
            {
                client.Send(new CharactersListMessage(false, true, new CharacterBaseInformations[0]));
            }
            else
            {
                SendCharactersListMessage(client, false);
            }
        }

        public static void HandleCharacterCreationRequestMessage(Client client, CharacterCreationRequestMessage message)
        {
            if (client.Account.Characters.Count >= 5)
            {
                client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS));
            }
            else
            {
                if (CharacterRecord.NameExist(message.name)) //need to add check for non allowed char
                {
                    client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                }
                else
                {
                    Breed breed = Breed.Breeds[(BreedEnum)message.breed];
                    string look = message.sex == false ? breed.MaleLook : breed.FemaleLook;
                    CharacterRecord character = new CharacterRecord(client.Account.Id, message.name, 1, 0, (BreedEnum)message.breed, message.colors.Distinct().Count() != 1 ? look.Insert(look.IndexOf("||") + 1, $"1={message.colors[0]},2={message.colors[1]},3={message.colors[2]},4={message.colors[3]},5={message.colors[4]}") : look, message.sex, breed.StartMapId, breed.StartDisposition.cellId, (DirectionsEnum)breed.StartDisposition.direction, AlignmentSideEnum.ALIGNMENT_NEUTRAL, 0, 0, false, 0, 0, 0, DateTime.Now);
                    character.Create();
                    client.Account.Characters = CharacterRecord.ReturnCharacters(client.Account.Id);
                    client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.OK));
                    SendCharactersListMessage(client, true);
                }
            }
        }

        public static void HandleCharacterNameSuggestionRequestMessage(Client client, CharacterNameSuggestionRequestMessage message)
        {
            client.Send(new CharacterNameSuggestionSuccessMessage(Functions.RandomName()));
        }

        public static void HandleCharacterDeletionRequestMessage(Client client, CharacterDeletionRequestMessage message)
        {
            CharacterRecord characterRecord = client.Account.Characters.FirstOrDefault(character => character.Id == message.characterId);
            if (characterRecord != null)
            {
                if (characterRecord.Level >= 20 ? Functions.CipherSecretAnswer(message.characterId, client.Account.SecretAnswer) != message.secretAnswerHash : Functions.CipherSecretAnswer(message.characterId, "000000000000000000") != message.secretAnswerHash)
                {
                    client.Send(new CharacterDeletionErrorMessage((sbyte)CharacterDeletionErrorEnum.DEL_ERR_BAD_SECRET_ANSWER));
                }
                else
                {
                    characterRecord.Delete();
                    client.Account.Characters.Remove(characterRecord);
                    SendCharactersListMessage(client, false);
                }
            }
            else
            {
                client.Send(new CharacterDeletionErrorMessage((sbyte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
            }
        }

        public static void HandleCharacterSelectionMessage(Client client, CharacterSelectionMessage message)
        {
            CharacterRecord characterRecord = client.Account.Characters.FirstOrDefault(character => character.Id == message.id);
            if (characterRecord != null)
            {
                CharacterEngine character = new CharacterEngine(characterRecord, client);
                client.Character = character;
                client.Send(new CharacterSelectedSuccessMessage(character.GetCharacterBaseInformations()));

                RoleplayHandler.SendEmoteListMessage(client, new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 19, 21, 22, 23, 24 });
                ChatHandler.SendEnabledChannelsMessage(client, new sbyte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
                InventoryHandler.SendInventoryContentMessage(client, new ObjectItem[0], character.Kamas); //TODO Get the characters items
                InventoryHandler.SendInventoryWeightMessage(client, 0, 1000)); //TODO Get pods and max pods

                client.Send(new AlignmentRankUpdateMessage(1, false));
                client.Send(new AlignmentSubAreasListMessage(new short[0], new short[0]));

                SendCharacterStatsListMessage(client);
                SendSetCharacterRestrictionsMessage(client);
                SendLifePointsRegenBeginMessage(client, 10);

                client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]));
                if (client.Account.LastConnection.HasValue && !string.IsNullOrEmpty(client.Account.LastIp))
                {
                    client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 152, new string[] { $"{client.Account.LastConnection.Value.Year}", $"{client.Account.LastConnection.Value.Month}", $"{client.Account.LastConnection.Value.Day}", $"{client.Account.LastConnection.Value.Hour}", $"{client.Account.LastConnection.Value.Minute}", client.Account.LastIp }));
                }
                client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 153, new string[] { client.Ip }));

                client.Account.LastConnection = DateTime.Now;
                client.Account.LastIp = client.Ip;
                characterRecord.LastUsage = DateTime.Now;
            }
            else
            {
                client.Send(new CharacterSelectedErrorMessage());
            }
        }

        public static void SendCharactersListMessage(Client client, bool tutorial)
        {
            CharacterBaseInformations[] characterBaseInformations = new CharacterBaseInformations[client.Account.Characters.Count];
            for (int i = 0; i < client.Account.Characters.Count; i++)
            {
                characterBaseInformations[i] = new CharacterBaseInformations(client.Account.Characters[i].Id, client.Account.Characters[i].Name, client.Account.Characters[i].Level, Functions.BuildEntityLook(client.Account.Characters[i]), (sbyte)client.Account.Characters[i].Breed, client.Account.Characters[i].Sex);
            }
            client.Send(new CharactersListMessage(false, tutorial, characterBaseInformations));
        }

        public static void SendCharacterStatsListMessage(Client client)
        {
            client.Send(new CharacterStatsListMessage(new CharacterCharacteristicsInformations(
                client.Character.Experience,
                client.Character.ExperienceLevelFloor,
                client.Character.ExperienceNextLevelFloor,
                client.Character.Kamas,
                client.Character.StatsPoints,
                client.Character.SpellsPoints,
                client.Character.GetActorExtendedAlignmentInformations(),
                client.Character.Health,
                client.Character.Health,
                client.Character.Energy,
                10000,
                client.Character.Level >= 100 ? (short)7 : (short)6,
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
        }

        public static void SendSetCharacterRestrictionsMessage(Client client)
        {
            client.Send(new SetCharacterRestrictionsMessage(new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, false, false, false, false, false)));
        }

        public static void SendLifePointsRegenBeginMessage(Client client, byte regenRate)
        {
            client.Send(new LifePointsRegenBeginMessage(regenRate));
        }
    }
}
