using CustomMusicCreator;
using Moq;

namespace CustomMusicCreatorTest
{
    public class AtracConverterTest
    {
        private readonly AtracConverter _converter;
        private readonly Mock<ILogger> _logger;
        public AtracConverterTest()
        {
            _logger = new Mock<ILogger>();
            _converter = new AtracConverter(_logger.Object);
        }
        [Fact]
        public void ConvertEach_Success()
        {
            _converter.ConvertEach(
                TestInfo.GetFilePath("wavs", "wavtest_valid.wav"), "result.wav");
            _logger.Verify(v => v.LogMessage(It.IsAny<string>()));
        }
        [Fact]
        public void ConvertEach_Throws_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(
                () => _converter.ConvertEach(
                TestInfo.GetFilePath("wavs", "wavtest_invalid_format.wav"), "result2.wav")
                );
            _logger.Verify(v => v.LogMessage(It.IsAny<string>()));
        }
    }
}