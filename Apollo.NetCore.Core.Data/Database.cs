namespace Apollo.NetCore.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using Apollo.NetCore.Core.Settings;

    using MySql.Data.MySqlClient;

    /// <summary>
    /// Administra la conexión con la base de datos.
    /// </summary>
    public class Database : IDatabase
    {
        #region Decalrations

        /// <summary>Settings de connectionString.</summary>
        private ConnectionStrings connectionString;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Database"/> class.</summary>
        /// <param name="connectionString">Settings de connectionString.</param>
        public Database(ConnectionStrings connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Devuelve una conección abierta a la base de datos tomando el connectionString por default
        /// de los settings.
        /// </summary>
        /// <returns>Un DBConnection.</returns>
        public IDbConnection GetConnection()
        {
            return this.GetConnection("DefaultConnection");
        }

        /// <summary>
        /// Devuelve una conección abierta a la base de datos tomando el connectionString por especificado
        /// de los settings.
        /// </summary>
        /// <param name="connectionName">Nombre del connectionString a buscar en los settings.</param>
        /// <returns>Un DBConnection.</returns>
        public IDbConnection GetConnection(string connectionName)
        {
            string connStr = this.connectionString[connectionName];
            IDbConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        #endregion
    }
}
