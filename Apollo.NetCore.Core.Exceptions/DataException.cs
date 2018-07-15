namespace Apollo.NetCore.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exepción base para representar errores de acceso a datos.
    /// </summary>
    public class DataException : Exception
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        public DataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}