namespace Training.Persona.Business.Validators
{
    using FluentValidation;

    using Training.Persona.Entities;

    /// <summary>
    /// Valida el objeto de tipo PagedList.
    /// </summary>
    public class PagedListValidator : FluentValidatorBase<PagedList<object>>
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PagedListValidator"/> class.</summary>
        public PagedListValidator()
        {
            // PageIndex debe ser mayor o igual a 1.
            this.RuleFor(t => t.PageIndex).GreaterThanOrEqualTo(1);

            // PageSize debe ser mayor o igual a 1 y menor o igual a 100.
            this.RuleFor(t => t.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }

        #endregion
    }
}
