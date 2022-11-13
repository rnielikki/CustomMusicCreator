namespace CustomMusicCreator.ConsoleApp
{
    internal class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine(message);
        }
    }
}
