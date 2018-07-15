namespace Training.Persona.Business.Validators
{
    using FluentValidation;

    /// <summary>Contiene un constructor estático para configuraciones globales de FluentValidations.</summary>
    /// <typeparam name="TEntity">Tipo de la entidad.</typeparam>
    public class FluentValidatorBase<TEntity> : AbstractValidator<TEntity>
    {
        #region Constructors

        /// <summary>Initializes static members of the <see cref="FluentValidatorBase"/> class.</summary>
        static FluentValidatorBase()
        {
            // Configura el DisplayNameResolver para que al asignar el mensaje de validación,
            // no inserte espacios en los nombres de las propiedades validadas de acuerdo a PascalCase.
            ValidatorOptions.DisplayNameResolver = (type, member, laloLambda) =>
            {
                if (member != null)
                {
                    return member.Name;
                }

                return null;
            };

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
        }

        #endregion
    }
}