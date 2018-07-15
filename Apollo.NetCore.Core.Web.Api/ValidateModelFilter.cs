namespace Apollo.NetCore.Core.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Apollo.NetCore.Core.Exceptions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Valida el modelo antes de entrar al metodo. Si el modelo no es valido lanza una excepción
    /// del tipo EntityValidationException, con los mensjes de validación del modelo en la propiedad
    /// ValidationErrors del objeto Data de la excepción.
    /// </summary>
    public class ValidateModelFilter : IActionFilter
    {
        #region Methods

        /// <summary>
        /// Valida el modelo.
        /// Si el modelo no es válido se termina la ejecución de la petición devolviendo BadRequest y un StatusMessage.
        /// </summary>
        /// <param name="context">Contexto de ejecución.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Verifica que el modelo sea valido.
            if (context.ModelState.IsValid == false)
            {
                // Genera la excepción.
                ValidationException exception = context.ModelState.ToEntityValidationException();
                IDictionary<string, string> validationErrors = exception.Data[ExDataKey.ValidationErrors] as IDictionary<string, string>;

                // Genera un mensaje de respuesta.
                HttpStatusCode errorStatusCode = HttpStatusCode.BadRequest;
                StatusMessage response = StatusMessageBuilder.Create(exception, errorStatusCode, validationErrors);

                // Devuelve el objeto del tipo StatusMessage como resultado de la petición.
                context.Result = new ObjectResult(response) { StatusCode = (int)errorStatusCode, DeclaredType = typeof(StatusMessage) };
            }
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Nothing to do.
        }

        #endregion
    }
}