namespace Apollo.NetCore.Core.Data
{
    using System.Data;

    /// <summary>
    /// Administra la conexión con la base de datos.
    /// </summary>
    public interface IDatabase
    {
        #region Methods

        /// <summary>
        /// Devuelve una conección abierta a la base de datos tomando el connectionString por default
        /// de los settings.
        /// </summary>
        /// <returns>Un DBConnection.</returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Devuelve una conección abierta a la base de datos tomando el connectionString por especificado
        /// de los settings.
        /// </summary>
        /// <param name="connectionName">Nombre del connectionString a buscar en los settings.</param>
        /// <returns>Un DBConnection.</returns>
        IDbConnection GetConnection(string connectionName);

        #endregion
    }
}