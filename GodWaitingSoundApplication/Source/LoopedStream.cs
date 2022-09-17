using NAudio.Wave;

namespace GodWaitingSoundApplication
{
    class LoopedStream : WaveStream
    {
        WaveStream source;
        public bool enableLooping = true;

        public LoopedStream(WaveStream source) { this.source = source; }

        public override WaveFormat WaveFormat => source.WaveFormat;

        public override long Length => source.Length;

        public override long Position { get => source.Position; set => source.Position = value; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;
            while (totalBytesRead < count)
            {
                int bytesRead = source.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (source.Position == 0 || !enableLooping)
                        break;

                    source.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
}
