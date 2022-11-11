using CustomMusicCreator;
using Moq;

namespace CustomMusicCreatorTest
{
    public class SgdConverterTest
    {
        private Mock<ILogger> _loggerMock;
        private ILogger _logger;
        public SgdConverterTest()
        {
            _loggerMock = new Mock<ILogger>();
            _logger = _loggerMock.Object;
        }
        [Fact]
        public void SgdConvertTest_ConvertOne_RightFormat()
        {
            string sourcePath1 = TestInfo.GetFilePath("aa3s", "RightFormat.wav");
            string sourcePath2 = TestInfo.GetFilePath("aa3s", "RightFormat1.wav");
            string[] results = new SgdConverter(_logger).ConvertAll(new[] { sourcePath1, sourcePath2} );
            var resultsWithNames = results.Select (f => Path.GetFileName(f));
            Assert.Contains("RightFormat.sgd", resultsWithNames);
            Assert.Contains("RightFormat1.sgd", resultsWithNames);
        }
        [Fact]
        public void SgdConvertTest_ConvertOne_WrongFormat()
        {
            string sourcePath1 = TestInfo.GetFilePath("aa3s", "RightFormat.wav");
            string sourcePath2 = TestInfo.GetFilePath("aa3s", "WrongFormat.wav");
            string sourcePath3 = TestInfo.GetFilePath("aa3s", "RightFormat1.wav");
            //should throw exception, when sgd conversion is failed.
            Assert.Throws<InvalidOperationException>(() => new SgdConverter(_logger).ConvertAll(new[] { sourcePath1, sourcePath2}));
            _loggerMock.Verify(l => l.LogMessage(It.IsAny<string>()));
        }
    }
}
