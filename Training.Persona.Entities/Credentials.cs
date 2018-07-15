namespace Training.Persona.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Credenciales para autenticación.
    /// </summary>
    public class Credentials
    {
        #region Properties

        /// <summary>
        /// ApiKey to identify your acount.
        /// </summary>
        [Required]
        public string ApiKey { get; set; }

        /// <summary>Nombre de usuario.</summary>
        public string UserName { get; set; }

        /// <summary>Contraseña de usuario.</summary>
        public string Password { get; set; }

        #endregion
    }
}