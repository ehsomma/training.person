// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using System;
    using System.Linq;

    using FluentValidation.TestHelper;

    using Training.Persona.Business.Validators;
    using Training.Persona.Entities;

    using Xunit;

    public class PersonaValidatorTests
    {
        #region Tests

        [Fact]
        public void al_validar_persona_nombreCompleto_no_debe_ser_null_no_debe_estar_vacío_y_no_debe_contener_más_de_60_caracteres()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.NombreCompleto, new Persona() { NombreCompleto = null });
            validator.ShouldHaveValidationErrorFor(p => p.NombreCompleto, new Persona() { NombreCompleto = string.Empty });
            validator.ShouldHaveValidationErrorFor(p => p.NombreCompleto, new Persona() { NombreCompleto = string.Empty.PadRight(61, 'X') });

            validator.ShouldNotHaveValidationErrorFor(p => p.NombreCompleto, new Persona() { NombreCompleto = "X" });
        }

        [Fact]
        public void al_validar_persona_email_no_debe_ser_null_debe_tener_entre_3_y_320_caracteres_y_debe_ser_una_dirección_válida()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = null });
            validator.ShouldHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = "XX" });
            validator.ShouldHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = string.Empty.PadRight(321, 'X') });
            validator.ShouldHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = "x@@com" });
            validator.ShouldHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = "xcom" });
            validator.ShouldHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = "x@x$" });

            validator.ShouldNotHaveValidationErrorFor(p => p.EMail, new Persona() { EMail = "x@x.com" });
        }

        [Fact]
        public void al_validar_persona_totalAhorro_debe_estar_entre_menos9999999p99_y_9999999p99()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            decimal minValue = -9999999.99M;
            decimal maxValue = 9999999.99M;

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.TotalAhorro, new Persona() { TotalAhorro = minValue - 0.01M });
            validator.ShouldHaveValidationErrorFor(p => p.TotalAhorro, new Persona() { TotalAhorro = maxValue + 0.1M });

            validator.ShouldNotHaveValidationErrorFor(p => p.TotalAhorro, new Persona() { TotalAhorro = minValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.TotalAhorro, new Persona() { TotalAhorro = maxValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.TotalAhorro, new Persona() { TotalAhorro = 500000.00M });
            validator.ShouldNotHaveValidationErrorFor(p => p.TotalAhorro, new Persona() { TotalAhorro = 500000 });
        }

        [Fact]
        public void al_validar_persona_porcAhorro_debe_estar_entre_0_y_100()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            decimal minValue = 0.00m;
            decimal maxValue = 100.00m;

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.PorcAhorro, new Persona() { PorcAhorro = minValue - 0.01m });
            validator.ShouldHaveValidationErrorFor(p => p.PorcAhorro, new Persona() { PorcAhorro = maxValue + 0.1m });

            validator.ShouldNotHaveValidationErrorFor(p => p.PorcAhorro, new Persona() { PorcAhorro = minValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.PorcAhorro, new Persona() { PorcAhorro = maxValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.PorcAhorro, new Persona() { PorcAhorro = 50.01M });
            validator.ShouldNotHaveValidationErrorFor(p => p.PorcAhorro, new Persona() { PorcAhorro = 50 });
        }

        [Fact]
        public void al_validar_persona_lat_debe_estar_entre_menos90p000000_y_90p000000()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            decimal minValue = -90.000000m;
            decimal maxValue = 90.000000m;

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.Lat, new Persona() { Lat = minValue - 0.0000001m });
            validator.ShouldHaveValidationErrorFor(p => p.Lat, new Persona() { Lat = maxValue + 0.0000001m });

            validator.ShouldNotHaveValidationErrorFor(p => p.Lat, new Persona() { Lat = minValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.Lat, new Persona() { Lat = maxValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.Lat, new Persona() { Lat = 45.0000001M });
            validator.ShouldNotHaveValidationErrorFor(p => p.Lat, new Persona() { Lat = 45 });
        }

        [Fact]
        public void al_validar_persona_lon_debe_estar_entre_menos180p000000_y_180p000000()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            decimal minValue = -180.000000m;
            decimal maxValue = 180.000000m;

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.Lon, new Persona() { Lon = minValue - 0.0000001m });
            validator.ShouldHaveValidationErrorFor(p => p.Lon, new Persona() { Lon = maxValue + 0.0000001m });

            validator.ShouldNotHaveValidationErrorFor(p => p.Lon, new Persona() { Lon = minValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.Lon, new Persona() { Lon = maxValue });
            validator.ShouldNotHaveValidationErrorFor(p => p.Lon, new Persona() { Lon = 90.0000001M });
            validator.ShouldNotHaveValidationErrorFor(p => p.Lon, new Persona() { Lon = 90 });
        }

        [Fact]
        public void al_validar_persona_estado_debe_pertenecer_al_enum_AccountStatus()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.Estado, new Persona());
            validator.ShouldHaveValidationErrorFor(p => p.Estado, new Persona() { Estado = Enum.GetValues(typeof(AccountStatus)).Cast<AccountStatus>().Last() + 1 });

            foreach (AccountStatus accountStatus in Enum.GetValues(typeof(AccountStatus)))
            {
                validator.ShouldNotHaveValidationErrorFor(p => p.Estado, new Persona() { Estado = accountStatus });
            }
        }

        [Fact]
        public void al_validar_persona_fechaNacimiento_si_existe_debe_tener_una_edad_entre_0_y_150_años()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.FechaNacimiento, new Persona() { FechaNacimiento = DateTime.Now.AddYears(-151) });
            validator.ShouldHaveValidationErrorFor(p => p.FechaNacimiento, new Persona() { FechaNacimiento = DateTime.Now.AddDays(1) });

            validator.ShouldNotHaveValidationErrorFor(p => p.FechaNacimiento, new Persona());
            validator.ShouldNotHaveValidationErrorFor(p => p.FechaNacimiento, new Persona() { FechaNacimiento = DateTime.Now.AddYears(-150) });
            validator.ShouldNotHaveValidationErrorFor(p => p.FechaNacimiento, new Persona() { FechaNacimiento = DateTime.Now });
        }

        [Fact]
        public void al_validar_persona_password_no_debe_ser_null_no_debe_estar_vacío_y_debe_tener_entre_6_y_120_caracteres()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.Password, new Persona() { Password = null });
            validator.ShouldHaveValidationErrorFor(p => p.Password, new Persona() { Password = string.Empty });
            validator.ShouldHaveValidationErrorFor(p => p.Password, new Persona() { Password = string.Empty.PadRight(5, 'X') });
            validator.ShouldHaveValidationErrorFor(p => p.Password, new Persona() { Password = string.Empty.PadRight(121, 'X') });

            validator.ShouldNotHaveValidationErrorFor(p => p.Password, new Persona() { Password = "XXXXXX" });
        }

        [Fact]
        public void al_validar_persona_regionalData_no_debe_ser_null()
        {
            // Arrange.
            PersonaValidator validator = new Training.Persona.Business.Validators.PersonaValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.RegionalData, new Persona() { RegionalData = null });

            validator.ShouldNotHaveValidationErrorFor(p => p.RegionalData.DateFormat, new Persona() { RegionalData = new RegionalData() });
        }

        #endregion
    }
}