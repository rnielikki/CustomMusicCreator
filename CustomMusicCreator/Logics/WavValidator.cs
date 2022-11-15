using NAudio.Wave;

namespace CustomMusicCreator
{
    /// <summary>
    /// Validates a wave file. This can be used before proceeding converesion.
    /// </summary>
    public class WavValidator
    {
        private readonly TimeSpan _range;
        public WavValidator()
        {
            _range = new TimeSpan(TimeSpan.TicksPerMillisecond);
        }
        public WavValidationCode ValidateWav(string path, TimeSpan timeSpan)
        {
            try
            {
                using WaveFileReader reader = new WaveFileReader(path);
                return ValidateWav(reader, timeSpan);
            }
            catch(FormatException)
            {
                return WavValidationCode.FormatError;
            }
        }
        internal WavValidationCode ValidateWav(WaveFileReader reader, TimeSpan timeSpan)
        {
            var sampleRate = reader.WaveFormat.SampleRate;
            var duration = reader.TotalTime;
            var difference = duration - timeSpan;
            if (difference > _range || difference < -_range) return WavValidationCode.LengthError;
            else if (sampleRate != 44100)
            {
                return WavValidationCode.SampleRateError;
            }
            return WavValidationCode.Valid;
        }
    }
}
