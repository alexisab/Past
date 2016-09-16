using Past.Database;
using Past.Network.Game;
using Past.Protocol.Messages;
using Past.Protocol.Types;

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
            if (client.Character.Map.TopNeighbourId == message.mapId)
                client.Character.CellId += 532;
            else if (client.Character.Map.BottomNeighbourId == message.mapId)
                client.Character.CellId -= 532;
            else if (client.Character.Map.LeftNeighbourId == message.mapId)
                client.Character.CellId += 13;
            else if (client.Character.Map.RightNeighbourId == message.mapId)
                client.Character.CellId -= 13;

            client.Character.Map.CurrentMap.RemoveClient(client);
            client.Send(new CurrentMapMessage(message.mapId));
        }
    }
}
