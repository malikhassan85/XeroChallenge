using System;

namespace XeroChallenge.Infrastructure.Persistence.Exceptions
{
    /// <summary>
    /// Represents the Exception class for all persistence related operations
    /// </summary>
    public class PersistenceException : Exception
    {
        public PersistenceException()
        {
        }

        public PersistenceException(string message)
            : base(message)
        {
        }

        public PersistenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
