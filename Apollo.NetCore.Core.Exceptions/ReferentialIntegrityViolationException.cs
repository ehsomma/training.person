namespace Apollo.NetCore.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exception producida al intentar violar una cláusula de integridad referencial en base de datos.
    /// </summary>
    public class ReferentialIntegrityViolationException : DataException
    {
        #region Declaration

        /// <summary>Nombre de la cláusula violada para índices y claves foráneas.</summary>
        private readonly string constraintName;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferentialIntegrityViolationException"/> class.
        /// </summary>
        public ReferentialIntegrityViolationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferentialIntegrityViolationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ReferentialIntegrityViolationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferentialIntegrityViolationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ReferentialIntegrityViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferentialIntegrityViolationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="constraintName">Nombre de la cláusula violada para índices y claves foráneas.</param>
        public ReferentialIntegrityViolationException(string message, Exception innerException, string constraintName)
            : base(message, innerException)
        {
            this.constraintName = constraintName;
        }

        #endregion

        #region Properties

        /// <summary>Nombre de la cláusula violada para índices y claves foráneas.</summary>
        public string ConstraintName
        {
            get
            {
                return this.constraintName;
            }
        }

        #endregion
    }
}