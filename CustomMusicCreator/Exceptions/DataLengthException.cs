namespace CustomMusicCreator.Exceptions
{
    internal class DataLengthException : Exception
    {
        public DataLengthException()
        {
        }

        public DataLengthException(string? message) : base(message)
        {
        }

        public DataLengthException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
