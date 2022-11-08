using CustomMusicCreator;
using CustomMusicCreator.Exceptions;
using Moq;

namespace CustomMusicCreatorTest
{
    public class WavValidationTest
    {
        private ILogger _logger;
        public WavValidationTest()
        {
            _logger = Mock.Of<ILogger>();
        }
        [Fact]
        public void ValidWavTest()
        {
            using var source = File.OpenRead("testfiles\\wavs\\wavtest_valid.wav");
            new AtracConverter(_logger).ValidateWav(source);
        }
        [Theory]
        [InlineData("wavtest_invalid_long.wav", typeof(DataLengthException))]
        [InlineData("wavtest_invalid_short.wav", typeof(DataLengthException))]
        [InlineData("wavtest_invalid_samplerate.wav", typeof(InvalidDataException))]
        [InlineData("wavtest_invalid_format.wav", typeof(FormatException))]
        public void InvalidWavTest(string name, Type exceptionType)
        {
            using var source = File.OpenRead($"testfiles\\wavs\\{name}");
            Assert.Throws(exceptionType, () => new AtracConverter(_logger).ValidateWav(source));
        }
    }
}