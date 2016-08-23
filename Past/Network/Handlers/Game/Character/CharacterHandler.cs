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
            client.Send(new CharactersListMessage(false, false, new CharacterBaseInformations[] { new CharacterBaseInformations(1, "admin", 200, new EntityLook(1, new short[] { 10 }, new int[0], new short[] { 125 }, new SubEntity[0]), 1, false) }));
        }

        public static void HandleCharacterSelectionMessage(GameClient client, CharacterSelectionMessage message)
        {
            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(1, "admin", 200, new EntityLook(1, new short[] { 10 }, new int[0], new short[] { 125 }, new SubEntity[0]), 1, false)));
        }

        public static void HandleCharacterCreationRequestMessage(GameClient client, CharacterCreationRequestMessage message)
        {
            //client.Send(new CharacterCreationResultMessage(1)); //error
        }

        public static void HandleCharacterNameSuggestionRequestMessage(GameClient client, CharacterNameSuggestionRequestMessage message)
        {
            client.Send(new CharacterNameSuggestionSuccessMessage(Functions.RandomName()));
        }
    }
}
