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

    public class PaisManagerTests
    {
        #region Tests

        [Fact]
        public void al_insertar_un_nuevo_pais_con_valor_null_debe_devolver_una_excepcion_de_tipo_ArgumentNullException_conteniendo_pais_en_la_propiedad_Message()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Pais>())).Returns(PaisMockGenerator.Insert(pais));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Insert(null);

            // Assert.
            action.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(nameof(pais));
        }

        [Fact]
        public void al_insertar_un_nuevo_pais_con_datos_válidos_debe_devolver_un_pais_con_el_mismo_codigoiata_enviado()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Pais>())).Returns(PaisMockGenerator.Insert(pais));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            pais = paisManager.Insert(pais);

            // Assert.
            pais.CodigoIata.Should().BeEquivalentTo("AR");
        }

        [Fact]
        public void al_insertar_un_nuevo_pais_con_codigoIata_existente_debe_devolver_una_excepción_UniqueKeyViolationException_en_donde_el_data_contenga_una_key_ErrorCode_con_valor_ERR_PAIS_IATACODEEXIST()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");

            UniqueKeyViolationException ukvex = new UniqueKeyViolationException("xxx PRIMARY xxx.");

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Pais>())).Throws(ukvex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Insert(pais);

            // Assert.
            pais.CodigoIata.Should().BeEquivalentTo("AR");
            action.Should().ThrowExactly<UniqueKeyViolationException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPaisIataCodeExist));
        }

        [Fact]
        public void al_insertar_un_nuevo_pais_y_se_produzca_una_excepcion_no_controlada_el_data_de_la_excepción_debe_contener_la_key_Pais()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Insert(It.IsAny<Pais>())).Throws(ex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Insert(pais);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["Pais"].Should().NotBeNull();
        }

        [Fact]
        public void al_actualizar_un_pais_con_valor_null_debe_devolver_una_excepcion_de_tipo_ArgumentNullException_conteniendo_pais_en_la_propiedad_Message()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");

            string nuevoNombre = "NuevaArgentina";

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Pais>())).Returns(PaisMockGenerator.UpdateNombre(pais, nuevoNombre));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Update(null);

            // Assert.
            action.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(nameof(pais));
        }

        [Fact]
        public void al_actualizar_el_nombre_de_un_pais_debe_devolver_el_pais_con_el_nombre_actualizado()
        {
            // Arrange.
            string nombreOriginal = "Argentina";
            string nombreActualizado = "Argentina se actualizó!";

            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");
            pais.Nombre = nombreOriginal;

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Pais>())).Returns(PaisMockGenerator.UpdateNombre(pais, nombreActualizado));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            pais = paisManager.Update(pais);

            // Assert.
            pais.Nombre.Should().BeEquivalentTo(nombreActualizado);
        }

        [Fact]
        public void al_actualizar_un_pais_con_codigoIata_inexistente_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_una_key_ErrorCode_con_valor_ERR_PAIS_NOTFOUND()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");
            pais.CodigoIata = "XX";

            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Pais>())).Throws(nfex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Update(pais);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPaisNotFound));
        }

        [Fact]
        public void al_actualizar_un_pais_y_se_produzca_una_excepcion_no_controlada_el_data_de_la_excepción_debe_contener_la_key_Pais()
        {
            // Arrange.
            Pais pais = PaisMockGenerator.GetByCodigoIata("AR");

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Update(It.IsAny<Pais>())).Throws(ex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Update(pais);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["Pais"].Should().NotBeNull();
        }

        [Fact]
        public void al_eliminar_un_pais_que_no_se_encuentre_en_el_sistema_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_la_key_errorCode_con_valor_ERR_PAIS_NOTFOUND()
        {
            // Arrange.
            string codigoIata = "XX";

            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Delete(It.IsAny<string>())).Throws(nfex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Delete(codigoIata);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPaisNotFound));
        }

        [Fact]
        public void al_eliminar_un_pais_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepcion_debe_contener_la_key_CodigoIata()
        {
            // Arrange.
            string codigoIata = "XX";

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Delete(It.IsAny<string>())).Throws(ex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Delete(codigoIata);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["CodigoIata"].Should().NotBeNull();
        }

        [Fact]
        public void al_eliminar_un_pais_con_id_existente_no_debe_devolver_ninguna_excepcion()
        {
            // Arrange.
            string codigoiata = "AR";

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Delete(It.IsAny<string>())).Returns(1);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Delete(codigoiata);
            
            // Assert.
            action.Should().NotThrow();
        }

        [Fact]
        public void al_solicitar_un_pais_mediante_su_codigoiata_con_valor_null_debe_devolver_una_excepción_de_tipo_ArgumentNullException_conteniendo_codigoiata_en_la_propiedad_Message()
        {
            // Arrange.
            string codigoiata = null;

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.GetByIataCode(codigoiata));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.GetByIataCode(null);

            // Assert.
            action.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(nameof(codigoiata));
        }

        [Fact]
        public void al_solicitar_un_pais_mediante_su_codigoiata_con_longitud_distinta_de_2_debe_devolver_una_excepcion_de_tipo_ArgumentException_en_donce_el_data_contenga_la_key_CodigoIata()
        {
            // Arrange.
            string codigoiata = "X";

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.GetByIataCode(It.IsAny<string>()));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.GetByIataCode(codigoiata);

            // Assert.
            action.Should().ThrowExactly<ArgumentException>()
                .And.Data["CodigoIata"].Should().NotBeNull();
        }

        [Fact]
        public void al_solicitar_un_pais_mediante_su_codigoiata_que_no_se_encuentre_en_el_sistema_debe_devolver_una_excepción_de_tipo_NotFoundException_en_donde_el_data_contenga_la_key_errorCode_con_valor_ERR_PAIS_NOTFOUND()
        {
            // Arrange.
            string codigoIata = "XX";

            NotFoundException nfex = new NotFoundException();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.GetByIataCode(It.IsAny<string>())).Throws(nfex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.GetByIataCode(codigoIata);

            // Assert.
            action.Should().ThrowExactly<NotFoundException>()
                .And.Data.Should().Contain(new DictionaryEntry("ErrorCode", ExErrorCode.ErrPaisNotFound));
        }

        [Fact]
        public void al_solicitar_un_pais_mediante_su_codigoiata_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepción_debe_contener_la_key_CodigoIata()
        {
            // Arrange.
            string codigoIata = "XX";

            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.GetByIataCode(It.IsAny<string>())).Throws(ex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.GetByIataCode(codigoIata);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["CodigoIata"].Should().NotBeNull();
        }

        [Fact]
        public void al_solicitar_un_pais_mediante_su_codigoiata_debe_devolver_un_pais()
        {
            // Arrange.
            string codigoiata = "AR";

            Pais pais = PaisMockGenerator.GetByCodigoIata(codigoiata);

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.GetByIataCode(It.IsAny<string>())).Returns(pais);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            var ret = paisManager.GetByIataCode(codigoiata);

            // Assert.
            ret.Should().BeOfType<Pais>();
        }

        [Fact]
        public void al_solicitar_una_lista_paginada_de_pais_y_se_produzca_una_excepción_no_controlada_el_data_de_la_excepción_debe_contener_keys_con_los_valores_de_los_3_parametros()
        {
            // Arrange.
            Exception ex = new Exception();

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Get(
                    It.IsAny<string>(),
                    It.IsInRange(1, int.MaxValue, Range.Inclusive),
                    It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Throws(ex);

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            Action action = () => paisManager.Get(string.Empty, 2, 2);

            // Assert.
            action.Should().ThrowExactly<Exception>()
                .And.Data["query"].Should().NotBeNull();
            action.Should().ThrowExactly<Exception>()
                .And.Data["pageIndex"].Should().NotBeNull();
            action.Should().ThrowExactly<Exception>()
                .And.Data["pageSize"].Should().NotBeNull();
        }

        [Fact]
        public void al_solicitar_la_pagina_2_de_una_lista_de_10_paises_con_paginacion_2_debe_devolver_una_lista_paginada_con_2_elementos_con_codigoIata_AF_y_AG()
        {
            // Arrange.
            PagedList<Pais> paisPagedList;

            int cantPaises = 10;
            int pageIndex = 2;
            int pageSize = 2;

            // Mock del repository.
            Mock<IPaisRepository> paisRepositoryMock = new Mock<IPaisRepository>();
            paisRepositoryMock.Setup(mgr => mgr.Get(
                    It.IsAny<string>(),
                    It.IsInRange(1, int.MaxValue, Range.Inclusive),
                    It.IsInRange(1, cantPaises, Range.Inclusive)))
                .Returns(PaisMockGenerator.GetPageFromList(cantPaises, pageIndex, pageSize));

            PaisManager paisManager = new PaisManager(paisRepositoryMock.Object, new PaisValidator(), new PagedListValidator());

            // Act.
            paisPagedList = paisManager.Get(string.Empty, pageIndex, pageSize);

            // Assert.
            paisPagedList.Should().BeOfType<PagedList<Pais>>();
            paisPagedList.Items[0].CodigoIata.Should().Be("AF");
            paisPagedList.Items[1].CodigoIata.Should().Be("AG");
        }

        #endregion
    }
}