// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using FluentValidation.TestHelper;

    using Training.Persona.Business.Validators;
    using Training.Persona.Entities;

    using Xunit;

    public class RegionalDataValidatorTests
    {
        #region Tests

        [Fact]
        public void al_validar_regionalData_dateFormat_si_no_es_null_no_debe_contener_más_de_10_caracteres()
        {
            // Arrange.
            RegionalDataValidator validator = new Training.Persona.Business.Validators.RegionalDataValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(r => r.DateFormat, new RegionalData() { DateFormat = string.Empty.PadRight(11, 'X') });

            validator.ShouldNotHaveValidationErrorFor(r => r.DateFormat, new RegionalData() { DateFormat = null });
            validator.ShouldNotHaveValidationErrorFor(r => r.DateFormat, new RegionalData() { DateFormat = string.Empty.PadRight(10, 'X') });
        }

        [Fact]
        public void al_validar_regionalData_timeFormat_si_no_es_null_no_debe_tener_más_de_15_caracteres()
        {
            // Arrange.
            RegionalDataValidator validator = new Training.Persona.Business.Validators.RegionalDataValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(r => r.TimeFormat, new RegionalData() { TimeFormat = string.Empty.PadRight(16, 'X') });

            validator.ShouldNotHaveValidationErrorFor(r => r.TimeFormat, new RegionalData() { TimeFormat = null });
            validator.ShouldNotHaveValidationErrorFor(r => r.TimeFormat, new RegionalData() { TimeFormat = string.Empty.PadRight(15, 'X') });
        }

        [Fact]
        public void al_validar_regionalData_languageCode_si_no_es_null_no_debe_tener_mas_de_5_caracteres()
        {
            // Arrange.
            RegionalDataValidator validator = new Training.Persona.Business.Validators.RegionalDataValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(r => r.LanguageCode, new RegionalData() { LanguageCode = string.Empty.PadRight(6, 'X') });

            validator.ShouldNotHaveValidationErrorFor(r => r.LanguageCode, new RegionalData() { LanguageCode = null });
            validator.ShouldNotHaveValidationErrorFor(r => r.LanguageCode, new RegionalData() { LanguageCode = string.Empty.PadRight(5, 'X') });
        }

        [Fact]
        public void al_validar_regionalData_countryCode_no_debe_ser_null_y_debe_tener_2_caracteres()
        {
            // Arrange.
            RegionalDataValidator validator = new Training.Persona.Business.Validators.RegionalDataValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(r => r.CountryCode, new RegionalData() { CountryCode = null });
            validator.ShouldHaveValidationErrorFor(r => r.CountryCode, new RegionalData() { CountryCode = "X" });
            validator.ShouldHaveValidationErrorFor(r => r.CountryCode, new RegionalData() { CountryCode = "XXX" });

            validator.ShouldNotHaveValidationErrorFor(r => r.CountryCode, new RegionalData() { CountryCode = "XX" });
        }

        #endregion
    }
}