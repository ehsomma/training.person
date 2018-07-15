// ReSharper disable once CheckNamespace
namespace FluentValidation
{
    using System;
    using System.Collections.Generic;

    using Apollo.NetCore.Core.Exceptions;

    using FluentValidation.Results;

    /// <summary>Extension methods for FluentValidations.</summary>
    public static class DefaultValidatorExtensions
    {
        #region Methods

        /// <summary>Performs validation and then throws an ValidationException if validation fails.</summary>
        /// <typeparam name="TEntity">The entity type to validate.</typeparam>
        /// <param name="validator">The validator class.</param>
        /// <param name="entity">The entity to validate.</param>
        public static void ValidateAndThrowExeption<TEntity>(this IValidator<TEntity> validator, TEntity entity)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            // Realiza la validación.
            FluentValidation.Results.ValidationResult validationResult = validator.Validate(entity);

            // Si la validación falla, lanza una excepción con la lista de errores.
            if (!validationResult.IsValid)
            {
                IDictionary<string, string> validationErrors = new Dictionary<string, string>();

                foreach (ValidationFailure validationFailure in validationResult.Errors)
                {
                    string message = string.Format("Error message: {0} Attempted value: {1}", validationFailure.ErrorMessage, validationFailure.AttemptedValue);
                    if (validationErrors.ContainsKey(validationFailure.PropertyName))
                    {
                        validationErrors[validationFailure.PropertyName] = string.Format("{0} {0}", validationErrors[validationFailure.PropertyName], message);
                    }
                    else
                    {
                        validationErrors.Add(validationFailure.PropertyName, message);
                    }
                }

                Apollo.NetCore.Core.Exceptions.ValidationException ex = new Apollo.NetCore.Core.Exceptions.ValidationException(null);
                ex.Data[ExDataKey.ValidationErrors] = validationErrors;
                throw ex;
            }
        }

        #endregion
    }
}