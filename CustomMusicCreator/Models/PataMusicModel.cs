using CustomMusicCreator.Utils;

namespace CustomMusicCreator
{
    /// <summary>
    /// Model for <see cref="PataMusicCreator"/>, for passing data to the creator.
    /// </summary>
    /// <remarks>This can be generated for passing data, and conversion can be done in other place.</remarks>
    public class PataMusicModel
    {
        public readonly string BaseMusicPath;
        public readonly string Level1MusicPath;
        public readonly string Level2MusicPath;
        public readonly string Level3MusicPath;
        public readonly string VoiceTheme;
        public readonly string DestinationPath;
        public readonly string DestinationDirectory;
        public PataMusicModel(string baseMusicPath,string level1MusicPath, string level2MusicPath, string level3MusicPath,
            string voiceTheme, string destinationPath)
        {
            BaseMusicPath = baseMusicPath;
            Level1MusicPath = level1MusicPath;
            Level2MusicPath = level2MusicPath;
            Level3MusicPath = level3MusicPath;
            VoiceTheme = voiceTheme;
            DestinationPath = destinationPath;
            DestinationDirectory = FilePathUtils.GetParentDirectory(destinationPath);
        }
    }
}
