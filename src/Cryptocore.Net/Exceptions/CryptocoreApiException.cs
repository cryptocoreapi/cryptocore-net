using System;

namespace Cryptocore.Net
{
    public class CryptocoreApiException : Exception
    {
        #region Constructors

        public CryptocoreApiException()
        { }

        public CryptocoreApiException(string message)
            : base(message)
        { }

        public CryptocoreApiException(string message, Exception innerException)
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}