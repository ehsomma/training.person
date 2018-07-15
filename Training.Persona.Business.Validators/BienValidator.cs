namespace Training.Persona.Business.Validators
{
    using FluentValidation;

    using Training.Persona.Entities;

    /// <summary>
    /// Valida el objeto de tipo Bien.
    /// </summary>
    public class BienValidator : FluentValidatorBase<Bien>
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="BienValidator"/> class.</summary>
        public BienValidator()
        {
            // Tipo no debe ser null, no debe estar vacío y no debe contener más de 60 caracteres.
            this.RuleFor(t => t.Tipo).NotNull().NotEmpty().MaximumLength(60);

            // Descripción no debe ser null, no debe estar vacío y no debe contener más de 255 caracteres.
            this.RuleFor(t => t.Descripcion).NotNullOrEmpty().MaximumLength(255);

            // Valor debe ser entre -9999999,99 y 9999999,99.
            this.RuleFor(t => t.Valor).InclusiveBetween(-9999999.99M, 9999999.99M);
        }

        #endregion
    }
}
