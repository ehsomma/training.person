namespace Training.Persona.Entities
{
    /// <summary>
    /// Estado de cuenta.
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>Estado Activa.</summary>
        Active = 1,

        /// <summary>Estado Inactiva.</summary>
        Inactive = 2,

        /// <summary>Estado Suspendida.</summary>
        Suspended = 4,

        /// <summary>Estado Eliminada (lógico).</summary>
        Deleted = 8
    }
}