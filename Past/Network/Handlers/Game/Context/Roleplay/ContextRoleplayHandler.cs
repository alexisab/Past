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
            var actors = new GameRolePlayCharacterInformations[]
            {
                new GameRolePlayCharacterInformations(client.Character.Id, client.Character.Look, new EntityDispositionInformations(client.Character.CellId, (sbyte)client.Character.Direction), client.Character.Name, new HumanInformations(new EntityLook[0], 0, 0, new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, true, false, false, false, false, true, true, true, false, false, false, false, false), 0), client.Character.PvPEnabled == true ? new ActorAlignmentInformations((sbyte)client.Character.AlignementSide, 0, (sbyte)Experience.GetCharacterGrade(client.Character.Honor), 0) : new ActorAlignmentInformations(0, 0, 0, 0))
            };
            client.Send(new MapComplementaryInformationsDataMessage(client.Character.Map.Id, client.Character.Map.SubareaId, new HouseInformations[0], actors, new InteractiveElement[0], new StatedElement[0], new MapObstacle[0], new FightCommonInformations[0]));
        }

        public static void HandleChangeMapMessage(GameClient client, ChangeMapMessage message)
        {
            client.Send(new CurrentMapMessage(message.mapId));
        }
    }
}
