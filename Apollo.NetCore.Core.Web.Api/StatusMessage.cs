namespace Apollo.NetCore.Core.Web.Api
{
    using System;
    using System.Collections.Generic;

    /// <summary>Contiene un mensaje de estado para responder a una petición que produjo un error.</summary>
    public class StatusMessage
    {
        #region Properties

        /// <summary>HTTP response status code (e.g. 401).</summary>
        public string Code { get; set; }

        /// <summary>HTTP response status code description (e.g. "Unauthorized").</summary>
        public string Descrip { get; set; }

        /// <summary>Unique error identifier for support pourpuse (e.g. "2B2926A1AB7C4B6A99C468561E0A777A").</summary>
        public string ErrorUniqueId { get; set; }

        /// <summary>Error code for business rules treatment (e.g. "ERR.AUTH.ACCESSTOKEN.INVALID").</summary>
        public string ErrorCode { get; set; }

        /// <summary>Date and time (UTC).</summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>Error message (e.g. "AccessToken invalid.").</summary>
        public string Message { get; set; }

        // /// <summary>Detalle del mensaje.</summary>
        // public string MessageDetail { get; set; }

        /// <summary>List of validation errors.</summary>
        public IDictionary<string, string> ValidationErrors { get; set; }

        #endregion
    }
}