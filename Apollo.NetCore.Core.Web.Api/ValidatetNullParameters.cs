namespace Apollo.NetCore.Core.Web.Api
{
#if false

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using Apollo.NetCore.Core.Exceptions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>Valida parámetros nulos.</summary>
    public class ValidatetNullParameters : IActionFilter
    {
    #region Static Fields

        /// <summary>The not null parameter names.</summary>
        private static readonly ConcurrentDictionary<ActionDescriptor, IList<string>> NotNullParameterNames = new ConcurrentDictionary<ActionDescriptor, IList<string>>();

    #endregion

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
                IDictionary<string, string> validationErrors = (exception.Data[ExDataKey.ValidationErrors] as IDictionary<string, string>);

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

        /// <summary>Devuelve la lista con los nombres de los parametros que no soportan NULL.
        ///     Link: http://stackoverflow.com/questions/17923622/modelstate-isvalid-even-when-it-should-not-be/24525834#24525834.</summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>Los nombres de los parametros que no soportan NULL.</returns>
        private static IList<string> GetNotNullParameterNames(ActionExecutingContext actionContext)
        {
            IList<string> result = NotNullParameterNames.GetOrAdd(
                actionContext.ActionDescriptor,
                descriptor =>
                    descriptor.Parameters
                        .Where(
                            p =>
                                !p.ParameterType    .IsOptional && p.BindingInfo.BindingSource.  .DefaultValue == null && !p.ParameterType.IsValueType
                                && p.ParameterType != typeof(string))
                        .Select(p => p.ParameterName)
                        .ToList());
            return result;
        }

    #endregion
    }

#endif
}