namespace Training.Persona.Entities
{
    /// <summary>
    /// Constantes para códigos de error de Excepciones y StatusMessage.
    /// </summary>
    public class ExErrorCode
    {
        #region Declarations

        /// <summary>Credenciales inválidas.</summary>
        public const string ErrAuthInvalidCredentials = "ERR.AUTH.INVALIDCREDENTIALS";

        /// <summary>Cuenta de Empresa o Pasajero inactiva.</summary>
        public const string ErrAuthInactiveAccount = "ERR.AUTH.INACTIVEACCOUNT";

        /// <summary>Ya existe una Persona con ese email.</summary>
        public const string ErrPersonaEmailExist = "ERR.PERSONA.EMAILEXIST";

        /// <summary>Persona no encontrada al buscar por id.</summary>
        public const string ErrPersonaNotFound = "ERR.PERSONA.NOTFOUND";

        /// <summary>Al mandar "prueba" en las observaciones simula un error de negocio.</summary>
        public const string ErrPersonaPruebaValidacionBusiness = "ERR.PERSONA.PRUEBAVALIDACIONBUSINESS";

        /// <summary>Ya existe un país con ese código IATA.</summary>
        public const string ErrPaisIataCodeExist = "ERR.PAIS.IATACODEEXIST";

        /// <summary>No existe un país con ese código IATA.</summary>
        public const string ErrPaisNotFound = "ERR.PAIS.NOTFOUND";

        #endregion
    }
}