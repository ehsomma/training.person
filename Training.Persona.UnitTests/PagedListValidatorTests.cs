// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using FluentValidation.TestHelper;

    using Training.Persona.Business.Validators;
    using Training.Persona.Entities;

    using Xunit;

    public class PagedListValidatorTests
    {
        #region Tests

        // PageIndex debe ser mayor o igual a 1.
        // PageSize debe ser mayor o igual a 1 y menor o igual a 100.
        [Fact]
        public void al_validar_pageList_pageIndex_debe_ser_mayor_o_igual_a_1()
        {
            // Arrange.
            PagedListValidator validator = new Training.Persona.Business.Validators.PagedListValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.PageIndex, new PagedList<object>() { PageIndex = 0 });
            validator.ShouldHaveValidationErrorFor(p => p.PageIndex, new PagedList<object>() { PageIndex = -1 });

            validator.ShouldNotHaveValidationErrorFor(p => p.PageIndex, new PagedList<object>() { PageIndex = 1 });
        }

        [Fact]
        public void al_validar_pageList_PageSize_debe_ser_mayor_o_igual_a_1_y_menor_o_igual_a_100()
        {
            // Arrange.
            PagedListValidator validator = new Training.Persona.Business.Validators.PagedListValidator();

            // Act.

            // Assert.
            validator.ShouldHaveValidationErrorFor(p => p.PageSize, new PagedList<object>() { PageSize = 0 });
            validator.ShouldHaveValidationErrorFor(p => p.PageSize, new PagedList<object>() { PageSize = -1 });
            validator.ShouldHaveValidationErrorFor(p => p.PageSize, new PagedList<object>() { PageSize = 101 });

            validator.ShouldNotHaveValidationErrorFor(p => p.PageSize, new PagedList<object>() { PageSize = 100 });
            validator.ShouldNotHaveValidationErrorFor(p => p.PageSize, new PagedList<object>() { PageSize = 1 });
        }

        #endregion
    }
}