namespace Training.Persona.Business.Interfaces
{
    using Training.Persona.Entities;

    /// <summary>
    /// Administra las operaciones de la entidad Pais.
    /// </summary>
    public interface IPaisManager
    {
        #region Methods

        /// <summary>
        /// Actualiza el país especificado.
        /// </summary>
        /// <param name="pais">El País a ingresar.</param>
        /// <returns>El País ingresado.</returns>
        Pais Insert(Pais pais);

        /// <summary>
        /// Actualiza el Pais especificado.
        /// </summary>
        /// <param name="pais">El Pais a actualizar.</param>
        /// <returns>El Pais actualizado.</returns>
        Pais Update(Pais pais);

        /// <summary>
        /// Elimina el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoiata">El código IATA del país a eliminar.</param>
        void Delete(string codigoiata);

        /// <summary>
        /// Obtiene el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoiata">El código IATA del país a buscar.</param>
        /// <returns>El País solicitado.</returns>
        Pais GetByIataCode(string codigoiata);

        /// <summary>
        /// Obtiene una lista paginada de países.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de países.</returns>
        PagedList<Pais> Get(string query, int pageIndex, int pageSize);

        #endregion
    }
}