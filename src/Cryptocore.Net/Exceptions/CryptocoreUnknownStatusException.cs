using System;
using System.Net;

namespace Cryptocore.Net
{
    public class CryptocoreUnknownStatusException : CryptocoreHttpException
    {
        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Constructor.
        /// </summary>
        public CryptocoreUnknownStatusException()
            : base(HttpStatusCode.GatewayTimeout, null, 0, "It is important to NOT treat this as a failure; the execution status is UNKNOWN and could have been a success.")
        { }

        #endregion Constructors
    }
}