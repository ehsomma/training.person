// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using FluentValidation.TestHelper;

    using Training.Persona.Business.Validators;
    using Training.Persona.Entities;

    using Xunit;

    public class BienValidatorTests
    {
        #region Tests

        [Fact]
        public void al_validar_bien_tipo_no_debe_ser_null_no_debe_estar_vacío_y_no_debe_contener_más_de_60_caracteres()
        {
            // Arrange.
            BienValidator validator = new Training.Persona.Business.Validators.BienValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(b => b.Tipo, new Bien() { Tipo = null });
            validator.ShouldHaveValidationErrorFor(b => b.Tipo, new Bien() { Tipo = string.Empty });
            validator.ShouldHaveValidationErrorFor(b => b.Tipo, new Bien() { Tipo = string.Empty.PadRight(61, 'X') });

            validator.ShouldNotHaveValidationErrorFor(b => b.Tipo, new Bien() { Tipo = "X" });
        }

        [Fact]
        public void al_validar_bien_descripción_no_debe_ser_null_no_debe_estar_vacío_y_no_debe_contener_más_de_255_caracteres()
        {
            // Arrange.
            BienValidator validator = new Training.Persona.Business.Validators.BienValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(b => b.Descripcion, new Bien() { Descripcion = null });
            validator.ShouldHaveValidationErrorFor(b => b.Descripcion, new Bien() { Descripcion = string.Empty });
            validator.ShouldHaveValidationErrorFor(b => b.Descripcion, new Bien() { Descripcion = string.Empty.PadRight(256, 'X') });

            validator.ShouldNotHaveValidationErrorFor(b => b.Descripcion, new Bien() { Descripcion = "X" });
        }

        [Fact]
        public void al_validar_bien_valor_debe_ser_entre_menos9999999p99_y_9999999p99()
        {
            // Arrange.
            BienValidator validator = new Training.Persona.Business.Validators.BienValidator();

            decimal minValue = -9999999.99M;
            decimal maxValue = 9999999.99M;

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(b => b.Valor, new Bien() { Valor = minValue - 0.01M });
            validator.ShouldHaveValidationErrorFor(b => b.Valor, new Bien() { Valor = maxValue + 0.1M });

            validator.ShouldNotHaveValidationErrorFor(b => b.Valor, new Bien() { Valor = minValue });
            validator.ShouldNotHaveValidationErrorFor(b => b.Valor, new Bien() { Valor = maxValue });
            validator.ShouldNotHaveValidationErrorFor(b => b.Valor, new Bien() { Valor = 500000.00M });
            validator.ShouldNotHaveValidationErrorFor(b => b.Valor, new Bien() { Valor = 500000 });
        }

        #endregion
    }
}