using CustomMusicCreator;
using CustomMusicCreator.Exceptions;
using NAudio.Wave;

namespace CustomMusicCreatorTest
{
    //Do not try to mock this, you can't (they don't have abstraction xD)
    public class MusicSplitterTest
    {
        [Fact]
        public void ValidateWavTest_IsValid()
        {
            string filePath = TestInfo.GetFilePath("wavs", "wavtest_valid.wav");
            using var reader = new WaveFileReader(filePath);
            new MusicSplitter().ValidateWav(reader);
        }
        [Theory]
        [InlineData("wavtest_invalid_long.wav", typeof(DataLengthException))]
        [InlineData("wavtest_invalid_short.wav", typeof(DataLengthException))]
        [InlineData("wavtest_invalid_samplerate.wav", typeof(InvalidDataException))]
        [InlineData("wavtest_invalid_format.wav", typeof(FormatException))]
        public void ValidateWavTest_IsInvalid(string name, Type exceptionType)
        {
            string filePath = TestInfo.GetFilePath("wavs", name);
            Assert.Throws(exceptionType, () =>
            {
                using var reader = new WaveFileReader(filePath);
                new MusicSplitter().ValidateWav(reader);
            });
        }
        [Fact]
        void SplitMusicTest()
        {
            string tempPath = TestInfo.GetFilePath("split-target", PataMusicCreator.TempPath);
            string inputWav = TestInfo.GetFilePath("split-target", "input.wav");
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
            var directoryInfo = Directory.CreateDirectory(tempPath);

            using var reader = new WaveFileReader(inputWav);
            var musicSplitter = new MusicSplitter();
            foreach (var path in musicSplitter.SplitMusic(reader, directoryInfo, "test"))
            {
                musicSplitter.ValidateWav(new WaveFileReader(path));
            }
        }
    }
}
