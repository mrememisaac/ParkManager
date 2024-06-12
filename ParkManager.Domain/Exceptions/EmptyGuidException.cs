namespace ParkManager.Domain.Exceptions
{
    [Serializable]
    public class EmptyGuidException : ArgumentException
    {
        public EmptyGuidException()
        {
        }

        public EmptyGuidException(string? message) : base(message)
        {
        }
        public EmptyGuidException(string? message, string paramName) : base(message, paramName)
        {
        }

        public EmptyGuidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}