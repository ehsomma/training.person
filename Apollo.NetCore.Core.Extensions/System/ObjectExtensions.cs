// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// Extensiones para todas las instancias de los objetos.
    /// </summary>
    public static class ObjectExtensions
    {
        #region Methods

        /// <summary>Serealiza el objeto en formato JSON.</summary>
        /// <param name="obj">Objeto a serializar.</param>
        /// <returns>Un string con el objeto en formato JSON.</returns>
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        #endregion
    }
}