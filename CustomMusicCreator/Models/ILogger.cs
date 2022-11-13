namespace CustomMusicCreator
{
    public interface ILogger
    {
        public void LogMessage(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}
