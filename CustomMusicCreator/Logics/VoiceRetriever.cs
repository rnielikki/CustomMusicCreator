using CustomMusicCreator.Utils;
using System.Linq;

namespace CustomMusicCreator
{
    internal class VoiceRetriever
    {
        private const string _voiceRelativePath = "files/voices";
        private const string _wavDirectoryName = "voices";
        private const string _sgdName = "ptp_btl_bgm_voice.sgd";
        private readonly string _voicePath;
        private readonly Dictionary<string, DirectoryInfo> _availableVoices;
        internal VoiceRetriever()
        {
            _voicePath = Path.Combine(FilePathUtils.ResourcePath, _voiceRelativePath);
            _availableVoices = new DirectoryInfo(_voicePath).GetDirectories().ToDictionary(kv => kv.Name, kv => kv);
        }
        internal string LoadSgd(string voiceName)
        {
            if (!_availableVoices.TryGetValue(voiceName, out var directoryInfo))
            {
                throw new ArgumentException($"The voice name [{voiceName}] doesn't exist.");
            }
            var sgdPath = Path.Combine(directoryInfo.FullName, _sgdName);
            if (!File.Exists(sgdPath))
            {
                throw new FileNotFoundException($"SGD file doesn't exist for the voice name [{voiceName}]");
            }
            return sgdPath;
        }

        internal  string[] LoadWavs(string voiceName)
        {
            throw new NotImplementedException();
        }
    }
}
