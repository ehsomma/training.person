// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using FluentValidation.TestHelper;

    using Training.Persona.Business.Validators;
    using Training.Persona.Entities;

    using Xunit;

    public class PaisValidatorTests
    {
        #region Tests

        [Fact]
        public void al_validar_pais_codigoIata_no_debe_ser_null_no_debe_estar_vacio_y_debe_tener_2_caracteres()
        {
            // Arrange.
            PaisValidator validator = new Training.Persona.Business.Validators.PaisValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.CodigoIata, new Pais() { CodigoIata = null });
            validator.ShouldHaveValidationErrorFor(p => p.CodigoIata, new Pais() { CodigoIata = string.Empty });
            validator.ShouldHaveValidationErrorFor(p => p.CodigoIata, new Pais() { CodigoIata = "X" });
            validator.ShouldHaveValidationErrorFor(p => p.CodigoIata, new Pais() { CodigoIata = "XXX" });

            validator.ShouldNotHaveValidationErrorFor(p => p.CodigoIata, new Pais() { CodigoIata = "XX" });
        }

        [Fact]
        public void al_validar_pais_nombre_no_debe_ser_null_no_debe_estar_vacío_no_debe_tener_menos_de_4_ni_más_de_120_caracteres()
        {
            // Arrange.
            PaisValidator validator = new Training.Persona.Business.Validators.PaisValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.Nombre, new Pais() { Nombre = null });
            validator.ShouldHaveValidationErrorFor(p => p.Nombre, new Pais() { Nombre = string.Empty });
            validator.ShouldHaveValidationErrorFor(p => p.Nombre, new Pais() { Nombre = string.Empty.PadRight(121, 'X') });

            validator.ShouldNotHaveValidationErrorFor(p => p.Nombre, new Pais() { Nombre = "XXXX" });
        }

        #endregion
    }
}