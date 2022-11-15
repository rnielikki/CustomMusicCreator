using CustomMusicCreator;

namespace CustomMusicCreatorTest
{
    public class WavValidatorTest
    {
        [Theory]
        [InlineData("wavtest_valid.wav", WavValidationCode.Valid)]
        [InlineData("wavtest_invalid_long.wav", WavValidationCode.LengthError)]
        [InlineData("wavtest_invalid_short.wav", WavValidationCode.LengthError)]
        [InlineData("wavtest_invalid_samplerate.wav", WavValidationCode.SampleRateError)]
        [InlineData("wavtest_invalid_format.wav", WavValidationCode.FormatError)]
        public void ValidateWavTest_Returns_RightCodes(string name, WavValidationCode code)
        {
            string filePath = TestInfo.GetFilePath("wavs", name);
            Assert.Equal(code, new WavValidator().ValidateWav(filePath, new TimeSpan(0, 0, 4)));
        }
    }
}
