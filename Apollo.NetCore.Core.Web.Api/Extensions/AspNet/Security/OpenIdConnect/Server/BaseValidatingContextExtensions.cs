// ReSharper disable once CheckNamespace
namespace AspNet.Security.OpenIdConnect.Server
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Apollo.NetCore.Core.Exceptions;
    using Apollo.NetCore.Core.Web.Api;
    /*
    /// <summary>
    /// Extensiones para la calse BaseValidatingContext.
    /// </summary>
    public static class BaseValidatingContextExtensions
    {
        #region Methods

        /// <summary>
        /// Rechaza la petición y configura el mensaje y código de respuesta para la petición.
        /// </summary>
        /// <param name="context">Contexto de la petición.</param>
        /// <param name="code">Código de respuesta.</param>
        /// <param name="error">Mensaje o código del error.</param>
        /// <param name="validationErrors">The validation errors.</param>
        /// <param name="description">Descrición del error.</param>
        /// <param name="uri">Uri de respuesta.</param>
        /// <returns>Verdadero o falso.</returns>
        public static bool RejectWithCustomError(
            this BaseValidatingContext context,
            HttpStatusCode code,
            string error = null,
            IDictionary<string, string> validationErrors = null,
            string description = null,
            string uri = null)
        {
            Exception exception = new Exception($"{description}");
            exception.Data[ExDataKey.ErrorCode] = error;
            StatusMessage statusMessage = StatusMessageBuilder.Create(exception, code, validationErrors);
            context.HttpContext.Items[ErrorFlag.CustomError] = statusMessage;
            // return context.Reject(error, description, uri);
            context.Reject(error, description, uri);
            return true;
        }

        #endregion
    }
    */
}