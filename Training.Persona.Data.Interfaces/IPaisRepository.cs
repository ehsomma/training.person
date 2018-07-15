namespace Training.Persona.Data.Interfaces
{
    using System.Collections.Generic;

    using Training.Persona.Entities;

    /// <summary>
    /// Administra las operaciones de persistencia la entidad Pais.
    /// </summary>
    public interface IPaisRepository
    {
        #region Methods

        /// <summary>
        /// Obtiene una lista paginada de países.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de países.</returns>
        PagedList<Pais> Get(string query, int pageIndex, int pageSize);

        /// <summary>
        /// Obtiene una lista de paises.
        /// </summary>
        /// <returns>Una lista de paises.</returns>
        IList<Pais> GetAll();

        /// <summary>
        /// Obtiene el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoIata">El código IATA del país a buscar.</param>
        /// <returns>Un pais.</returns>
        Pais GetByIataCode(string codigoIata);

        /// <summary>
        /// Inserta el país especificado.
        /// </summary>
        /// <param name="pais">El País a insertar.</param>
        /// <returns>El País ingresado.</returns>
        Pais Insert(Pais pais);

        /// <summary>
        /// Actualiza el país especificado.
        /// </summary>
        /// <param name="pais">El País a actualizar.</param>
        /// <returns>El País actualizado.</returns>
        Pais Update(Pais pais);

        /// <summary>
        /// Elimina País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoIata">El código IATA del país a eliminar.</param>
        /// <returns>La cantidad de registros afectados.</returns>
        int Delete(string codigoIata);

        #endregion
    }
}