namespace Apollo.NetCore.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exepción para representar errores de http BadRequest.
    /// </summary>
    /// <remarks>
    /// The server, while acting as a gateway or proxy, received an invalid response from the upstream
    /// server it accessed in attempting to fulfill the request.
    /// </remarks>
    public class BadGatewayException : ApiException
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BadGatewayException"/> class.
        /// </summary>
        public BadGatewayException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadGatewayException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public BadGatewayException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadGatewayException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BadGatewayException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}