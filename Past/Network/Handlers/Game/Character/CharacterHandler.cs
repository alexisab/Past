using Past.Network.Game;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using Past.Utils;
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
            var character = client.Account.Characters.First(x => x.Id == message.id);
            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(character.Id, character.Name, character.Level, character.Look, character.Breed, character.Sex)));
            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]));
        }

        public static void HandleCharacterCreationRequestMessage(GameClient client, CharacterCreationRequestMessage message)
        {
            client.Send(new CharacterCreationResultMessage(1)); //error
        }

        public static void HandleCharacterNameSuggestionRequestMessage(GameClient client, CharacterNameSuggestionRequestMessage message)
        {
            client.Send(new CharacterNameSuggestionSuccessMessage(Functions.RandomName()));
        }
    }
}
