namespace Training.Persona.Business
{
    using System;

    using Apollo.NetCore.Core.Exceptions;

    using FluentValidation;

    using Training.Persona.Business.Interfaces;
    using Training.Persona.Data.Interfaces;
    using Training.Persona.Entities;

    /// <summary>
    /// Administra las operaciones de la entidad Persona.
    /// </summary>
    public class PersonaManager : IPersonaManager
    {
        #region Declarations

        /// <summary>Administra las operaciones de la entidad Persona.</summary>
        private readonly IPersonaRepository personaRepository;

        /// <summary>Valida el objeto de tipo Persona.</summary>
        private readonly IValidator<Persona> personaValidator;

        /// <summary>Valida el objeto de tipo PagedList.</summary>
        private readonly IValidator<PagedList<object>> pagedListValidator;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PersonaManager"/> class.</summary>
        /// <param name="personaRepository">Administra las operaciones de persistencia de la entidad Persona.</param>
        /// <param name="personaValidator">Valida el objeto de tipo Persona.</param>
        /// <param name="pagedListValidator">Valida el objeto de tipo PagedList.</param>
        public PersonaManager(
            IPersonaRepository personaRepository, 
            IValidator<Persona> personaValidator, 
            IValidator<PagedList<object>> pagedListValidator)
        {
            this.personaRepository = personaRepository;
            this.personaValidator = personaValidator;
            this.pagedListValidator = pagedListValidator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserta la persona especificada.
        /// </summary>
        /// <param name="persona">La persona a insertar.</param>
        /// <returns>La persona insertada con su nuevo identificador.</returns>
        /// <exception cref="ArgumentNullException">El parámetro Persona es nulo.</exception>
        public Persona Insert(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            // Valida la entidad a insertar.
            this.personaValidator.ValidateAndThrowExeption(persona);

            // Validación de negocio.
            this.ValidateObservaciones(persona.Obs);

            Persona ret;

            try
            {
                // Inserta la persona especificada.
                ret = this.personaRepository.Insert(persona);
            }
            catch (UniqueKeyViolationException ex)
            {
                // Si es una excepción por violación de UK se agrega el error correspondiente.
                if (ex.Message.Contains("UK_Personas_EMail"))
                {
                    ex.AddErrorCode(ExErrorCode.ErrPersonaEmailExist);
                }

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Persona"] = persona.ToJson();

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Actualiza la persona especificada.
        /// </summary>
        /// <param name="persona">La persona a actualizar.</param>
        /// <returns>La persona actualizada.</returns>
        /// <exception cref="NotFoundException">No existe una persona con el Id especificado.</exception>
        /// <exception cref="ArgumentNullException">El parámetro Persona es nulo.</exception>
        public Persona Update(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            // Valida la entidad a actualizar.
            this.personaValidator.ValidateAndThrowExeption(persona);

            // Validación de negocio.
            this.ValidateObservaciones(persona.Obs);

            Persona ret;

            try
            {
                // Actualiza la persona especificada.
                ret = this.personaRepository.Update(persona);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPersonaNotFound);

                throw;
            }
            catch (UniqueKeyViolationException ex)
            {
                // Si es una excepción por violación de UK se agrega el error correspondiente.
                if (ex.Message.Contains("UK_Personas_EMail"))
                {
                    ex.AddErrorCode(ExErrorCode.ErrPersonaEmailExist);
                }

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Persona"] = persona.ToJson();

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Elimina la Persona correspondiente al id especificado.
        /// </summary>
        /// <param name="id">El identificador de la Persona a eliminar.</param>
        /// <exception cref="NotFoundException">No existe una persona con el Id especificado.</exception>
        public void Delete(int id)
        {
            try
            {
                // Elimina la persona correspondiente al id especificado.
                int recordsAffected = this.personaRepository.Delete(id);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPersonaNotFound);

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Id"] = id;

                throw;
            }
        }

        /// <summary>
        /// Obtiene una lista paginada de personas.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de personas.</returns>
        public PagedList<Persona> Get(string query, int pageIndex, int pageSize = 10)
        {
            this.pagedListValidator.ValidateAndThrowExeption(new PagedList<object>() { PageSize = pageSize, PageIndex = pageIndex });

            PagedList<Persona> ret;

            try
            {
                // Obtiene la lista paginada de personas.
                ret = this.personaRepository.Get(query, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                ex.Data["query"] = query;
                ex.Data["pageIndex"] = pageIndex;
                ex.Data["pageSize"] = pageSize;

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Obtiene una persona según el id especificado.
        /// </summary>
        /// <param name="id">El identificador de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        public Persona GetById(int id)
        {
            Persona ret;

            try
            {
                // Obtiene una persona según el id especificado.
                ret = this.personaRepository.GetById(id);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPersonaNotFound);

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Id"] = id;

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Obtiene una persona según el correo electrónico especificado.
        /// </summary>
        /// <param name="email">El correo electrónico de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        public Persona GetByEmail(string email)
        {
            Persona ret;

            try
            {
                // Obtiene una persona según el correo electrónico especificado.
                ret = this.personaRepository.GetByEmail(email);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPersonaNotFound);

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Email"] = email;

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Devuelve una excepción de tipo ValidationException si las observaciones contienen la 
        /// expresión "post_EPP".
        /// </summary>
        /// <param name="observaciones">Observaciones a validar.</param>
        /// <exception cref="Apollo.NetCore.Core.Exceptions.ValidationException">Si se encontró la expresión "post_EPP" en las observaciones.</exception>
        private void ValidateObservaciones(string observaciones)
        {
            if (observaciones != null && observaciones.Contains("post_EPP"))
            {
                throw new Apollo.NetCore.Core.Exceptions.ValidationException()
                    .AddErrorCode(ExErrorCode.ErrPersonaPruebaValidacionBusiness);
            }
        }

        #endregion
    }
}