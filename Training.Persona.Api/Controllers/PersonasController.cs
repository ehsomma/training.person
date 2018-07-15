namespace Training.Persona.Api.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using Training.Persona.Business.Interfaces;
    using Training.Persona.Entities;

    /// <summary>
    /// Operaciones del recurso Personas.
    /// </summary>
    public class PersonasController : Controller
    {
        #region Declarations

        /// <summary>Administra las operaciones de la entidad Persona.</summary>
        private IPersonaManager personaManager;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PersonasController"/> class.</summary>
        /// <param name="personaManager">Administra las operaciones de la entidad Persona.</param>
        public PersonasController(IPersonaManager personaManager)
        {
            this.personaManager = personaManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Operaciones del recurso Persona.
        /// </summary>
        /// <param name="nombre">El nombre para el saludo.</param>
        /// <returns>Un saludo.</returns>
        [HttpGet]
        [Route("personas/saludo")]
        public string GetSaludo(string nombre)
        {
            this.CheckAccessToken();

            ////throw new Exception("Prueba.").AddErrorCode(ExErrorCode.ErrPersonaNotFound);
            return $"Hola {nombre}.";
        }

        /// <summary>
        /// Obtiene una lista paginada de personas.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de personas.</returns>
        [HttpGet]
        [Route("personas")]
        public PagedList<Persona> GetPersonas([FromQuery] string query, [FromQuery] int pageIndex, [FromQuery] int pageSize = 10)
        {
            this.CheckAccessToken();

            // Obtiene la lista paginada de personas.
            PagedList<Persona> personaList = this.personaManager.Get(query, pageIndex, pageSize);
            return personaList;
        }

        /// <summary>
        /// Obtiene una persona por su Id.
        /// </summary>
        /// <param name="id">El identificador de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        /// <response code="404 NotFound">No se encontró la Persona correspondiente al id especificado. ErrorCode: **ERR.PERSONA.NOTFOUND**.</response>
        [HttpGet]
        [Route("personas/{id:int}")]
        public Persona GetById([FromRoute] int id)
        {
            this.CheckAccessToken();

            // Obtiene una persona por su Id.
            Persona persona = this.personaManager.GetById(id);
            return persona;
        }

        /// <summary>
        /// Obtiene una persona por su correo electrónico.
        /// </summary>
        /// <param name="email">El correo electrónico de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        /// <response code="404 NotFound">No se encontró la Persona correspondiente al correo electrónico especificado. ErrorCode: **ERR.PERSONA.NOTFOUND**.</response>
        [HttpGet]
        [Route("personas/{email}")]
        public Persona GetByEmail([FromRoute] string email)
        {
            this.CheckAccessToken();

            // Obtiene una persona por su correo electrónico.
            Persona persona = this.personaManager.GetByEmail(email);
            return persona;
        }

        /// <summary>
        /// Elimina una persona.
        /// </summary>
        /// <param name="id">El identificador de la persona a eliminar.</param>
        /// <response code="404 NotFound">No se encontró la Persona correspondiente al id especificado. ErrorCode: **ERR.PERSONA.NOTFOUND**.</response>
        [HttpDelete]
        [Route("personas/{id}")]
        public void DeleteById([FromRoute] int id)
        {
            this.CheckAccessToken();

            // Elimina la persona.
            this.personaManager.Delete(id);
        }

        /// <summary>
        /// Inserta una persona.
        /// </summary>
        /// <param name="persona">Los datos a ingresar.</param>
        /// <returns>La persona ingresada.</returns>
        /// <response code="403 Forbidden">
        /// * Ya existe una persona con ese email. ErrorCode: **ERR.PERSONA.EMAILEXIST**.
        /// * Al mandar "prueba" en las observaciones simula un error de negocio. ErrorCode: **ERR.PERSONA.PRUEBAVALIDACIONBUSINESS**.
        /// </response>
        [HttpPost]
        [Route("personas")]
        public Persona Post([FromBody] Persona persona)
        {
            this.CheckAccessToken();

            // Inserta la Persona especificada.
            Persona newPersona = this.personaManager.Insert(persona);
            return newPersona;
        }

        /// <summary>
        /// Actualiza la persona especificada.
        /// </summary>
        /// <param name="persona">La persona con los datos a actualizar.</param>
        /// <returns>La persona actualizada.</returns>
        /// <response code="403 Forbidden">
        /// * Ya existe una persona con ese email. ErrorCode: **ERR.PERSONA.EMAILEXIST**.
        /// * Al mandar "prueba" en las observaciones simula un error de negocio. ErrorCode: **ERR.PERSONA.PRUEBAVALIDACIONBUSINESS**.
        /// </response>
        /// <response code="404 NotFound">No se encontró la Persona correspondiente al id especificado. ErrorCode: **ERR.PERSONA.NOTFOUND**.</response>
        [HttpPut]
        [Route("personas")]
        public Persona Put([FromBody] Persona persona)
        {
            this.CheckAccessToken();

            // Actualiza la Persona especificada.
            Persona updatedPersona = this.personaManager.Update(persona);
            return updatedPersona;
        }

        #endregion
    }
}