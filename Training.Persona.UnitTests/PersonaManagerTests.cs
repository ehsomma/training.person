// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using System;
    using System.Collections;

    using Apollo.NetCore.Core.Exceptions;

    using FluentAssertions;

    using Moq;

    using Training.Persona.Business;
    using Training.Persona.Business.Validators;
    using Training.Persona.Data.Interfaces;
    using Training.Persona.Entities;

    using Xunit;

    public class PersonaManagerTests
    {
        #region Tests

        [Fact]
        public void al_insertar_una_nueva_persona_con_datos_válidos_debe_devolver_la_persona_con_un_id_mayor_a_cero()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Persona>())).Returns(PersonaMockGenerator.Insert(persona, 1));

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            persona = personaManager.Insert(persona);

            // Assert.
            persona.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public void al_insertar_una_nueva_persona_con_email_existente_debe_devolver_una_excepción_UniqueKeyViolationException_en_donde_el_data_contenga_una_key_ErrorCode_con_valor_ERR_PERSONA_EMAILEXIST()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            UniqueKeyViolationException ukvex = new UniqueKeyViolationException("xxx UK_Personas_EMail xxx.");

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Persona>())).Throws(ukvex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Insert(persona);

            // Assert.
            action.Should().ThrowExactly<UniqueKeyViolationException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPersonaEmailExist));
        }

        [Fact]
        public void al_insertar_una_nueva_persona_con_valor_null_debe_devolver_una_excepción_de_tipo_ArgumentNullException_conteniendo_persona_en_la_propiedad_Message()
        {
            // Arrange.
            ArgumentNullException anex = new ArgumentNullException();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Persona>())).Throws(anex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Insert(null);

            // Assert.
            action.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain("persona");
        }

        [Fact]
        public void al_insertar_una_persona_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepción_debe_contener_la_key_Persona()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Persona>())).Throws(ex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Insert(persona);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                ////.And.Data.Contains("Persona"); // No funciona de esta manera (da siempre OK).
                .And.Data["Persona"].Should().NotBeNull();
        }

        [Fact]
        public void al_ingresar_una_persona_en_donde_observaciones_contenga_post_EPP_debe_devolver_una_excepción_de_tipo_ValidationException_en_donde_el_data_contenga_una_key_ErrorCode_ERR_PERSONA_PRUEBAVALIDACIONBUSINESS()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                },
                Obs = "Observaciones que contienen la expresión \"post_EPP\" que no es válida."
            };

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Persona>()));

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Insert(persona);

            // Assert.
            action.Should().ThrowExactly<ValidationException>()
                .And.Data.Contains(new DictionaryEntry() { Key = "ErrorCode", Value = ExErrorCode.ErrPersonaPruebaValidacionBusiness });
        }

        [Fact]
        public void al_actualizar_las_observaciones_de_una_persona_debe_devolver_la_persona_con_las_observaciones_actualizadas()
        {
            // Arrange.
            string observacionOriginal = "Notas varias originales.";
            string observacionActualizada = "Notas varias actualizadas.";

            Persona persona = new Persona()
            {
                Id = 1,
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                },
                Obs = observacionOriginal
            };

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Persona>())).Returns(PersonaMockGenerator.UpdateObservacion(persona, observacionActualizada));

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            persona = personaManager.Update(persona);

            // Assert.
            persona.Obs.Should().BeEquivalentTo(observacionActualizada);
        }

        [Fact]
        public void al_actualizar_una_persona_que_no_se_encuentre_en_el_sistema_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_la_key_ErrorCode_con_valor_ERR_PERSONA_NOTFOUND()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                Id = 1,
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Persona>())).Throws(nfex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Update(persona);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPersonaNotFound));
        }

        [Fact]
        public void al_actualizar_una_persona_con_email_existente_debe_devolver_una_excepción_de_tipo_UniqueKeyViolationException_en_donde_el_data_contenga_la_key_errorCode_con_valor_ERR_PERSONA_EMAILEXIST()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                Id = 1,
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            UniqueKeyViolationException ukvex = new UniqueKeyViolationException("xxx UK_Personas_EMail xxx.");

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Persona>())).Throws(ukvex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Update(persona);

            // Assert.
            action.Should().ThrowExactly<UniqueKeyViolationException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPersonaEmailExist));
        }

        [Fact]
        public void al_actualizar_una_persona_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepcion_debe_contener_la_key_Persona()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                Id = 1,
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Persona>())).Throws(ex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Update(persona);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                ////.And.Data.Contains("Persona"); // No funciona de esta manera (da siempre OK).
                .And.Data["Persona"].Should().NotBeNull();
        }

        [Fact]
        public void al_actualizar_una_persona_con_valor_null_debe_devolver_una_excepción_de_tipo_ArgumentNullException_conteniendo_persona_en_la_propiedad_Message()
        {
            // Arrange.
            ArgumentNullException anex = new ArgumentNullException();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Persona>())).Throws(anex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Update(null);

            // Assert.
            action.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain("persona");
        }

        [Fact]
        public void al_actualizar_una_persona_en_donde_observaciones_contenga_post_EPP_debe_devolver_una_excepción_de_tipo_ValidationException_en_donde_el_data_contenga_una_key_ErrorCode_ERR_PERSONA_PRUEBAVALIDACIONBUSINESS()
        {
            // Arrange.
            Persona persona = new Persona()
            {
                Id = 1,
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                },
                Obs = "Observaciones que contienen la expresión \"post_EPP\" que no es válida."
            };

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Persona>()));

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Update(persona);

            // Assert.
            action.Should().ThrowExactly<ValidationException>()
                .And.Data.Contains(new DictionaryEntry() { Key = "ErrorCode", Value = ExErrorCode.ErrPersonaPruebaValidacionBusiness });
        }

        [Fact]
        public void al_eliminar_una_persona_que_no_se_encuentre_en_el_sistema_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_la_key_errorCode_con_valor_ERR_PERSONA_NOTFOUND()
        {
            // Arrange.
            int id = 1;

            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Delete(It.IsInRange(1, int.MaxValue, Range.Inclusive))).Throws(nfex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Delete(id);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPersonaNotFound));
        }

        [Fact]
        public void al_eliminar_una_persona_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepcion_debe_contener_la_key_id()
        {
            // Arrange.
            int id = 1;

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Delete(It.IsInRange(1, int.MaxValue, Range.Inclusive))).Throws(ex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Delete(id);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                ////.And.Data.Contains("Id"); // No funciona de esta manera (da siempre OK).
                .And.Data["Id"].Should().NotBeNull();
        }

        [Fact]
        public void al_eliminar_una_persona_con_id_existente_no_debe_devolver_ninguna_excepcion()
        {
            // Arrange.
            int id = 1;

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Delete(It.IsInRange(1, int.MaxValue, Range.Inclusive))).Returns(1);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Delete(id);

            // Assert.
            action.Should().NotThrow();
        }

        [Fact]
        public void al_solicitar_la_pagina_2_de_una_lista_de_6_personas_con_paginación_de_2_debe_devolver_una_lista_paginada_con_2_elementos_con_id_3_y_4()
        {
            // Arrange.
            PagedList<Persona> personaPagedList;

            int cantPersonas = 6;
            int pageIndex = 2;
            int pageSize = 2;

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Get(
                                                        It.IsAny<string>(),
                                                        It.IsInRange(1, int.MaxValue, Range.Inclusive),
                                                        It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                                            .Returns(PersonaMockGenerator.GetPageFromList(cantPersonas, pageIndex, pageSize));

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            personaPagedList = personaManager.Get(string.Empty, pageIndex, pageSize);

            // Assert.
            personaPagedList.Should().BeOfType<PagedList<Persona>>();
            personaPagedList.Items[0].Id.Should().Be(3);
            personaPagedList.Items[1].Id.Should().Be(4);
        }

        [Fact]
        public void al_solicitar_una_lista_paginada_de_personas_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepcion_debe_contener_una_key_para_cada_uno_de_los_3_parametros_enviados()
        {
            // Arrange.
            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.Get(
                    It.IsAny<string>(),
                    It.IsInRange(1, int.MaxValue, Range.Inclusive),
                    It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Throws(ex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.Get(string.Empty, 2, 2);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["query"].Should().NotBeNull();
            action.Should().Throw<Exception>()
                .And.Data["pageIndex"].Should().NotBeNull();
            action.Should().Throw<Exception>()
                .And.Data["pageSize"].Should().NotBeNull();
        }

        [Fact]
        public void al_solicitar_una_persona_mediante_su_id_que_no_se_encuentre_en_el_sistema_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_la_key_errorCode_con_valor_ERR_PERSONA_NOTFOUND()
        {
            // Arrange.
            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.GetById(It.IsInRange(1, int.MaxValue, Range.Inclusive))).Throws(nfex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.GetById(1);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPersonaNotFound));
        }

        [Fact]
        public void al_solicitar_una_persona_mediante_su_id_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepción_debe_contener_la_key_id()
        {
            // Arrange.
            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.GetById(It.IsInRange(1, int.MaxValue, Range.Inclusive))).Throws(ex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.GetById(1);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["Id"].Should().NotBeNull();
        }

        [Fact]
        public void al_solicitar_una_persona_mediante_su_id_debe_devolver_una_persona()
        {
            // Arrange.

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.GetById(It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Returns(PersonaMockGenerator.PersonaTestCreator(1));
            ////.Returns((Persona)null);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            var ret = personaManager.GetById(1);

            // Assert.
            ret.Should().BeOfType<Persona>();
        }

        [Fact]
        public void al_solicitar_una_persona_mediante_su_email_que_no_se_encuentre_en_el_sistema_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_la_key_errorCode_con_valor_ERR_PERSONA_NOTFOUND()
        {
            // Arrange.
            string email = "uno@testing.com";

            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.GetByEmail(It.IsAny<string>())).Throws(nfex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.GetByEmail(email);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPersonaNotFound));
        }

        [Fact]
        public void al_solicitar_una_persona_mediante_su_email_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepción_debe_contener_la_key_email()
        {
            // Arrange.
            string email = "uno@testing.com";

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.GetByEmail(It.IsAny<string>())).Throws(ex);

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            Action action = () => personaManager.GetByEmail(email);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["Email"].Should().NotBeNull();
        }

        [Fact]
        public void al_solicitar_una_persona_mediante_su_email_debe_devolver_una_persona()
        {
            // Arrange.
            string email = "uno@testing.com";

            // Mock del repository.
            Mock<IPersonaRepository> personaRepositoryMock = new Mock<IPersonaRepository>();
            personaRepositoryMock.Setup(mgr => mgr.GetByEmail(It.IsAny<string>()))
                .Returns(PersonaMockGenerator.PersonaTestCreator(1));

            PersonaManager personaManager = new PersonaManager(personaRepositoryMock.Object, new PersonaValidator(), new PagedListValidator());

            // Act.
            var ret = personaManager.GetByEmail(email);

            // Assert.
            ret.Should().BeOfType<Persona>();
        }

        #endregion
    }
}