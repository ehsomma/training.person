namespace Apollo.NetCore.Core.Settings
{
    using System.Collections.Generic;

    /// <summary>
    /// Settings para ConnectionString.
    /// </summary>
    public class ConnectionStrings : Dictionary<string, string>
    {
        #region Properties

        /// <summary>Obtiene el connection string correspondiente a "DefaultConnection".</summary>
        public string DefaultConnection
        {
            get
            {
                return this["DefaultConnection"];
            }
        }

        #endregion
    }
}