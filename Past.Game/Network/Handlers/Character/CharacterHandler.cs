using Past.Common.Data;
using Past.Common.Database.Record;
using Past.Common.Utils;
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
            CharacterRecord character = client.Account.Characters.FirstOrDefault(x => x.Id == message.characterId);
            if (character != null)
            {
                if (character.Level >= 20 ? Functions.CipherSecretAnswer(message.characterId, client.Account.SecretAnswer) != message.secretAnswerHash : Functions.CipherSecretAnswer(message.characterId, "000000000000000000") != message.secretAnswerHash)
                {
                    client.Send(new CharacterDeletionErrorMessage((sbyte)CharacterDeletionErrorEnum.DEL_ERR_BAD_SECRET_ANSWER));
                }
                else
                {
                    character.Delete();
                    client.Account.Characters.Remove(character);
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
            CharacterRecord characterRecord = client.Account.Characters.FirstOrDefault(x => x.Id == message.id);
            if (characterRecord != null)
            {
                Engine.Character character = new Engine.Character(characterRecord, client);
                client.Send(new CharacterSelectedSuccessMessage(character.GetCharacterBaseInformations()));

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

        public void SendCharacterStatsListMessage(Client client)
        {
            client.Send(new CharacterStatsListMessage());
        }
    }
}
