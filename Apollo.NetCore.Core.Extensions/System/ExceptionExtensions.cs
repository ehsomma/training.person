// ReSharper disable once CheckNamespace
namespace System
{
    using Apollo.NetCore.Core.Exceptions;

    /// <summary>
    /// Extensiones para todas las instancias de Exception.
    /// </summary>
    public static class ExceptionExtensions
    {
        #region Declarations

        /// <summary>
        /// Separadores para el Split.
        /// </summary>
        private static readonly string[] Separator = new string[] { "\r\n" };

        #endregion

        #region Methods

        /// <summary>
        /// Extrae de la excepción especificada los datos más relevantes.
        /// </summary>
        /// <remarks>
        /// Las excepciones completas pasadas a texto pesan como 24MB.
        /// </remarks>
        /// <param name="exception">La excepción sobre la cual se va a hacer el resumen.</param>
        /// <returns>Un objeto con datos resumidos de la excepción.</returns>
        public static object ToResumeLog(this Exception exception)
        {
            object ret = null;
            if (exception != null)
            {
                Type type = exception.GetType();
                ret = new
                {
                    Message = exception.Message,
                    Class = type.Name,
                    Type = type.FullName,
                    StackTrace = exception.StackTrace.Split(Separator, StringSplitOptions.RemoveEmptyEntries),
                    InnerException = ToResumeLog(exception.InnerException),
                    Data = exception.Data,
                    Source = exception.Source
                };
            }

            return ret;
        }

        /// <summary>Add an errorCode to the exception.</summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="exceptionErrorCode">The error code to add. (Use class ExceptionErrorCode).</param>
        /// <returns>The <see cref="Exception"/>.</returns>
        public static Exception AddErrorCode(this Exception ex, string exceptionErrorCode)
        {
            // Agrega el errorCode al data. (si existe, lo reemplaza).
            ex.Data[ExDataKey.ErrorCode] = exceptionErrorCode;

            return ex;
        }

        /// <summary>Get an errorCode from the data dictionary of the exception.</summary>
        /// <param name="ex">The exeption.</param>
        /// <returns>The ErrorCode.</returns>
        public static string GetErrorCode(this Exception ex)
        {
            string ret;

            try
            {
                ret = ex.Data[ExDataKey.ErrorCode].ToString();
            }
            catch
            {
                // La excepción no tiene ErrorCode.
                ret = string.Empty;
            }

            return ret;
        }

        #endregion
    }
}