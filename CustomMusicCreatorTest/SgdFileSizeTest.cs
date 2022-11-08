using CustomMusicCreator;

namespace CustomMusicCreatorTest
{
    public class SgdFileSizeTest
    {
        [Fact]
        public void ValidSgdSizeTest()
        {
            using var source = File.OpenRead("testfiles\\sgds\\sgdtest_valid.sgd");
            Assert.True(new SgdConverter().Validate(source));
        }
        [Fact]
        public void InvalidSgdTest()
        {
            using var source = File.OpenRead($"testfiles\\sgds\\sgdtest_invalid.sgd");
            Assert.False(new SgdConverter().Validate(source));
        }

    }
}
