namespace Training.Persona.Business.Validators
{
    using FluentValidation;

    using Training.Persona.Entities;

    /// <summary>
    /// Valida el objeto de tipo País.
    /// </summary>
    public class PaisValidator : FluentValidatorBase<Pais>
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PaisValidator"/> class.</summary>
        public PaisValidator()
        {
            // CodigoIata no debe ser null y debe tener 2 caracteres.
            this.RuleFor(t => t.CodigoIata).NotNull().Length(2);

            // Nombre no debe ser null y debe tener entre 1 y 120 caracteres.
            this.RuleFor(t => t.Nombre).NotNull().MinimumLength(1).MaximumLength(120);
        }

        #endregion
    }
}
