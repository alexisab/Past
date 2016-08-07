using Past.Network.Game;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;

namespace Past.Network.Handlers.Game.Approach
{
    public class ApproachHandler
    {
        [MessageHandler(110)]
        public static void HandleAuthenticationTicketMessage(GameClient client, AuthenticationTicketMessage message)
        {
            client.Send(new AuthenticationTicketAcceptedMessage());
            //ConsoleUtils.Write(ConsoleUtils.type.DEBUG, "Ticket {0} ...", message.ticket);
        }

        [MessageHandler(150)]
        public static void HandleCharactersListRequestMessage(GameClient client, CharactersListRequestMessage message)
        {
            client.Send(new CharactersListMessage(false, false, new CharacterBaseInformations[]
            {
                new CharacterBaseInformations(1, "admin", 200, new EntityLook(1, new short[] { 10 }, new int[0], new short[] { 125 }, new SubEntity[0]), 1, false)
            }));
        }

        [MessageHandler(152)]
        public static void HandleCharacterSelectionMessage(GameClient client, CharacterSelectionMessage message)
        {
            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(1, "admin", 200, new EntityLook(1, new short[] { 10 }, new int[0], new short[] { 125 }, new SubEntity[0]), 1, false)));
            client.Send(new TextInformationMessage((sbyte)TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]));
        }
    }
}