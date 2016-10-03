using Past.Protocol.Enums;

namespace Past.Common.Utils
{
    public class GameServerManager
    {
        public int Id { get; set; }
        public ServerStatusEnum Status { get; set; }
        public bool IsSelectable { get; set; }
    }
}
