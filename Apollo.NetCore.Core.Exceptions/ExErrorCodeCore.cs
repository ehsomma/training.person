namespace Apollo.NetCore.Core.Exceptions
{
    /// <summary>
    /// Constantes para códigos de error de Excepciones y StatusMessage.
    /// </summary>
    public class ExErrorCodeCore
    {
        #region Declarations

        /// <summary>The Authorization header is missing or has an invalid format (format: Authorization:Bearer {yourToken}).</summary>
        public const string ErrAuthAuthorizationHeader = "ERR.AUTH.AUTHORIZATIONHEADER";

        /// <summary>AccessToken inválido.</summary>
        public const string ErrAuthAccesstokenInvalid = "ERR.AUTH.ACCESSTOKEN.INVALID";

        /// <summary>AccessToken vencido.</summary>
        public const string ErrAuthAccesstokenExpired = "ERR.AUTH.ACCESSTOKEN.EXPIRED";

        /// <summary>AccessToken solicitado desde IP no permitida.</summary>
        public const string ErrAuthAccesstokenIpNotAllowed = "ERR.AUTH.ACCESSTOKEN.IPNOTALLOWED";

        /// <summary>AccessToken con cantidad de usos (hits) superada.</summary>
        public const string ErrAuthAccesstokenOverHitsLimit = "ERR.AUTH.ACCESSTOKEN.OVERHITSLIMIT";

        /// <summary>Server Timeout.</summary>
        public const string ErrTimeout = "ERR.TIMEOUT";

        #endregion
    }
}