using NAudio.Wave;

//I have no idea. Copied from https://markheath.net/post/trimming-wav-file-using-naudio
namespace CustomMusicCreator.Utils
{
    public static class WavFileUtils
    {
        public static void TrimWavFile(WaveFileReader reader, string outPath, TimeSpan cutFromStart, TimeSpan length)
        {
            using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
            {
                double bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000d;

                int startPos = (int)(cutFromStart.TotalMilliseconds * bytesPerMillisecond);
                startPos -= startPos % reader.WaveFormat.BlockAlign;

                int endPos = startPos + (int)(length.TotalMilliseconds * bytesPerMillisecond);
                endPos -= endPos % reader.WaveFormat.BlockAlign;

                TrimWavFile(reader, writer, startPos, endPos);
            }
        }

        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos && reader.Position < reader.Length)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}
