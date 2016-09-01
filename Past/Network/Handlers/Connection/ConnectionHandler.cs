using Past.Database;
using Past.Network.Login;
using Past.Protocol.Enums;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using Past.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Past.Network.Handlers.Connection
{
    public class ConnectionHandler
    {
        public static void HandleIdentificationMessage(LoginClient client, IdentificationMessage message) //TODO Auto connect
        {
            Account account = Account.ReturnAccount(message.login);
            if (account == null)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
                return;
            }
            string version = message.version.major + "." + message.version.minor + "." + message.version.revision + "." + message.version.buildType;
            if (version != "2.0.0.0")
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, new Protocol.Types.Version(2, 0, 0, 0)));
                return;
            }
            if (account.BannedUntil > DateTime.Now)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                return;
            }
            if (Functions.CipherString(account.Password, client.Ticket) != message.password)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
                return;
            }
            else
            {
                client.Account = account;
                if (client.Account.Nickname == string.Empty)
                {
                    client.Send(new NicknameRegistrationMessage());
                    return;
                }
                if (LoginServer.Clients.Count(x => x.Account.Login == account.Login) > 1)
                {
                    LoginServer.Clients.First(x => x.Account.Login == account.Login).Disconnect();
                }
                client.Send(new IdentificationSuccessMessage(account.HasRights, false, account.Nickname, 0, account.SecretQuestion, 42195168000000));
                client.Send(new ServersListMessage(new GameServerInformations[]
                {
                    new GameServerInformations(111, 3, 0, true, 0),
                }));
            }
        }

        public static void HandleIdentificationMessageWithServerIdMessage(LoginClient client, IdentificationMessageWithServerIdMessage message)
        {

        }

        public static void HandleServerSelectionMessage(LoginClient client, ServerSelectionMessage message)
        {
            client.Send(new SelectedServerDataMessage(message.serverId, "127.0.0.1", 5555, true, client.Ticket));
            TransitionHelper.AddPlayerAccount(client.Account, client.Ticket);
            client.Disconnect();
        }

        public static void HandleNicknameChoiceRequestMessage(LoginClient client, NicknameChoiceRequestMessage message)
        {
            string nickname = message.nickname;
            if (!Regex.IsMatch(nickname, "^[a-zA-Z\\-]{3,29}$", RegexOptions.Compiled))
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.INVALID_NICK));
            }
            else
            {
                if (nickname == client.Account.Login)
                {
                    client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.SAME_AS_LOGIN));
                }
                else
                {
                    if (client.Account.Login.Contains(nickname))
                    {
                        client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.TOO_SIMILAR_TO_LOGIN));
                    }
                    else
                    {
                        if (Account.NicknameExist(nickname))
                        {
                            client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.ALREADY_USED));
                        }
                        else
                        {
                            client.Account.Nickname = nickname;
                            Account.Update(client.Account);
                            client.Send(new NicknameAcceptedMessage());
                            Console.WriteLine("{0} {1}", client.Account.Login, client.Account.Nickname);
                            client.Send(new IdentificationSuccessMessage(client.Account.HasRights, false, client.Account.Nickname, 0, client.Account.SecretQuestion, 42195168000000));
                            client.Send(new ServersListMessage(new GameServerInformations[]
                            {
                                new GameServerInformations(111, 3, 0, true, 0),
                            }));
                        }
                    }
                }
            }
        }
    }
}
