using CustomMusicCreator;
using NAudio.Wave;

namespace CustomMusicCreatorTest
{
    public class WavFormatConverterTest
    {
        [Fact]
        public void WavFormatConvertTest_Results_RightFormat()
        {
            string filePath = TestInfo.GetFilePath("wavs", "wavtest_invalid_multiple.wav");
            string fileOutputPath = TestInfo.GetFilePath("wavs", "wavtest_converted_valid.wav");
            if (File.Exists(fileOutputPath))
            {
                File.Delete(fileOutputPath);
            }
            new WavFormatConverter().Convert(filePath, fileOutputPath);
            var validator = new WavValidator();
            Assert.NotEqual(WavValidationCode.Valid, validator.ValidateWav(filePath, new TimeSpan(0, 0, 4)));
            Assert.Equal(WavValidationCode.Valid, validator.ValidateWav(fileOutputPath, new TimeSpan(0, 0, 4)));
        }
    }
}
