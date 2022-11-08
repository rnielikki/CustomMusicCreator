using FFMpegCore;

namespace CustomMusicCreator
{
    internal class AtracConverter
    {
        private readonly ILogger _logger;
        private readonly TimeSpan _range;
        internal AtracConverter(ILogger logger)
        {
            _logger = logger;
            _range = new TimeSpan(TimeSpan.TicksPerMillisecond / 2);
        }
        internal void Convert(string[] filePath, string prefix)
        {
            for(int i=0;i<filePath.Length;i++)
            {
                ConvertEach(filePath[i], $"ptpat_battle_{prefix}_{i+1:00}.wav");
            }
        }
        internal void ConvertEach(string filePath, string newName)
        {
            try
            {
                var fullPath = Path.GetFullPath(filePath);
                var fileName = Path.GetFileName(filePath);

                using var stream = File.OpenRead(filePath);
                ValidateWav(stream);
                //execute here
                //File.Move(Path.GetFileName(filePath), newName);
                if (!File.Exists(
                    Path.Combine(Path.GetDirectoryName(filePath) ?? Directory.GetDirectoryRoot(filePath), newName)))
                {
                    _logger.LogError("Error from [PSP_AT3TOOL]");
                    throw new FileNotFoundException($"Failed to create file from {fileName}. " +
                        $"The converting process didn't generate the file.");
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        internal void ValidateWav(FileStream source)
        {
            string fileName = Path.GetFileName(source.Name);
            var mediaInfo = FFProbe.Analyse(source);
            var timeSpan = new TimeSpan(0, 0, 4);
            _logger.LogMessage($"Validating {fileName}...");
            if (mediaInfo.PrimaryAudioStream == null)
            {
                throw new ArgumentException($"Error: {fileName} is invalid sound file.");
            }
            if (mediaInfo.Format.FormatName != "wav")
            {
                throw new FormatException($"Error: {fileName} must be WAV file");
            }
            var sampleRate = mediaInfo.PrimaryAudioStream.SampleRateHz;
            var duration =
                new TimeSpan(
                source.Length * 80_000_000
                / mediaInfo.PrimaryAudioStream.BitRate);
            var difference = duration - timeSpan;
            if (difference>_range|| difference< -_range)
            {
                string detailMessage = (difference > TimeSpan.Zero) ? $"{difference} longer" : $"{-difference} shorter";
                throw new Exceptions.DataLengthException
                ($"Error: The time of {fileName} must be EXACTLY 4 seconds, but it is {detailMessage}");
            }
            else if (sampleRate != 44100)
            {
                new InvalidDataException($"Sample rate of {fileName} must be 44100Hz, but the stream is {sampleRate} Hz.");
            }
        }
    }
}