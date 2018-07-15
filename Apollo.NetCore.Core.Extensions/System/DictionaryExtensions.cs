// ReSharper disable once CheckNamespace
namespace System
{
    using System.Collections;
    using System.Collections.Generic;

    using Linq;

    /// <summary>
    /// Extensiones para todas las instancias de Dictionary.
    /// </summary>
    public static class DictionaryExtensions
    {
        #region Methods

        /// <summary>
        /// Obtiene un valor del diccionario especificado mediante la llave especificada ignorando
        /// si la llave esta o viene en mayúscula o minúscula.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de los elementos del diccionario.</typeparam>
        /// <param name="dictionary">Diccionario en donde se va a buscar.</param>
        /// <param name="key">Llave a buscar.</param>
        /// <returns>Valor correspondiente a la llave o null.</returns>
        public static TEntity GetValueIgnoreCase<TEntity>(this IDictionary<string, TEntity> dictionary, string key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TEntity value = default(TEntity);
            string[] keys = dictionary.Keys.ToArray();
            int i = 0;
            int l = keys.Length;
            while (i < l
                   && string.Compare(keys[i], key, StringComparison.OrdinalIgnoreCase) != 0)
            {
                i++;
            }

            if (i < l)
            {
                value = dictionary[keys[i]];
            }

            return value;
        }

        /// <summary>
        /// Obtiene un valor del diccionario especificado mediante la llave especificada ignorando
        /// si la llave esta o viene en mayúscula o minúscula.
        /// </summary>
        /// <param name="dictionary">Diccionario en donde se va a buscar.</param>
        /// <param name="key">Llave a buscar.</param>
        /// <returns>Valor correspondiente a la llave o null.</returns>
        public static object GetValueIgnoreCase(this IDictionary dictionary, string key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            object value = null;
            List<string> keys = new List<string>();
            foreach (object item in dictionary.Keys)
            {
                keys.Add((string)item);
            }

            int i = 0;
            int l = keys.Count;
            while (i < l
                   && string.Compare(keys[i], key, StringComparison.OrdinalIgnoreCase) != 0)
            {
                i++;
            }

            if (i < l)
            {
                value = dictionary[keys[i]];
            }

            return value;
        }

        #endregion
    }
}