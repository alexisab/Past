using Past.Network.Game;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using Past.Utils;

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
                var look = new EntityLook(1, new short[] { 10 }, new int[0], new short[] { 125 }, new SubEntity[0]);
                for (int i = 0; i < client.Account.Characters.Count; i++)
                {
                    array[i] = new CharacterBaseInformations(client.Account.Characters[i].Id, client.Account.Characters[i].Name, 1, look, client.Account.Characters[i].Breed, client.Account.Characters[i].Sex);
                }
                client.Send(new CharactersListMessage(false, false, array));
            }
        }

        public static void HandleCharacterSelectionMessage(GameClient client, CharacterSelectionMessage message)
        {
            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(1, "admin", 200, new EntityLook(1, new short[] { 10 }, new int[0], new short[] { 125 }, new SubEntity[0]), 1, false)));
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
