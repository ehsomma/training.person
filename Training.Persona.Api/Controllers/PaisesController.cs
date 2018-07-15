namespace Training.Persona.Api.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using Training.Persona.Business.Interfaces;
    using Training.Persona.Entities;

    /// <summary>
    /// Operaciones del recurso Países.
    /// </summary>
    public class PaisesController : Controller
    {
        #region Declarations

        /// <summary>Administra las operaciones de la entidad Pais.</summary>
        private IPaisManager paisManager;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PaisesController"/> class.</summary>
        /// <param name="paisManager">Administra las operaciones de la entidad Pais.</param>
        public PaisesController(IPaisManager paisManager)
        {
            this.paisManager = paisManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene una lista paginada de países.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize"> Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de países.</returns>
        [HttpGet]
        [Route("paises")]
        public PagedList<Pais> GetPaises([FromQuery] string query, [FromQuery] int pageIndex, [FromQuery] int pageSize = 10)
        {
            this.CheckAccessToken();

            // Obtiene una lista paginada de países.
            PagedList<Pais> paises = this.paisManager.Get(query, pageIndex, pageSize);
            return paises;
        }

        /// <summary>
        /// Obtiene el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoiata">El código IATA del país a buscar.</param>
        /// <returns>El País solicitado.</returns>
        [HttpGet]
        [Route("paises/{codigoiata}")]
        public Pais Get([FromRoute] string codigoiata)
        {
            this.CheckAccessToken();

            // Obtiene el País correspondiente al código iata especificado.
            Pais pais = this.paisManager.GetByIataCode(codigoiata);
            return pais;
        }

        /// <summary>
        /// Inserta un país.
        /// </summary>
        /// <param name="pais">Los datos a ingresar.</param>
        /// <returns>El país ingresado.</returns>
        /// <response code="403 Forbidden">Ya existe un país con ese código IATA. ErrorCode: **ERR.PAIS.IATACODEEXIST**.</response>
        [HttpPost]
        [Route("paises")]
        public Pais Post([FromBody] Pais pais)
        {
            this.CheckAccessToken();

            // Inserta un país.
            Pais newPais = this.paisManager.Insert(pais);
            return newPais;
        }

        /// <summary>
        /// Actualiza un país.
        /// </summary>
        /// <param name="pais">Los datos a editar.</param>
        /// <returns>El país actualizado.</returns>
        /// <response code="403 Forbidden">No existe un país con el código IATA especificado. ErrorCode: **ERR.PAIS.NOTFOUND**.</response>
        [HttpPut]
        [Route("paises")]
        public Pais Put([FromBody] Pais pais)
        {
            this.CheckAccessToken();

            // Actualiza el país especificado.
            Pais updatedPais = this.paisManager.Update(pais);
            return updatedPais;
        }

        /// <summary>
        /// Elimina un país.
        /// </summary>
        /// <param name="codigoiata">El código IATA del país a eliminar.</param>
        /// <response code="404 NotFound">No se encontró el País correspondiente al código especificado.</response>
        [HttpDelete]
        [Route("paises/{codigoiata}")]
        public void Delete([FromRoute] string codigoiata)
        {
            this.CheckAccessToken();

            // Elimina el país.
            this.paisManager.Delete(codigoiata);
        }

        #endregion
    }
}
