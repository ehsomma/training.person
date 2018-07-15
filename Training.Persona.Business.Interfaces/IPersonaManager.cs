namespace Training.Persona.Business.Interfaces
{
    using Training.Persona.Entities;

    /// <summary>
    /// Administra las operaciones de la entidad Persona.
    /// </summary>
    public interface IPersonaManager
    {
        #region Methods

        /// <summary>
        /// Inserta la persona especificada.
        /// </summary>
        /// <param name="persona">La persona a insertar.</param>
        /// <returns>La Persona insertada con su nuevo identificador.</returns>
        Persona Insert(Persona persona);

        /// <summary>
        /// Actualiza la persona especificada.
        /// </summary>
        /// <param name="persona">La persona a actualizar.</param>
        /// <returns>La persona actualizada.</returns>
        Persona Update(Persona persona);

        /// <summary>
        /// Elimina la Persona correspondiente al id especificado.
        /// </summary>
        /// <param name="id">El identificador de la Persona a eliminar.</param>
        void Delete(int id);

        /// <summary>
        /// Obtiene una lista paginada de personas.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de personas.</returns>
        PagedList<Persona> Get(string query, int pageIndex, int pageSize);

        /// <summary>
        /// Obtiene una persona según el id especificado.
        /// </summary>
        /// <param name="id">El identificador de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        Persona GetById(int id);

        /// <summary>
        /// Obtiene una persona según el correo electrónico especificado.
        /// </summary>
        /// <param name="email">El correo electrónico de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        Persona GetByEmail(string email);

        #endregion
    }
}