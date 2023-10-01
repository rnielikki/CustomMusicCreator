using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace CustomMusicCreator
{
    public class WavFormatConverter
    {
        public static bool IsConvertable(WavValidationCode code) =>
            code == WavValidationCode.SampleRateError || code == WavValidationCode.EncodingError;

        internal void Convert(string inputPath, string outputPath)
        {
            using WaveFileReader reader = new WaveFileReader(inputPath);
            var resampled = new WdlResamplingSampleProvider(reader.ToSampleProvider(), 44100);
            WaveFileWriter.CreateWaveFile16(outputPath, resampled);
        }
    }
}
