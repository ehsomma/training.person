namespace Training.Persona.Business.Validators
{
    using System;

    using FluentValidation;

    using Training.Persona.Entities;

    /// <summary>
    /// Valida el objeto de tipo Persona.
    /// </summary>
    public class PersonaValidator : FluentValidatorBase<Persona>
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PersonaValidator"/> class.</summary>
        public PersonaValidator()
        {
            // NombreCompleto no debe ser null, no debe estar vacío y no debe contener más de 60 caracteres.
            this.RuleFor(t => t.NombreCompleto).NotNullOrEmpty().MaximumLength(60);

            // EMail no debe ser null, debe tener entre 3 y 320 caracteres y debe ser una dirección válida.
            this.RuleFor(t => t.EMail).NotNull().Length(3, 320).EmailAddress();

            // TotalAhorro debe estar entre -9999999.99 y 9999999.99.
            this.RuleFor(t => t.TotalAhorro).InclusiveBetween(-9999999.99M, 9999999.99M);
            
            // PorcAhorro debe estar entre 0 y 100.
            this.RuleFor(t => t.PorcAhorro).InclusiveBetween(0, 100);
            
            // Lat debe estar entre -90.000000 y 90.000000.
            this.RuleFor(t => t.Lat).InclusiveBetween(-90.000000M, 90.000000M);
            
            // Lon debe estar entre -180.000000 y 180.000000. 
            this.RuleFor(t => t.Lon).InclusiveBetween(-180.000000M, 180.000000M);
            
            // Estado debe pertenecer al enum AccountStatus.
            this.RuleFor(t => t.Estado).IsInEnum();
            
            // FechaNacimiento si existe debe tener una edad entre 0 y 150 años.
            this.RuleFor(t => t.FechaNacimiento).Must(EdadValida).When(t => t.FechaNacimiento > DateTime.MinValue);
            
            // RegionalData no debe ser null.
            this.RuleFor(t => t.RegionalData).NotNull();

            // Valida el RegionalData.
            this.RuleFor(t => t.RegionalData).SetValidator(new RegionalDataValidator());

            // Password no debe ser null, no debe estar vacío y debe tener entre 6 y 120 caracteres.
            this.RuleFor(t => t.Password).NotNullOrEmpty().MinimumLength(6).MaximumLength(120);

            // Valida cada objeto Bien de la lista Bienes.  
            this.RuleFor(t => t.Bienes).SetCollectionValidator(new BienValidator());
        }

        /// <summary>
        /// Valida que la fecha de nacimiento represente una edad válida.
        /// </summary>
        /// <param name="fechaNacimiento">Fecha de Nacimiento.</param>
        /// <returns>Devuelve true si la fecha de nacimiento representa una edad válida.</returns>
        private static bool EdadValida(DateTime fechaNacimiento)
        {
            bool ret = false;

            DateTime today = DateTime.Now;

            int age = today.Year - fechaNacimiento.Year;

            if (today.Month < fechaNacimiento.Month ||
                (today.Month == fechaNacimiento.Month && today.Day < fechaNacimiento.Day))
            {
                age--;
            }

            if (age >= 0 && age <= 150)
            {
                ret = true;
            }

            return ret;
        }

        #endregion
    }
}