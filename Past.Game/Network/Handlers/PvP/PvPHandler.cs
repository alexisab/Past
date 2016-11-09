using Past.Protocol.Messages;

namespace Past.Game.Network.Handlers.PvP
{
    public class PvPHandler
    {
        public static void SendAlignmentRankUpdateMessage(Client client, sbyte alignmentRank)
        {
            client.Send(new AlignmentRankUpdateMessage(alignmentRank, false));
        }

        public static void SendAlignmentSubAreasListMessage(Client client)
        {
            client.Send(new AlignmentSubAreasListMessage(new short[0], new short[0]));
        }
    }
}
