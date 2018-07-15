namespace Apollo.NetCore.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exepción base para representar errores de http InternalServerError.
    /// </summary>
    public class InternalServerErrorException : ApiException
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        public InternalServerErrorException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InternalServerErrorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}