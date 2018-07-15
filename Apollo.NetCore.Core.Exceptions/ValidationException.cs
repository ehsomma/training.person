namespace Apollo.NetCore.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exepción para representar errores de validación.
    /// </summary>
    public class ValidationException : Exception
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationException(string message)
            : base(message ?? "Validation failed. See 'ValidationErrors' property for more details.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidationException(string message, Exception innerException)
            : base(message ?? "Validation failed. See 'ValidationErrors' property for more details.", innerException)
        {
        }

        #endregion
    }
}
