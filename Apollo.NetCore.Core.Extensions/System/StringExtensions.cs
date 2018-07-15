// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// Extensiones para todas las instancias de String.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods

        /// <summary>
        /// Deserealiza la cadena en formato JSON en un objeto del tipo TEntity.
        /// </summary>
        /// <typeparam name="TEntity">Clase del objeto destino.</typeparam>
        /// <param name="value">Cadena en formato JSON.</param>
        /// <returns>Un objeto del tipo TEntity.</returns>
        public static TEntity FromJson<TEntity>(this string value) where TEntity : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(value);
        }

        #endregion
    }
}
