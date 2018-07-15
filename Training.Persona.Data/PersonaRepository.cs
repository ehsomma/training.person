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
    /// Administra las operaciones de persistencia la entidad Persona.
    /// </summary>
    public class PersonaRepository : IPersonaRepository
    {
        #region Declarations

        /// <summary>Administra la conexión con la base de datos.</summary>
        private readonly IDatabase database;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="PersonaRepository"/> class.</summary>
        /// <param name="database">Administra la conexión con la base de datos.</param>
        public PersonaRepository(IDatabase database)
        {
            this.database = database;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserta la Persona especificada.
        /// </summary>
        /// <param name="persona">La Persona a insertar.</param>
        /// <exception cref="ArgumentNullException">El valor de persona es null.</exception>
        /// <returns>La Persona insertada con su nuevo identificador.</returns>
        public Persona Insert(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            using (IDbConnection conn = this.database.GetConnection())
            {
                const string Sql = @"INSERT INTO personas
                                           ( NombreCompleto
                                            ,EMail
                                            ,TotalAhorro
                                            ,PorcAhorro
                                            ,Obs
                                            ,Direccion
                                            ,Lat
                                            ,Lon
                                            ,EstadoId
                                            ,FechaNacimiento
                                            ,RecibirNotificaciones
                                            ,DateFormat
                                            ,TimeFormat
                                            ,LanguageCode
                                            ,ZonaHoraria
                                            ,CountryCode
                                            ,Password)
                                     VALUES
                                           ( @NombreCompleto
                                            ,@EMail
                                            ,@TotalAhorro
                                            ,@PorcAhorro
                                            ,@Obs
                                            ,@Direccion
                                            ,@Lat
                                            ,@Lon
                                            ,@EstadoId
                                            ,@FechaNacimiento
                                            ,@RecibirNotificaciones
                                            ,@DateFormat
                                            ,@TimeFormat
                                            ,@LanguageCode
                                            ,@ZonaHoraria
                                            ,@CountryCode
                                            ,@Password);
                                    SELECT LAST_INSERT_ID();";

                int idPersona = conn.QuerySingleAndThrow<int>(
                    Sql,
                    new
                    {
                        NombreCompleto = persona.NombreCompleto,
                        EMail = persona.EMail,
                        TotalAhorro = persona.TotalAhorro,
                        PorcAhorro = persona.PorcAhorro,
                        Obs = persona.Obs,
                        Direccion = persona.Direccion,
                        Lat = persona.Lat,
                        Lon = persona.Lon,
                        EstadoId = persona.Estado,
                        FechaNacimiento = persona.FechaNacimiento,
                        RecibirNotificaciones = persona.RecibirNotificaciones,
                        DateFormat = persona.RegionalData.DateFormat,
                        TimeFormat = persona.RegionalData.TimeFormat,
                        LanguageCode = persona.RegionalData.LanguageCode,
                        ZonaHoraria = persona.RegionalData.TimeZone.ToJson(),
                        CountryCode = persona.RegionalData.CountryCode,
                        Password = persona.Password
                    });

                /*
                NOTA: Si las propiedades de la entidad coinciden exactamente con las columnas de la 
                tabla, se puede pasar el objeto de la entidad directamente sin pasar un objeto anónimo.
                En este caso, EstadoId y 
                 - El campo EstadoId no coincide con la propiedad Estado.
                 - El campo ZonaHoraria no tiene propiedad directa en Persona.

                int idPersona = conn.QuerySingleAndThrow<int>(
                    Sql,
                    persona);
                */

                // Asigna el id del viaje recién ingresado.
                persona.Id = idPersona;

                // Inserta los bienes de la Persona especificada.
                this.InsertBienes(persona, conn);

                return persona;
            }
        }

        /// <summary>
        /// Actualiza la Persona especificada.
        /// </summary>
        /// <param name="persona"> La Persona a actualizar.</param>
        /// <exception cref="ArgumentNullException">El valor de persona es null.</exception>
        /// <returns>La persona actualizada.</returns>
        public Persona Update(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            int ret = 0;

            using (IDbConnection conn = this.database.GetConnection())
            {
                // Actualiza persona.
                const string Sql = @"UPDATE personas
                                        SET NombreCompleto = @NombreCompleto, 
                                            EMail  = @EMail,
                                            TotalAhorro  = @TotalAhorro,
                                            PorcAhorro  = @PorcAhorro,
                                            Obs  = @Obs,
                                            Direccion  = @Direccion,
                                            Lat  = @Lat,
                                            Lon  = @Lon,
                                            EstadoId  = @EstadoId,
                                            FechaNacimiento  = @FechaNacimiento,
                                            RecibirNotificaciones  = @RecibirNotificaciones,
                                            DateFormat  = @DateFormat,
                                            TimeFormat  = @TimeFormat,
                                            LanguageCode  = @LanguageCode,
                                            ZonaHoraria  = @ZonaHoraria,
                                            CountryCode  = @CountryCode,
                                            Password  = @Password
                                     WHERE Id = @Id";

                ret = conn.ExecuteAndThrow(
                        Sql,
                        new
                        {
                            Id = persona.Id,
                            NombreCompleto = persona.NombreCompleto,
                            EMail = persona.EMail,
                            TotalAhorro = persona.TotalAhorro,
                            PorcAhorro = persona.PorcAhorro,
                            Obs = persona.Obs,
                            Direccion = persona.Direccion,
                            Lat = persona.Lat,
                            Lon = persona.Lon,
                            EstadoId = persona.Estado,
                            FechaNacimiento = persona.FechaNacimiento,
                            RecibirNotificaciones = persona.RecibirNotificaciones,
                            DateFormat = persona.RegionalData.DateFormat,
                            TimeFormat = persona.RegionalData.TimeFormat,
                            LanguageCode = persona.RegionalData.LanguageCode,
                            ZonaHoraria = persona.RegionalData.TimeZone.ToJson(),
                            CountryCode = persona.RegionalData.CountryCode,
                            Password = persona.Password
                        });

                if (ret == 0)
                {
                    throw new NotFoundException();
                }
                else
                {
                    // Inserta los bienes de la Persona especificada.
                    this.InsertBienes(persona, conn);
                }
            }

            return persona;
        }

        /// <summary>
        /// Elimina la Persona correspondiente al id especificado.
        /// </summary>
        /// <param name="id">El identificador de la Persona a eliminar.</param>
        /// <returns>La cantidad de registros afectados.</returns>
        public int Delete(int id)
        {
            int ret = 0;
            
            using (IDbConnection conn = this.database.GetConnection())
            {
                const string Sql = @"DELETE FROM bienes WHERE PersonaId = @Id; 
                                     DELETE FROM personas WHERE Id = @Id";
                ret = conn.ExecuteAndThrow(
                    Sql,
                    new
                    {
                        Id = id
                    });
            }

            if (ret == 0)
            {
                throw new NotFoundException();
            }

            return ret;
        }

        /// <summary>
        /// Obtiene una lista paginada de personas.
        /// </summary>
        /// <param name="query">Texto de búsqueda.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página (De 1 a 100. default 10).</param>
        /// <returns>Una lista paginada de personas.</returns>
        public PagedList<Persona> Get(string query, int pageIndex, int pageSize)
        {
            PagedList<Persona> pagedlistPersonas =
                new PagedList<Persona>() { PageIndex = pageIndex, PageSize = pageSize };

            using (IDbConnection conn = this.database.GetConnection())
            {
                // Obtiene la cantidad total de registros filtrados.
                string sqlCount = @"SELECT COUNT(*) FROM personas";
                pagedlistPersonas.TotalItemCount = conn.ExecuteScalar<int>(sqlCount);

                int offset = pageSize * (pageIndex - 1);
                
                // Obtiene ids de personas de la pagina.
                string sqlIds =
                    string.Format(
                        @"SELECT Id 
                          FROM personas 
                          ORDER BY Id 
                          LIMIT @pageSize OFFSET @offset");

                IList<int> idsRegistros = conn.QueryAndThrow<int>(
                    sqlIds,
                    new
                    {
                        offset = offset,
                        pageSize = pageSize
                    }).ToList();

                // Obtiene las personas correspondientes a la lista de ids especificada.
                pagedlistPersonas.Items = this.GetByIds(idsRegistros);
            }

            return pagedlistPersonas;
        }

        /// <summary>
        /// Obtiene una persona según el id especificado.
        /// </summary>
        /// <param name="id">El identificador de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        public Persona GetById(int id)
        {
            IList<int> idsPersonas = new List<int>();

            idsPersonas.Add(id);

            Persona persona = this.GetByIds(idsPersonas).FirstOrDefault();

            return persona;
        }

        /// <summary>Obtiene una persona según el correo electrónico especificado.</summary>
        /// <param name="email">El correo electrónico de la persona a buscar.</param>
        /// <returns>Una persona.</returns>
        public Persona GetByEmail(string email)
        {
            Persona persona;

            using (IDbConnection conn = this.database.GetConnection())
            {
                // TODO: NotFound?

                // Obtien id de personas, buscando por email.
                string sql = "SELECT ID FROM personas WHERE EMail = @EMail";

                int idPersona = conn.ExecuteScalarAndThrow<int>(sql, new { EMail = email });

                persona = this.GetById(idPersona);
            }

            return persona;
        }

        /// <summary>
        /// Inserta los bienes de la Persona especificada.
        /// </summary>
        /// <param name="persona">La Persona que contiene la lista de bienes.</param>
        /// <param name="conn">Conexión con la base de datos.</param>
        private void InsertBienes(Persona persona, IDbConnection conn)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            if (conn == null)
            {
                throw new ArgumentNullException(nameof(conn));
            }

            // Inserta los bienes.
            if (persona.Bienes != null)
            {
                // Elimina bienes para luego insertarlos.
                // NOTA: Esta técnica evita determinar que hacer con cada bien por si requiere insert, 
                // updete, delete o nada. No siempre se debe usar esta técnica.
                const string Sql2 = @"DELETE FROM bienes WHERE PersonaId = @PersonaId";
                conn.ExecuteAndThrow(
                    Sql2,
                    new
                        {
                            PersonaId = persona.Id
                        });

                const string Sql = @"INSERT INTO bienes
                                           ( Tipo
                                            ,Descripcion
                                            ,Valor
                                            ,PersonaId)
                                        VALUES
                                           ( @Tipo
                                            ,@Descripcion
                                            ,@Valor
                                            ,@PersonaId);
                                        SELECT LAST_INSERT_ID();";

                foreach (Bien bien in persona.Bienes)
                {
                    int idBien = conn.QuerySingleAndThrow<int>(
                        Sql,
                        new
                            {
                                Tipo = bien.Tipo,
                                Descripcion = bien.Descripcion,
                                Valor = bien.Valor,
                                PersonaId = persona.Id
                            });

                    // Asigna el id del bien recien ingresado.
                    bien.Id = idBien;
                }
            }
        }

        /// <summary>
        /// Obtiene las Personas correspondientes a la lista de ids especificada.
        /// </summary>
        /// <param name="ids">Lista de ids.</param>
        /// <returns>Una lista de Personas.</returns>
        private IList<Persona> GetByIds(IList<int> ids)
        {
            IList<Persona> ret;

            using (IDbConnection conn = this.database.GetConnection())
            {
                if (ids.Any())
                {
                    string sqlPage = $@"
                        SELECT 
                            PER.Id,                           
                            PER.NombreCompleto,                           
                            PER.EMail,                           
                            PER.FechaCreo,                           
                            PER.FechaActualizo,                           
                            PER.TotalAhorro,                           
                            PER.PorcAhorro,                           
                            PER.Obs,                           
                            PER.Direccion,                           
                            PER.Lat,                           
                            PER.Lon,                           
                            PER.EstadoId AS Estado,                           
                            PER.FechaNacimiento,                           
                            PER.RecibirNotificaciones,                           
                            PER.Password,                           
                            '' AS FixSplit1,
                            PER.DateFormat,                           
                            PER.TimeFormat,                           
                            PER.LanguageCode,                           
                            PER.ZonaHoraria,                           
                            PER.CountryCode,                           
                            BIE.Id,                           
                            BIE.Tipo,  
                            BIE.Descripcion,  
                            BIE.Valor,  
                            BIE.PersonaId
                        FROM personas PER                            
                            LEFT JOIN bienes BIE ON PER.Id = BIE.PersonaId                            
                        WHERE 
                            PER.ID IN @ids 
                        ORDER BY PER.Id, BIE.Id";

                    IDictionary<int, Persona> bufferPersonas = new Dictionary<int, Persona>();

                    // Ejecuta la consulta.
                    Func<Persona, RegionalData, Bien, Persona> func = (persona, regionalData, bien) =>
                    {
                        // Agrupa por persona.
                        if (!bufferPersonas.ContainsKey(persona.Id))
                        {
                            bufferPersonas[persona.Id] = persona;
                        }

                        // Asigna RegionalData.
                        if (regionalData != null)
                        {
                            bufferPersonas[persona.Id].RegionalData = regionalData;

                            if (regionalData.ZonaHoraria != null)
                            {
                                // Deserializa el campo ZonaHoraria al objeto TimeZone.
                                bufferPersonas[persona.Id].RegionalData.TimeZone = regionalData.ZonaHoraria.FromJson<Training.Persona.Entities.TimeZone>();
                            }
                        }

                        // Carga la lista de Bienes.
                        if (bien != null)
                        {
                            bufferPersonas[persona.Id].Bienes.Add(bien);
                        }

                        return bufferPersonas[persona.Id];
                    };
                    conn.QueryAndThrow<Persona, RegionalData, Bien, Persona>(
                        sqlPage,
                        func,
                        new { ids },
                        splitOn: "FixSplit1,Id");

                    ret = bufferPersonas.Values.ToList();
                }
                else
                {
                    ret = new List<Persona>();
                }
            }

            return ret;
        }

        #endregion
    }
}