using CustomMusicCreator;
using CustomMusicCreator.Utils;
using NAudio.Wave;

namespace CustomMusicCreatorTest
{
    //Do not try to mock this, you can't (they don't have abstraction xD)
    public class MusicSplitterTest
    {
        [Fact]
        void SplitMusicTest()
        {
            string tempPath = TestInfo.GetFilePath("split-target", FilePathUtils.TempPath);
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
                musicSplitter.ValidateWav(new WaveFileReader(path), new TimeSpan(0,0,4));
            }
        }
    }
}
