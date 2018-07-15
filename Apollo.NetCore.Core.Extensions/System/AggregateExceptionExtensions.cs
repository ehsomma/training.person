// ReSharper disable once CheckNamespace
namespace System
{
    using Linq;

    /// <summary>
    /// Extensiones para todas las instancias de AggregateExceptionExtensions.
    /// </summary>
    public static class AggregateExceptionExtensions
    {
        #region Methods

        /// <summary>
        /// Relanza la primera excepción si no es null o la excepción original.
        /// </summary>
        /// <remarks>
        /// En lugar de usar throw ex.InnerExceptions.FirstOrDefault() que daria exepción si en un
        /// AggregateException sin inners exception se usa esta extension.
        /// </remarks>
        /// <param name="aggregateException">Excepción original.</param>
        public static void ThrowFirstOrDefault(this AggregateException aggregateException)
        {
            Exception innerFirstException = aggregateException.InnerExceptions.FirstOrDefault();
            if (innerFirstException != null)
            {
                throw innerFirstException;
            }
            else
            {
                throw aggregateException;
            }
        }

        #endregion
    }
}