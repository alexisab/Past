using Past.Network.Game;
using Past.Protocol.Messages;
using Past.Protocol.Types;
using System.Linq;

namespace Past.Network.Handlers.Game.Context.Roleplay
{
    public class ContextRoleplayHandler
    {
        public static void HandleMapInformationsRequestMessage(GameClient client, MapInformationsRequestMessage message)
        {
            client.Send(new MapComplementaryInformationsDataMessage(client.Character.Map.Id, client.Character.Map.SubareaId, new HouseInformations[0], new GameRolePlayActorInformations[0], new InteractiveElement[0], new StatedElement[0], new MapObstacle[0], new FightCommonInformations[0]));
            client.Character.Map.CurrentMap.AddClient(client);
        }

        public static void HandleChangeMapMessage(GameClient client, ChangeMapMessage message)
        {
            short cell = client.Character.CellId;
            if (client.Character.Map.TopNeighbourId == message.mapId)
                cell += 532;
            if (client.Character.Map.BottomNeighbourId == message.mapId)
                cell -= 532;
            if (client.Character.Map.LeftNeighbourId == message.mapId)
                cell += 13;
            if (client.Character.Map.RightNeighbourId == message.mapId)
                cell -= 13;
            client.Character.CellId = cell;
            client.Character.MapId = message.mapId;
            client.Character.Map.CurrentMap.RemoveClient(client);
            client.Send(new CurrentMapMessage(message.mapId));
        }

        public static void HandleEmotePlayRequestMessage(GameClient client, EmotePlayRequestMessage message)
        {
            client.Character.Map.CurrentMap.Send(new EmotePlayMessage(message.emoteId, 0, client.Character.Id));
        }

        public static void HandleGameRolePlayPlayerFightRequestMessage(GameClient client, GameRolePlayPlayerFightRequestMessage message)
        {
            var targetClient = client.Character.Map.CurrentMap.Clients.FirstOrDefault(x => x.Character.Id == message.targetId);
            if (targetClient != null && targetClient != client)
            {
                client.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage(client.Character.Id, client.Character.Id, targetClient.Character.Id));
                targetClient.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage(client.Character.Id, client.Character.Id, targetClient.Character.Id));
            }
        }

        public static void HandleGameRolePlayPlayerFightFriendlyAnswerMessage(GameClient client, GameRolePlayPlayerFightFriendlyAnswerMessage message)
        {
            client.Send(new GameContextDestroyMessage());
            client.Send(new GameContextCreateMessage(2));

            client.Send(new GameFightJoinMessage(true, true, false, false, 30000, 1));

            client.Send(new GameFightStartingMessage(1));

            client.Send(new GameFightPlacementPossiblePositionsMessage(new short[] { client.Character.CellId }, new short[] { client.Character.CellId }, 2));
            client.Send(new GameFightShowFighterMessage(new GameFightFighterInformations(client.Character.Id, client.Character.Look, client.Character.Disposition, 1, true, new GameFightMinimalStats(50, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0))));

        }
    }
}
