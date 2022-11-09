namespace CustomMusicCreator.Exceptions
{
    internal class DataLengthException : ApplicationException
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
