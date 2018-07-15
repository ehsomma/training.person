namespace Apollo.NetCore.Core.Exceptions
{
    /// <summary>
    /// Constantes para keys del data de excepciones.
    /// </summary>
    public class ExDataKey
    {
        #region Declarations

        /// <summary>Key del Data de Excepciones que identifica la colección de errores de validación.</summary>
        public const string ValidationErrors = "ValidationErrors";

        /// <summary>Key del Data de Excepciones que identifica el código unico de error de una exepción (permite mostrarla al usuario y ubicar la exepción rápidamente en el log de la aplicación).</summary>
        public const string ErrorUniqueId = "ErrorUniqueId";

        /// <summary>Key del Data de Excepciones que identifica el código interno de error asignado desde negocio para que el desarrollados pueda tomar deciciones de acuerdo a este código.</summary>
        public const string ErrorCode = "ErrorCode";

        /// <summary>Key del Data de Excepciones que identifica el constraint name en una violación de UK o FK.</summary>
        public const string ConstraintName = "ConstraintName";

        #endregion
    }
}