// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using System;
    using System.Collections.Generic;

    using Training.Persona.Entities;

    public static class PersonaMockGenerator
    {
        #region Methods

        /// <summary>
        /// Devuelve una Persona válida con el identificador especificado.
        /// </summary>
        /// <param name="id">El id a asignar a la persona.</param>
        /// <returns>Una persona con el id especificado.</returns>
        public static Persona PersonaTestCreator(int id)
        {
            Persona persona = new Persona()
            {
                Id = id,
                NombreCompleto = "Testing, Uno",
                EMail = "uno@testing.com",
                TotalAhorro = 100.0M,
                PorcAhorro = 1.0M,
                Direccion = "Server de testing 1",
                Lat = -34.6377278M,
                Lon = -58.4098517M,
                Estado = AccountStatus.Active,
                FechaNacimiento = DateTime.Now,
                RecibirNotificaciones = true,
                Sexo = "M",
                Password = "123456",
                RegionalData = new RegionalData()
                {
                    CountryCode = "AR"
                }
            };

            return persona;
        }

        /// <summary>
        /// Asigna el id especificado a la persona especificada simulando un insert.
        /// </summary>
        /// <param name="persona">La persona a editar.</param>
        /// <param name="newId">El id a asignar.</param>
        /// <returns>La persona con el nuevo id.</returns>
        public static Persona Insert(Persona persona, int newId)
        {
            persona.Id = newId;

            return persona;
        }

        /// <summary>
        /// Asigna la observacion especificada a la persona especificada simulando un update.
        /// </summary>
        /// <param name="persona">La persona a editar.</param>
        /// <param name="observacion">La obseravacion a asignar.</param>
        /// <returns>La persona con la nueva observación.</returns>
        public static Persona UpdateObservacion(Persona persona, string observacion)
        {
            persona.Obs = observacion;

            return persona;
        }

        /// <summary>
        /// Devuelve una lista de persona según la cantidad especificada de elementos.
        /// </summary>
        /// <param name="count">Cantidad de elementos a agregar a la lista.</param>
        /// <returns>Una lista de personas.</returns>
        public static List<Persona> GetList(int count)
        {
            List<Persona> listPersona = new List<Persona>(count);

            for (int i = 1; i <= count; i++)
            {
                listPersona.Add(PersonaMockGenerator.PersonaTestCreator(i));
            }

            return listPersona;
        }

        /// <summary>
        /// Devuelve una página especifica de una lista paginada de personas con la cantidad de 
        /// elementos especificados.
        /// </summary>
        /// <param name="count">Cantidad de elementos a agregar a la lista.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página.</param>
        /// <returns>Una lista paginada de personas.</returns>
        public static PagedList<Persona> GetPageFromList(int count, int pageIndex, int pageSize)
        {
            PagedList<Persona> pagedListPersona = new PagedList<Persona>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemCount = pageSize
            };

            // Se filtran los elementos de la página solicitada.
            List<Persona> items = GetList(count).GetRange((pageIndex - 1) * pageSize, pageSize);
            pagedListPersona.Items = items;

            return pagedListPersona;
        }

        #endregion
    }
}