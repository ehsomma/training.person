namespace Training.Persona.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Contenedor de lista paginada de objetos del tipo especificado.
    /// </summary>
    /// <typeparam name="TItems">Tipo de los items a devolver.</typeparam>
    public class PagedList<TItems> where TItems : class
    {
        #region Properties

        /// <summary>Lista de items a devolver (en una página).</summary>
        public IList<TItems> Items { get; set; }

        /// <summary>Total general de items.</summary>
        public int TotalItemCount { get; set; }

        /// <summary>Índice de la página que se está devolviendo.</summary>
        [Range(1, Int32.MaxValue)]
        public int PageIndex { get; set; }

        /// <summary>Cantidad de items por página.</summary>
        [Range(1, 100)]
        public int PageSize { get; set; }

        #endregion
    }
}