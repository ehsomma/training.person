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
        /// Actualiza el pa�s especificado.
        /// </summary>
        /// <param name="pais">El Pa�s a ingresar.</param>
        /// <returns>El Pa�s ingresado.</returns>
        Pais Insert(Pais pais);

        /// <summary>
        /// Actualiza el Pais especificado.
        /// </summary>
        /// <param name="pais">El Pais a actualizar.</param>
        /// <returns>El Pais actualizado.</returns>
        Pais Update(Pais pais);

        /// <summary>
        /// Elimina el Pa�s correspondiente al c�digo iata especificado.
        /// </summary>
        /// <param name="codigoiata">El c�digo IATA del pa�s a eliminar.</param>
        void Delete(string codigoiata);

        /// <summary>
        /// Obtiene el Pa�s correspondiente al c�digo iata especificado.
        /// </summary>
        /// <param name="codigoiata">El c�digo IATA del pa�s a buscar.</param>
        /// <returns>El Pa�s solicitado.</returns>
        Pais GetByIataCode(string codigoiata);

        /// <summary>
        /// Obtiene una lista paginada de pa�ses.
        /// </summary>
        /// <param name="query">Texto de b�squeda.</param>
        /// <param name="pageIndex">N�mero de p�gina a solicitar.</param>
        /// <param name="pageSize">Cantidad de �tems por p�gina (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de pa�ses.</returns>
        PagedList<Pais> Get(string query, int pageIndex, int pageSize);

        #endregion
    }
}