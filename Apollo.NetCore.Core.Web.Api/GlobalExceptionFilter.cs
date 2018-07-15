namespace Apollo.NetCore.Core.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Apollo.NetCore.Core.Exceptions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// A filter that runs after an action has thrown an <see cref="T:System.Exception" />.
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        #region Declarations

        /// <summary>Type used to perform logging.</summary>
        private readonly ILogger logger;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="GlobalExceptionFilter"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Cuando logger es null.</exception>
        public GlobalExceptionFilter(ILoggerFactory logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.logger = logger.CreateLogger("Global Exception Filter");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            HttpStatusCode errorStatusCode = HttpStatusCode.InternalServerError;
            IDictionary<string, string> validationErrors = null;

            // Obtiene los errores de validación de la excepción.
            validationErrors = this.GetValidationErrors(exception);

            // Asigna un statusCode según el tipo de excepción.
            if (exception is NotFoundException)
            {
                errorStatusCode = HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException
                  || exception is BadRequestException)
            {
                errorStatusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is InvalidOperationException
                  || exception is ForbiddenException
                  || exception is UniqueKeyViolationException
                  || exception is ReferentialIntegrityViolationException)
            {
                errorStatusCode = HttpStatusCode.Forbidden;
            }
            else if (exception is UnauthorizedAccessException
                  || exception is UnauthorizedException)
            {
                errorStatusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is ArgumentNullException
                  || exception is ArgumentOutOfRangeException
                  || exception is ArgumentException)
            {
                errorStatusCode = HttpStatusCode.InternalServerError;
            }
            else if (exception is BadGatewayException)
            {
                errorStatusCode = HttpStatusCode.BadGateway;
            }
            else if (exception is TimeoutException)
            {
                errorStatusCode = HttpStatusCode.GatewayTimeout;
            }
            else
            {
                // Otras excepciones.
                errorStatusCode = HttpStatusCode.InternalServerError;
            }

            // Obtiene la instancia de StatusMessage para armar la respuesta de la petición.
            StatusMessage response = StatusMessageBuilder.Create(exception, errorStatusCode, validationErrors);

            // Devuelve el objeto del tipo StatusMessage como resultado de la petición.
            context.Result = new ObjectResult(response) { StatusCode = (int)errorStatusCode, DeclaredType = typeof(StatusMessage) };
            this.logger.LogError("GlobalExceptionFilter", exception);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Devuelve un diccionario con la información de validación extraida del data de la excepción.
        /// Busca recursivamente en las excepciones internas.
        /// </summary>
        /// <param name="exception">Excepción que contiene información de validación.</param>
        /// <returns>Diccionario con la información de validación extraida del data de la excepción.</returns>
        private IDictionary<string, string> GetValidationErrors(Exception exception)
        {
            IDictionary<string, string> validationErrors = null;
            if (exception != null)
            {
                if (exception.Data.Contains(ExDataKey.ValidationErrors))
                {
                    validationErrors = (IDictionary<string, string>)exception.Data[ExDataKey.ValidationErrors];
                }

                IDictionary<string, string> validationErrorsInner = this.GetValidationErrors(exception.InnerException);
                if (validationErrorsInner != null)
                {
                    if (validationErrors != null)
                    {
                        // Hace merge de los diccionarios sin sobreescribir las claves.
                        foreach (KeyValuePair<string, string> pair in validationErrorsInner)
                        {
                            if (!validationErrors.ContainsKey(pair.Key))
                            {
                                validationErrors.Add(pair);
                            }
                        }
                    }
                    else
                    {
                        validationErrors = validationErrorsInner;
                    }
                }
            }

            return validationErrors;
        }

        #endregion
    }
}