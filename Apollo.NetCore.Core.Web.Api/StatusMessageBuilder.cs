namespace Apollo.NetCore.Core.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;

    using Apollo.NetCore.Core.Exceptions;

    /// <summary>
    /// Crea una instancia StatusMessage.
    /// </summary>
    public static class StatusMessageBuilder
    {
        #region Methods

        /// <summary>
        /// Crea una instancia StatusMessage en base a la excepción y los parametros especificados.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="errorStatusCode">The HttpStatusCode.</param>
        /// <param name="validationErrors">The validation errors.</param>
        /// <returns>The <see cref="StatusMessage"/>.</returns>
        public static StatusMessage Create(Exception exception, HttpStatusCode errorStatusCode, IDictionary<string, string> validationErrors)
        {
            // Obtiene el ErrorCode de la excepción.
            string errorCode = GetInternalErrorCode(exception);
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                if (validationErrors != null)
                {
                    errorCode = "ERR.VALIDATION";
                }
                else
                {
                    errorCode = "ERR";
                }
            }

            // Obtiene el ErrorUniqueId de la excepción, si no tiene genera uno.
            string errorUniqueId = GetErrorUniqueId(exception);
            if (string.IsNullOrWhiteSpace(errorUniqueId))
            {
                errorUniqueId = Guid.NewGuid().ToString("N");
            }

            StatusMessage ret = new StatusMessage
            {
                Code = Convert.ToInt32(errorStatusCode).ToString(),
                Descrip = Enum.GetName(typeof(HttpStatusCode), errorStatusCode),
                ErrorCode = errorCode,
                ErrorUniqueId = errorUniqueId,
                Message = exception.Message,
                TimeStamp = DateTime.UtcNow,
                ValidationErrors = validationErrors
            };

            return ret;
        }

        /// <summary>
        /// Crea una instancia StatusMessage en base los parametros especificados.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="errorStatusCode">The HttpStatusCode.</param>
        /// <param name="errorCode">Error code for business rules treatment.</param>
        /// <returns>The <see cref="StatusMessage"/>.</returns>
        public static StatusMessage Create(string message, HttpStatusCode errorStatusCode, string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                errorCode = "ERR";
            }

            StatusMessage ret = new StatusMessage
            {
                Code = Convert.ToInt32(errorStatusCode).ToString(),
                Descrip = Enum.GetName(typeof(HttpStatusCode), errorStatusCode),
                ErrorCode = errorCode,
                ErrorUniqueId = null,
                Message = message,
                TimeStamp = DateTime.UtcNow,
                ValidationErrors = null
            };

            return ret;
        }

        /// <summary>
        /// Intenta obtener un código de error (registrado previamente desde negocio) en el data de la
        /// excepción especificada, si lo encuentra, lo devuelve. De lo contrario devuelve null.
        /// Busca recursivamente en las excepciones internas.
        /// </summary>
        /// <param name="exception">La excepción en donde se va a buscar el código de error.</param>
        /// <returns>El código de error para la excepción o null.</returns>
        private static string GetErrorUniqueId(Exception exception)
        {
            string errorUniqueId = null;
            if (exception != null)
            {
                object dataValue = exception.Data.GetValueIgnoreCase(ExDataKey.ErrorUniqueId);
                if (dataValue != null)
                {
                    errorUniqueId = dataValue.ToString();
                }
                else
                {
                    errorUniqueId = GetErrorUniqueId(exception.InnerException);
                    AggregateException aex = exception as AggregateException;
                    if (aex != null)
                    {
                        int i = 0;
                        while (i < aex.InnerExceptions.Count && string.IsNullOrWhiteSpace(errorUniqueId))
                        {
                            errorUniqueId = GetErrorUniqueId(aex.InnerExceptions[i]);
                            i++;
                        }
                    }
                }
            }

            return errorUniqueId;
        }

        /// <summary>
        /// Intenta obtener un código de error (registrado previamente desde negocio) en el data de la
        /// excepción especificada, si lo encuentra, lo devuelve. De lo contrario devuelve empty.
        /// </summary>
        /// <param name="exception">La excepción en donde se va a buscar el código de error.</param>
        /// <returns>El código de error para la excepción o null.</returns>
        private static string GetInternalErrorCode(Exception exception)
        {
            string errorCode = null;

            if (exception != null)
            {
                // Intenta obtener el errorCode del data de la excepción.
                if (exception.Data.Contains(ExDataKey.ErrorCode))
                {
                    object dataValue = exception.Data[ExDataKey.ErrorCode];
                    errorCode = dataValue == null ? string.Empty : dataValue.ToString();
                }
                else
                {
                    errorCode = GetInternalErrorCode(exception.InnerException);
                }
            }

            return errorCode;
        }

        #endregion
    }
}