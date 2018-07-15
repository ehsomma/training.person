namespace Apollo.NetCore.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exepción para representar errores de http Forbidden.
    /// </summary>
    public class ForbiddenException : ApiException
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        public ForbiddenException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ForbiddenException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}