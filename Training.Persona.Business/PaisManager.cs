namespace Training.Persona.Business
{
    using System;

    using Apollo.NetCore.Core.Exceptions;

    using FluentValidation;

    using Training.Persona.Business.Interfaces;

    using Training.Persona.Data.Interfaces;
    using Training.Persona.Entities;

    /// <summary>
    /// Administra las operaciones de la entidad Pais.
    /// </summary>
    public class PaisManager : IPaisManager
    {
        #region Declarations

        /// <summary>Administra las operaciones de persistencia de la entidad Pais.</summary>
        private readonly IPaisRepository paisRepository;

        /// <summary>Valida el objeto de tipo Pais.</summary>
        private readonly IValidator<Pais> paisValidator;

        /// <summary>Valida el objeto de tipo PagedList.</summary>
        private readonly IValidator<PagedList<object>> pagedListValidator;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PaisManager"/> class.</summary>
        /// <param name="paisRepository">Administra las operaciones de persistencia de la entidad Pais.</param>
        /// <param name="paisValidator">Valida el objeto de tipo Pais.</param>
        /// <param name="pagedListValidator">Valida el objeto de tipo PagedList.</param>
        public PaisManager(
            IPaisRepository paisRepository,
            IValidator<Pais> paisValidator, 
            IValidator<PagedList<object>> pagedListValidator)
        {
            this.paisRepository = paisRepository;
            this.paisValidator = paisValidator;
            this.pagedListValidator = pagedListValidator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserta el país especificado.
        /// </summary>
        /// <param name="pais">El País a ingresar.</param>
        /// <returns>El País ingresado.</returns>
        /// <exception cref="ArgumentNullException">El parámetro Pais es nulo.</exception>
        public Pais Insert(Pais pais)
        {
            if (pais == null)
            {
                throw new ArgumentNullException(nameof(pais));
            }

            // Valida la entidad a insertar.
            this.paisValidator.ValidateAndThrowExeption(pais);

            Pais ret;

            try
            {
                // Inserta el país especificado.
                ret = this.paisRepository.Insert(pais);
            }
            catch (UniqueKeyViolationException ex)
            {
                // Si es una excepción por violación de UK se agrega el error correspondiente.
                if (ex.Message.Contains("PRIMARY"))
                {
                    ex.AddErrorCode(ExErrorCode.ErrPaisIataCodeExist);
                }

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Pais"] = pais.ToJson();

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Actualiza el Pais especificado.
        /// </summary>
        /// <param name="pais">El Pais a actualizar.</param>
        /// <returns>El Pais actualizado.</returns>
        /// <exception cref="NotFoundException">No existe un Pais con el CodigoIata especificado.</exception>
        /// <exception cref="ArgumentNullException">El parámetro Pais es nulo.</exception>
        public Pais Update(Pais pais)
        {
            if (pais == null)
            {
                throw new ArgumentNullException(nameof(pais));
            }

            // Valida la entidad a actualizar.
            this.paisValidator.ValidateAndThrowExeption(pais);

            Pais ret;

            try
            {
                // Actualiza el Pais especificado.
                ret = this.paisRepository.Update(pais);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPaisNotFound);

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["Pais"] = pais.ToJson();

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Elimina el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoiata">El código IATA del país a eliminar.</param>
        /// <exception cref="NotFoundException">No existe un país con el código IATA especificado.</exception>
        public void Delete(string codigoiata)
        {
            try
            {
                // Elimina el País correspondiente al código iata especificado.
                this.paisRepository.Delete(codigoiata);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPaisNotFound);

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["CodigoIata"] = codigoiata;

                throw;
            }
        }

        /// <summary>
        /// Obtiene el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoiata">El código IATA del país a buscar.</param>
        /// <returns>El País solicitado.</returns>
        /// <exception cref="NotFoundException">No existe un país con el código IATA especificado.</exception>
        /// <exception cref="ArgumentNullException">El codigoIata es nulo.</exception>
        /// <exception cref="ArgumentException">El codigoIata no tiene longitud 2.</exception>
        public Pais GetByIataCode(string codigoiata)
        {
            if (codigoiata == null)
            {
                throw new ArgumentNullException(nameof(codigoiata));
            }

            if (codigoiata.Length != 2)
            {
                ArgumentException aex = new ArgumentException(nameof(codigoiata));
                aex.Data["CodigoIata"] = codigoiata;
                throw aex;
            }

            Pais ret;

            try
            {
                // Obtiene el País correspondiente al código iata especificado.
                ret = this.paisRepository.GetByIataCode(codigoiata);
            }
            catch (NotFoundException ex)
            {
                ex.AddErrorCode(ExErrorCode.ErrPaisNotFound);

                throw;
            }
            catch (Exception ex)
            {
                ex.Data["CodigoIata"] = codigoiata;

                throw;
            }

            return ret;
        }

        /// <summary>
        /// Obtiene una lista paginada de países.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de países.</returns>
        public PagedList<Pais> Get(string query, int pageIndex, int pageSize = 10)
        {
            this.pagedListValidator.ValidateAndThrowExeption(new PagedList<object>() { PageSize = pageSize, PageIndex = pageIndex });

            PagedList<Pais> ret;

            try
            {
                // Obtiene una lista paginada de países.
                ret = this.paisRepository.Get(query, pageIndex, pageSize);
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

        #endregion
    }
}