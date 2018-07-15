// ReSharper disable once CheckNamespace
namespace System
{
    using System.Data;

    using Apollo.NetCore.Core.Exceptions;

    using MySql.Data.MySqlClient;

    /// <summary>
    /// Manager procesa las excepciones.
    /// </summary>
    public class ExceptionManager
    {
        #region Decalrations

        /// <summary>Código de error de MySql para clave duplicada.</summary>
        private const int MySqlErrCodeUkDuplicateEntry = 1062;

        /// <summary>Código de error de MySql para violación de integridad referencial.</summary>
        private const int MySqlErrCodeFkConstrainFails = 1451;

        #endregion

        #region Methods

        /// <summary>
        /// Intenta ejecutar la función especificada realizando un tratamiento de excepciones de LinQ
        /// convirtiéndolas en excepciones core.
        /// </summary>
        /// <typeparam name="TReturn">El tipo de respuesta de la función.</typeparam>
        /// <param name="func">Función a ejecutar.</param>
        /// <returns>La respuesta de la ejecución de la función.</returns>
        public static TReturn TryLinq<TReturn>(Func<TReturn> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            TReturn ret;

            try
            {
                ret = func();
            }
            catch (InvalidOperationException ex)
            {
                /*
                Posibles exepciones:
                InvalidOperationException
                    .Single sin resultado: "Sequence contains no elements".
                    .Single con más de un resultado: "Sequence contains more than one element".
                    .First sin resultado: "Sequence contains no elements".
                 */

                // Si es un InvalidOperationException de LinQ.
                if (ex.Source == "System.Linq")
                {
                    switch (ex.Message)
                    {
                        case "Sequence contains no elements":
                            throw new NotFoundException(ex.Message, ex);

                        case "Sequence contains more than one element":
                            throw;

                        default:
                            throw;
                    }
                }
                else
                {
                    throw;
                }
            }

            return ret;
        }

        /// <summary>
        /// Procesa la excepción de base de datos especificada y devuelve una excepción core agregando
        /// datos de la consulta y la excepción original como inner.
        /// </summary>
        /// <param name="conn">La nonexión usada.</param>
        /// <param name="ex">Excepción original.</param>
        /// <exception cref="ArgumentNullException">El argumento ex es null.</exception>
        /// <returns>Una excepción core.</returns>
        public static Exception Process(IDbConnection conn, Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            /*
             Para estos casos no hace falta tratamiento:
             ==========================================
             * Servicio de base de datos apagado (InternalServerError, UnableToConnectToHost).
             * Nombre de base de datos incorrecta (InternalServerError, AuthenticationFailed).
             * Credenciales de base de datos incorrecta (InternalServerError, AuthenticationFailed).

             */

            string constraintName = null;
            Apollo.NetCore.Core.Exceptions.DataException dex;
            MySqlException mysqlEx = ex as MySqlException;
            if (mysqlEx != null)
            {
                // Exception de MySql.
                switch (mysqlEx.Number)
                {
                    case MySqlErrCodeUkDuplicateEntry:
                        // Duplicate entry.

                        /*
                        Ejemplos de mensajes:
                        Message = "Duplicate entry 'Traslada' for key 'UK_Name'"
                        Message = "Duplicate entry '1' for key 'PRIMARY'"
                        */
                        constraintName = GetConstraintName(mysqlEx.Message, "UK");
                        dex = new UniqueKeyViolationException(mysqlEx.Message, mysqlEx, constraintName);
                        break;

                    case MySqlErrCodeFkConstrainFails:
                        // Foreign key constraint fails.

                        /*
                        Ejemplos de mensajes:
                        Message = "Cannot delete or update a parent row: a foreign key constraint fails (`zwitcher`.`appendpointconfig`, CONSTRAINT `FK_AppConfig_Id` FOREIGN KEY (`AppConfigId`) REFERENCES `appconfig` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION)"
                        */

                        constraintName = GetConstraintName(mysqlEx.Message, "FK");
                        dex = new ReferentialIntegrityViolationException(mysqlEx.Message, mysqlEx, constraintName);
                        break;

                    default:
                        // Otra excepción de MySql.
                        dex = new Apollo.NetCore.Core.Exceptions.DataException(mysqlEx.Message, mysqlEx);
                        break;
                }
            }
            else
            {
                // Otra exepción.
                dex = new Apollo.NetCore.Core.Exceptions.DataException(ex.Message, ex);
            }

            // Agrega al data de la excepción datos de la conexión.
            if (conn != null)
            {
                // TODO:
            }

            if (!string.IsNullOrWhiteSpace(constraintName))
            {
                dex.Data.Add(ExDataKey.ConstraintName, constraintName);
            }

            throw dex;
        }

        /// <summary>
        /// Obtiene el constraint name del mensaje especificado de acuerdo al acrónimo especificado.
        /// </summary>
        /// <param name="message">Mensaje en el cual se va a buscar el constraint.</param>
        /// <param name="constraintAcronym">Acrónomo del constraint a buscar (ej: "PK"/"PRIMARY", "FK", "UK", etc.).</param>
        /// <returns>El constraint name or null.</returns>
        private static string GetConstraintName(string message, string constraintAcronym)
        {
            string ret = null;

            try
            {
                // Reemplaza "'", """ y "`" por "*" ya que en algunos mensajes viene comilla simple
                // y en otros doble. De esta forma busca el fin del nombre del constarint por "*".
                const string Start = "*";
                message = message.Replace("'", Start).Replace(Convert.ToChar(34).ToString(), Start).Replace("`", Start);
                int constStart = message.IndexOf(constraintAcronym, StringComparison.Ordinal);
                int constEnd = message.IndexOf(Start, constStart, StringComparison.Ordinal);
                if (constStart > 0 & constEnd > 0)
                {
                    ret = message.Substring(constStart, constEnd - constStart);
                }
            }
            catch
            {
                ret = "Error al obtener el constarint name.";
            }

            return ret;
        }

        #endregion
    }
}