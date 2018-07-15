namespace Training.Persona.Entities
{
    /// <summary>
    /// Bienes personales de una persona.
    /// </summary>
    public class Bien
    {
        #region Properties

        /// <summary>Identificador del bien.</summary>
        public int Id { get; set; }

        /// <summary>Tipo de bien (Mueble, Inmueble).</summary>
        public string Tipo { get; set; }

        /// <summary>Descripción del bien.</summary>        
        public string Descripcion { get; set; }

        /// <summary>Valor del bien.</summary>
        public decimal Valor { get; set; }

        #endregion
    }
}