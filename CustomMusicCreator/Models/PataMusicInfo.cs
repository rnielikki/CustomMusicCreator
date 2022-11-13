namespace CustomMusicCreator
{
    public class PataMusicInfo
    {
        public string BaseMusic { get; private set; }
        public string Level1Music { get; private set; }
        public string Level2Music { get; private set; }
        public string Level3Music { get; private set; }
        public string VoiceTheme { get; private set; }
        public PataMusicInfo(string baseMusicPath,
            string level1MusicPath, string level2MusicPath, string level3MusicPath, string voiceThemeName)
        {
            BaseMusic = baseMusicPath;
            Level1Music = level1MusicPath;
            Level2Music = level2MusicPath;
            Level3Music = level3MusicPath;
            VoiceTheme= voiceThemeName;
        }
    }
}
