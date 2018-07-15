namespace Training.Persona.Entities
{
    /// <summary>
    /// Contains geolocated data from a place.
    /// </summary>
    public class Location
    {
        #region Declarations

        /// <summary>Full location name (e.g. "Miami International Airport (MIA)").</summary>
        public string Name { get; set; }

        /// <summary>Address line (number and street name). (e.g. "1640 NW 42nd Ave, Miami, FL 33126, USA").</summary>
        public string Address { get; set; }

        /// <summary>Complementary address line (door, floor, office, etc). (e.g. "second floor, office 2").</summary>
        public string Address2 { get; set; }

        /// <summary>Full location name and address line. (e.g. "Miami International Airport (MIA) 1640 NW 42nd Ave, Miami, FL 33126, USA").</summary>
        public string Summary { get; set; }

        /// <summary>Country code (ISO 3166-1 alfa-2 ). (e.g. "US").</summary>
        public string CountryCode { get; set; }

        /// <summary>Latitude (WGS-84 format). (e.g. 25.796549).</summary>
        public string Latitude { get; set; }

        /// <summary>Longitude (WGS-84 format). (e.g. -80.275613).</summary>
        public string Longitude { get; set; }

        /// <summary>Location category, such as Address, Airport, Hotel, Political, etc. (e.g. "airport;establishment").</summary>
        public string Category { get; set; }

        /// <summary>Determines if the location has coordinates of a concrete point, for example the locations with political categories are abstract and locations with stablishment categories are concrete.</summary>
        public bool IsConcrete { get; set; }

        /// <summary>The real value inserted by user for a search. (e.g. "miami airport").</summary>
        public string AutocompleteSelectedItem { get; set; }

        /// <summary>A time zone offset from Coordinated Universal Time (UTC) by a whole number of hours (UTC−12 to UTC+14).</summary>
        public TimeZone TimeZone { get; set; }

        #endregion

        #region Mehods
        /// <summary>
        /// Representación del Objeto.
        /// </summary>
        /// <returns>Una string con los datos contenidos.</returns>
        public override string ToString()
        {
            return string.Format(
                "{0} {1} - {2} ({3}), ({4}, {5}), {6}, {7}",
                this.Summary,
                this.Name,
                this.Address,
                this.CountryCode,
                this.Latitude,
                this.Longitude,
                this.Category,
                this.IsConcrete);
        }
        #endregion
    }
}