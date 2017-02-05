using Past.Protocol.IO;

namespace Past.Protocol.Files.Dlm.Elements
{
    public class SoundElement : BasicElement
    {
        public int SoundId { get; set; }
        public short BaseVolume { get; set; }
        public int FullVolumeDistance { get; set; }
        public int NullVolumeDistance { get; set; }
        public short MinDelayBetweenLoops { get; set; }
        public short MaxDelayBetweenLoops { get; set; }

        public SoundElement(Cell cell) : base(cell)
        {
        }

        public SoundElement FromRaw(Cell cell, BigEndianReader raw)
        {
            SoundElement element = new SoundElement(cell);
            SoundId = raw.ReadInt();
            BaseVolume = raw.ReadShort();
            FullVolumeDistance = raw.ReadInt();
            NullVolumeDistance = raw.ReadInt();
            MinDelayBetweenLoops = raw.ReadShort();
            MaxDelayBetweenLoops = raw.ReadShort();
            return element;
        }
    }
}
