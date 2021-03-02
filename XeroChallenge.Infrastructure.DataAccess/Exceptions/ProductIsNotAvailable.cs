using System;

namespace XeroChallenge.Infrastructure.Persistence.Exceptions
{
    /// <summary>
    /// An exception when there is an operation against unavailable product
    /// </summary>
    public class ProductIsNotAvailable : PersistenceException
    {
        public ProductIsNotAvailable()
        {
        }

        public ProductIsNotAvailable(string message)
            : base(message)
        {
        }

        public ProductIsNotAvailable(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
