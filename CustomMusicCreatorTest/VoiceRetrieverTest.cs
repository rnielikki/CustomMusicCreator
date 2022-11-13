using CustomMusicCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMusicCreatorTest
{
    public class VoiceRetrieverTest
    {
        private readonly VoiceRetriever _voiceRetriever = new VoiceRetriever();
        [Fact]
        public void LoadVoices_Loaded()
        {
            var file = _voiceRetriever.LoadSgd("16Ponbekedatta");
            Assert.Equal(327_328, new FileInfo(file).Length);
            //will be implemented later
            //string[] wavs = VoiceRetriever.LoadWavs("16Ponbekedatta");
        }
        [Fact]
        public void LoadVoices_FailToLoad()
        {
            Assert.Throws<ArgumentException>(()=>_voiceRetriever.LoadSgd("17TikTokTak"));
            //Assert.Throws<ArgumentException>(()=>VoiceRetriever.LoadWavs("17TikTokTak"));
        }
    }
}
