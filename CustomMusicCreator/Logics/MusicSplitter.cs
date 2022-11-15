using CustomMusicCreator.Utils;
using NAudio.Wave;

namespace CustomMusicCreator
{
    /// <summary>
    /// Chunks all music to 4 seconds, and validates music.
    /// </summary>
    internal class MusicSplitter
    {
        private readonly WavValidator _validator = new WavValidator();
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

        internal void ValidateWav(WaveFileReader reader, TimeSpan timeSpan)
        {
            switch (_validator.ValidateWav(reader, timeSpan))
            {
                case WavValidationCode.FormatError:
                    throw new FormatException("The file is not valid WAV file.");
                case WavValidationCode.SampleRateError:
                    throw new InvalidDataException($"Sample rate of the file must be 44100Hz, but the stream is {reader.WaveFormat.SampleRate} Hz.");
                case WavValidationCode.LengthError:
                    throw new Exceptions.DataLengthException(GetLengthErrorMessage());
            }
            string GetLengthErrorMessage()
            {
                var duration = reader.TotalTime;
                var difference = duration - timeSpan;
                string detailMessage = (difference > TimeSpan.Zero) ? $"{difference} longer" : $"{-difference} shorter";
                return $"Error: The time of the file must be EXACTLY [{timeSpan}], but it is [{duration}] ({detailMessage})";
            }
        }
    }
}
