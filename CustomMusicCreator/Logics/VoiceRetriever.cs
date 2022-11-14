namespace CustomMusicCreator
{
    internal class VoiceRetriever
    {
        private readonly VoiceData _voiceData;
        internal VoiceRetriever()
        {
            _voiceData = VoiceData.Get();
        }
        internal string LoadSgd(string voiceName)
        {
            var dir = _voiceData.GetVoiceDirectory(voiceName);
            if (dir==null)
            {
                throw new ArgumentException($"The voice name [{voiceName}] doesn't exist.");
            }
            var sgdPath = Path.Combine(dir.FullName, VoiceData.SgdName);
            if (!File.Exists(sgdPath))
            {
                throw new FileNotFoundException($"SGD file doesn't exist for the voice name [{voiceName}]");
            }
            return sgdPath;
        }
    }
}
