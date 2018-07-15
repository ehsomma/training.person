namespace Training.Persona.Entities
{
    /// <summary>
    /// The access Token to be authenticated in our system.
    /// </summary>
    public class AccessToken
    {
        #region Properties

        /// <summary>AccessToken to autenticate the user.</summary>
        public string Token { get; set; }

        /// <summary>In how many minutes will expire the AccessToken.</summary>
        public int? ExpiresIn { get; set; }

        /// <summary>Maximum number of hits for the AccessToken. If the value is null, there is no limit.</summary>
        public int? MaxHits { get; set; }

        #endregion
    }
}