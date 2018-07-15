namespace Training.Persona.Business.Validators
{
    using FluentValidation;

    using Training.Persona.Entities;

    /// <summary>
    /// Valida el objeto de tipo RegionalData.
    /// </summary>
    public class RegionalDataValidator : FluentValidatorBase<RegionalData>
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="RegionalDataValidator"/> class.</summary>
        public RegionalDataValidator()
        {
            // DateFormat si no es null no debe contener más de 10 caracteres.
            this.RuleFor(t => t.DateFormat).MaximumLength(10).When(t => t.DateFormat != null);

            // TimeFormat si no es null no debe tener más de 15 caracteres.
            this.RuleFor(t => t.TimeFormat).MaximumLength(15).When(t => t.TimeFormat != null);

            // LanguageCode si no es null no debe tener mas de 5 caracteres.
            this.RuleFor(t => t.LanguageCode).MaximumLength(5).When(t => t.LanguageCode != null);

            // CountryCode no debe ser null y debe tener 2 caracteres.
            this.RuleFor(t => t.CountryCode).NotNull().Length(2);
        }

        #endregion
    }
}
