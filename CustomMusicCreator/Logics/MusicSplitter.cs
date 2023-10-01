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
        private readonly WavFormatConverter _converter = new WavFormatConverter();
        private readonly ILogger _logger;
        internal MusicSplitter(ILogger logger)
        {
            _logger = logger;
        }
        internal string[] ValidateAndLoadPaths(DirectoryInfo directoryInfo, string filePath, string prefix, TimeSpan timeSpan)
        {
            using var reader = new WaveFileReader(
                    ValidateWav(filePath, timeSpan, Path.Combine(directoryInfo.FullName, $"{prefix}-converted.wav"))
                );
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

        private string ValidateWav(string path, TimeSpan timeSpan, string altFileName)
        {
            using WaveFileReader reader = new WaveFileReader(path);
            var validationResult = _validator.ValidateWav(reader, timeSpan);
            if (WavFormatConverter.IsConvertable(validationResult))
            {
                _logger.LogWarning($"{path}: format is NOT 44100Hz or/and 16-bit signed PCM. This file will be automatically converted.");
                _converter.Convert(path, altFileName);
                return altFileName;
            }
            switch (validationResult)
            {
                case WavValidationCode.Valid:
                    _logger.LogMessage($"{path}: succesfully validated.");
                    return path;
                case WavValidationCode.FormatError:
                    throw new FormatException("The file is not valid WAV file.");
                case WavValidationCode.LengthError:
                    throw new Exceptions.DataLengthException(GetLengthErrorMessage());
                default:
                    throw new InvalidOperationException("An unknown error occured :(");
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
