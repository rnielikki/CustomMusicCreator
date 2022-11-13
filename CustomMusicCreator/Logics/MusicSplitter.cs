using CustomMusicCreator.Utils;
using NAudio.Wave;

namespace CustomMusicCreator
{
    /// <summary>
    /// Chunks all music to 4 seconds, and validates music.
    /// </summary>
    internal class MusicSplitter
    {
        private readonly TimeSpan _range;
        internal MusicSplitter()
        {
            _range = new TimeSpan(TimeSpan.TicksPerMillisecond / 2);
        }
        internal string[] ValidateAndLoadPaths(DirectoryInfo directoryInfo, string filePath, string prefix, TimeSpan timeSpan)
        {
            using var reader = new WaveFileReader(filePath);
            ValidateWav(reader, timeSpan);
            if (timeSpan.Seconds <= 4)
            {
                throw new InvalidOperationException("Timespan must be more than 4 seconds.");
            }
            return SplitMusic(reader, directoryInfo, prefix);
        }
        internal string[] SplitMusic(WaveFileReader reader, DirectoryInfo directoryInfo, string prefix)
        {
            int amount = (int)(Math.Round(reader.TotalTime.TotalSeconds, 0)/4);
            string[] filePaths = new string[amount];

            for(int i = 0;i<amount;i++)
            {
                string pathName = Path.Combine(directoryInfo.FullName, $"{prefix}-{i + 1}.wav");
                WavFileUtils.TrimWavFile(
                    reader, pathName, TimeSpan.FromSeconds(4 * i), TimeSpan.FromSeconds(4));
                filePaths[i]=pathName;
            }
            return filePaths;
        }

        internal void ValidateWav(WaveFileReader reader) => ValidateWav(reader, new TimeSpan(0, 0, 4));
        internal void ValidateWav(WaveFileReader reader, TimeSpan timeSpan)
        {
            var sampleRate = reader.WaveFormat.SampleRate;
            var duration = reader.TotalTime;
            var difference = duration - timeSpan;
            if (difference > _range || difference < -_range)
            {
                string detailMessage = (difference > TimeSpan.Zero) ? $"{difference} longer" : $"{-difference} shorter";
                throw new Exceptions.DataLengthException
                ($"Error: The time of the file must be EXACTLY [{timeSpan}], but it is [{duration}] ({detailMessage})");
            }
            else if (sampleRate != 44100)
            {
                throw new InvalidDataException($"Sample rate of the file must be 44100Hz, but the stream is {sampleRate} Hz.");
            }
        }
    }
}
