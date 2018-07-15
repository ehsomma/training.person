namespace Training.Persona.Entities
{
    /// <summary>Contiene información regional.</summary>
    public class RegionalData
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="RegionalData"/> class.</summary>
        public RegionalData()
        {
            this.TimeZone = new TimeZone();
        }

        #endregion

        #region Declarations

        /// <summary>Date format (e.g. "MM/dd/yyyy", "dd/MM/yyyy").</summary>
        public string DateFormat { get; set; }

        /// <summary>Time format (e.g. "hh:mm tt" for 12 hours format, "HH:mm" for 24 hours format).</summary>
        public string TimeFormat { get; set; }

        /// <summary>Language code (e.g. "en", "es", "pt").</summary>
        public string LanguageCode { get; set; }

        /// <summary>A time zone offset from Coordinated Universal Time (UTC) by a whole number of hours (UTC−12 to UTC+14).</summary>
        public TimeZone TimeZone { get; set; }

        /// <summary>Código IATA del país de residencia.</summary>
        public string CountryCode { get; set; }

        /// <summary>Datos serializados de la zona horaria.</summary>
        public string ZonaHoraria { get; set; }

        #endregion
    }
}