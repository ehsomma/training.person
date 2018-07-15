// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    using Apollo.NetCore.Core.Exceptions;

    using global::Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>Extensiones del diccionario de validaciones del modelo.</summary>
    public static class ModelStateDictionaryExtension
    {
        #region Methods

        /// <summary>
        /// Genera una excepción con los datos de validación del modelo.
        /// </summary>
        /// <param name="modelState">The model state.</param>
        /// <returns>Una excepción de validación. <see cref="ValidationException"/></returns>
        [SuppressMessage("Apollo.StyleCop.ApolloRules.ReadabilityRules", "SC2101:FunctionMustHaveOnlyOneReturn", Justification = "Return valido, se utiliza en una expresión lambda.")]
        public static ValidationException ToEntityValidationException(this ModelStateDictionary modelState)
        {
            // Crea la excepción de validación.
            ValidationException ret = new ValidationException("Validation failed for one or more entities. See 'ValidationErrors' from for more details.");

            // Prepara los mensajes de error para agregar a la excepción.
            // FIX: 2016-03-30 - DAE - Se omiten los elementos del diccionario que no tienen errores.
            // Se generaba un registro de error vacio en los parametros del path aunque estuvieran correctos.
            IDictionary<string, string> errorsDict = modelState
                                                        .Where(kvp => kvp.Value.Errors.Count != 0)
                                                            .ToDictionary(
                                                                kvp => kvp.Key,
                                                                kvp =>
                                                                {
                                                                    StringBuilder sb = new StringBuilder();
                                                                    foreach (ModelError error in kvp.Value.Errors)
                                                                    {
                                                                        if (error.Exception != null)
                                                                        {
                                                                            sb.Append(error.ErrorMessage)
                                                                                .AppendFormat(" ({0})", error.Exception.Message)
                                                                                .AppendLine();
                                                                        }
                                                                        else
                                                                        {
                                                                            sb.AppendLine(error.ErrorMessage);
                                                                        }
                                                                    }

                                                                    return sb.ToString().Trim();
                                                                });

            // Agrega los mensajes de validación.
            ret.Data.Add(ExDataKey.ValidationErrors, errorsDict);

            return ret;
        }

        #endregion
    }
}