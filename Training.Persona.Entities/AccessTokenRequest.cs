namespace Training.Persona.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A request for an AccessToken in order to be authenticated in our system.
    /// </summary>
    public class AccessTokenRequest
    {
        #region Properties

        /// <summary>
        /// ApiKey provided by DotTransfers to identify your acount.
        /// </summary>
        [Required]
        public string ApiKey { get; set; }

        /// <summary>
        /// User name (if nedd an AccessToken from user credentials).
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User password (if nedd an AccessToken from user credentials).
        /// </summary>
        public string Password { get; set; }

        #endregion
    }
}