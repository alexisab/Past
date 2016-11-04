using Past.Protocol.Messages;
using Past.Protocol.Types;

namespace Past.Game.Network.Handlers.Context.Roleplay
{
    public class ContextRoleplayHandler
    {
        public static void HandleMapInformationsRequestMessage(Client client, MapInformationsRequestMessage message)
        {
            client.Send(new MapComplementaryInformationsDataMessage(client.Character.Map.Id, (short)client.Character.Map.SubAreaId, new HouseInformations[0], new GameRolePlayActorInformations[0], new InteractiveElement[0], new StatedElement[0], new MapObstacle[0], new FightCommonInformations[0]));
            client.Character.CurrentMap.AddClient(client);
        }

        public static void SendEmoteListMessage(Client client, sbyte[] emoteIds)
        {
            client.Send(new EmoteListMessage(emoteIds));
        }
    }
}
