namespace Training.Persona.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Apollo.NetCore.Core.Data;
    using Apollo.NetCore.Core.Exceptions;

    using Dapper;

    using Training.Persona.Data.Interfaces;
    using Training.Persona.Entities;

    /// <summary>
    /// Administra las operaciones de persistencia la entidad Pais.
    /// </summary>
    public class PaisRepository : IPaisRepository
    {
        #region Declarations

        /// <summary>Administra la conexión con la base de datos.</summary>
        private readonly IDatabase database;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PaisRepository"/> class.</summary>
        /// <param name="database">Administra la conexión con la base de datos.</param>
        public PaisRepository(IDatabase database)
        {
            this.database = database;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene una lista paginada de países.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de países.</returns>
        public PagedList<Pais> Get(string query, int pageIndex, int pageSize)
        {
            PagedList<Pais> pagedList =
                new PagedList<Pais>()
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize
                    };

            using (IDbConnection conn = this.database.GetConnection())
            {
                // Obtiene la cantidad total de registros filtrados.
                string sqlCount = @"SELECT COUNT(*) FROM paises";
                pagedList.TotalItemCount = conn.ExecuteScalar<int>(sqlCount);

                int offset = pageSize * (pageIndex - 1);

                // Obtiene los países de la pagina solicitada.
                string sqlIds = @"SELECT 
                                        CodigoIata
                                       ,Nombre 
                                  FROM paises 
                                  ORDER BY CodigoIata 
                                  LIMIT @pageSize OFFSET @offset";

                IList<Pais> pageItems = conn.QueryAndThrow<Pais>(
                    sqlIds,
                    new
                    {
                        offset = offset,
                        pageSize = pageSize
                    }).ToList();

                pagedList.Items = pageItems;
            }

            return pagedList;
        }

        /// <summary>
        /// Obtiene una lista de paises.
        /// </summary>
        /// <returns>Una lista de paises.</returns>
        public IList<Pais> GetAll()
        {
            IList<Pais> paises;

            using (IDbConnection conn = this.database.GetConnection())
            {
                const string Sql = @" SELECT  CodigoIata
                                            , Nombre
                                        FROM paises";

                paises = conn.QueryAndThrow<Pais>(Sql).ToList();
            }

            return paises;
        }

        /// <summary>
        /// Obtiene el País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoIata">El código IATA del país a buscar.</param>
        /// <returns>El País correspondiente al código iata especificado.</returns>
        public Pais GetByIataCode(string codigoIata)
        {
            Pais pais;

            using (IDbConnection conn = this.database.GetConnection())
            {
                // Obtien id de personas, buscando por email.
                const string Sql = @"SELECT 
                                         CodigoIata
                                        ,Nombre 
                                     FROM paises 
                                     WHERE CodigoIata = @CodigoIata";

                pais = conn.QuerySingleAndThrow<Pais>(Sql, new { CodigoIata = codigoIata });
            }

            return pais;
        }

        /// <summary>
        /// Inserta el país especificado.
        /// </summary>
        /// <param name="pais">El País a insertar.</param>
        /// <returns>El País ingresado.</returns>
        public Pais Insert(Pais pais)
        {
            if (pais == null)
            {
                throw new ArgumentNullException(nameof(pais));
            }

            using (IDbConnection conn = this.database.GetConnection())
            {
                const string Sql = @"INSERT INTO paises
                                           ( CodigoIata
                                            ,Nombre)
                                     VALUES
                                           ( @CodigoIata
                                            ,@Nombre);";

                // NOTE: A diferencia de Persona, no se usa QuerySingleAndThrow ya que Pais no tiene
                // un Id entero y autonumérico, es un varchar.
                conn.ExecuteAndThrow(
                     Sql,
                     new
                     {
                         CodigoIata = pais.CodigoIata,
                         Nombre = pais.Nombre
                     });

                return pais;
            }
        }
        
        /// <summary>
        /// Actualiza el país especificado.
        /// </summary>
        /// <param name="pais">El País a actualizar.</param>
        /// <returns>El País actualizado.</returns>
        public Pais Update(Pais pais)
        {
            if (pais == null)
            {
                throw new ArgumentNullException(nameof(pais));
            }

            int ret = 0;

            using (IDbConnection conn = this.database.GetConnection())
            {
                const string Sql = @"UPDATE paises
                                        SET CodigoIata = @CodigoIata, 
                                            Nombre  = @Nombre
                                     WHERE CodigoIata = @CodigoIata";
                ret = conn.ExecuteAndThrow(
                        Sql,
                        new
                        {
                            CodigoIata = pais.CodigoIata,
                            Nombre = pais.Nombre
                        });
            }

            if (ret == 0)
            {
                throw new NotFoundException();
            }

            return pais;
        }

        /// <summary>
        /// Elimina País correspondiente al código iata especificado.
        /// </summary>
        /// <param name="codigoIata">El código IATA del país a eliminar.</param>
        /// <returns>La cantidad de registros afectados.</returns>
        public int Delete(string codigoIata)
        {
            int ret;

            using (IDbConnection conn = this.database.GetConnection())
            {
                const string Sql = @"DELETE FROM paises
                                     WHERE CodigoIata = @CodigoIata";

                ret = conn.ExecuteAndThrow(
                    Sql,
                    new
                    {
                        CodigoIata = codigoIata
                    });
            }

            if (ret == 0)
            {
                throw new NotFoundException();
            }

            return ret;
        }

        #endregion
    }
}