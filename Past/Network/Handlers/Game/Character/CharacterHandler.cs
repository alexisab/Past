using Past.Database;
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
            var character = client.Account.Characters.FirstOrDefault(x => x.Id == message.id);
            if (character == null)
                client.Send(new CharacterSelectedErrorMessage());

            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(character.Id, character.Name, character.Level, character.Look, character.Breed, character.Sex)));
            client.Send(new NotificationListMessage(new int[0]));
            client.Send(new EmoteListMessage(new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 19, 21, 22, 23, 24 }));
            client.Send(new EnabledChannelsMessage(new sbyte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new sbyte[0]));
            client.Send(new InventoryContentMessage(new ObjectItem[0], 0)); //TODO Get the characters items, kamas
            client.Send(new InventoryWeightMessage(0, 1000)); //TODO Get pods and max pods
            client.Send(new CharacterStatsListMessage(new CharacterCharacteristicsInformations(
                character.Experience,
                Experience.GetLevelFloor(character.Level).CharacterXp,
                character.Level == 200 ? long.MaxValue : Experience.GetNextLevelFloor(character.Level).CharacterXp,
                0,
                0,
                0,
                new ActorExtendedAlignmentInformations(0, 0, 0, 0, 0, 0, false),
                55,
                55,
                10000,
                10000,
                6,
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
            client.Send(new CurrentMapMessage(21891588));
            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]));
            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 152, new string[] { System.DateTime.Now.Year.ToString(), System.DateTime.Now.Month.ToString(), System.DateTime.Now.Day.ToString(), System.DateTime.Now.Hour.ToString(), System.DateTime.Now.Minute.ToString("00"), client.Ip })); //TODO Last connection date / ip
            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 153, new string[] { client.Ip }));
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
