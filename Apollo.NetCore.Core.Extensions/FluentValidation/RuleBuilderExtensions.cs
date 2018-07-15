// ReSharper disable once CheckNamespace
namespace FluentValidation
{
    /// <summary>Extension methods for FluentValidations.</summary>
    public static class RuleBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Valida que no sea null o vacio.
        /// </summary>
        /// <typeparam name="T">The T.</typeparam>
        /// <typeparam name="TProperty">The TProperty.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="errorMessage">Mensaje de error a mostrar.</param>
        /// <returns>True, si pasa la validación. De lo contrario, false.</returns>
        public static FluentValidation.IRuleBuilderOptions<T, TProperty> NotNullOrEmpty<T, TProperty>(this FluentValidation.IRuleBuilder<T, TProperty> builder, string errorMessage = null)
        {
            /*
            IRuleBuilderOptions<T, TProperty> ret;

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                ret = builder.NotNull()
                             .NotEmpty();
                errorMessage = "'{PropertyName}' is required.";
            }
            else
            {
                ret = builder.NotNull().WithMessage(errorMessage)
                             .NotEmpty().WithMessage(errorMessage);
            }
            */

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = "'{PropertyName}' Should not be null or empty.";
            }

            IRuleBuilderOptions<T, TProperty> ret =
                builder.NotNull().WithMessage(errorMessage)
                    .NotEmpty().WithMessage(errorMessage);

            return ret;
        }

        #endregion
    }
}