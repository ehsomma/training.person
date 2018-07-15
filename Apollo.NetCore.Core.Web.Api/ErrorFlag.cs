namespace Apollo.NetCore.Core.Web.Api
{
    /// <summary>
    /// Constantes para flags en excepciones.
    /// </summary>
    public class ErrorFlag
    {
        #region Declarations

        /// <summary>Flag que se asigna al HttpContext del API para determinar si el middleware debe generar un response personalizado con un StatusMessage.</summary>
        public const string CustomError = "APOLLO.CUSTOMERROR";

        #endregion
    }
}