// ReSharper disable InconsistentNaming

namespace Training.Persona.UnitTests
{
    using System;
    using System.Collections.Generic;

    using Training.Persona.Entities;

    public static class PaisMockGenerator
    {
        #region Methods

        /// <summary>
        /// Devuelve el pais especificado simulando un insert.
        /// </summary>
        /// <param name="pais">El pais a devolver.</param>
        /// <returns>Un pais.</returns>
        public static Pais Insert(Pais pais)
        {
            return pais;
        }

        /// <summary>
        /// Asigna el nombre especificado al pais especificado simulando un update.
        /// </summary>
        /// <param name="pais">El Pais a editar.</param>
        /// <param name="nombre">El nombre a asignar.</param>
        /// <returns>El Pais con el nuevo nombre.</returns>
        public static Pais UpdateNombre(Pais pais, string nombre)
        {
            pais.Nombre = nombre;

            return pais;
        }

        /// <summary>
        /// Devuelve una página especifica de una lista paginada de paises con la cantidad de 
        /// elementos especificados.
        /// </summary>
        /// <param name="count">Cantidad de elementos a agregar a la lista.</param>
        /// <param name="pageIndex">Número de página a solicitar.</param>
        /// <param name="pageSize">Cantidad de ítems por página.</param>
        /// <returns>Una lista paginada de paises.</returns>
        public static PagedList<Pais> GetPageFromList(int count, int pageIndex, int pageSize)
        {
            PagedList<Pais> pagedListPais = new PagedList<Pais>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemCount = pageSize
            };

            // Se filtran los elementos de la página solicitada.
            List<Pais> items = GetList(count).GetRange((pageIndex - 1) * pageSize, pageSize);
            pagedListPais.Items = items;

            return pagedListPais;
        }

        /// <summary>
        /// Devuelve el pais correspondiente al codigoIata especificado.
        /// </summary>
        /// <param name="codigoIata">Código IATA del país solicitado.</param>
        /// <returns>El Pais solicitado.</returns>
        public static Pais GetByCodigoIata(string codigoIata)
        {
            List<Pais> paises = GetAll();

            Pais ret = paises.Find(p => p.CodigoIata.Equals(codigoIata));
            return ret;
        }

        /// <summary>
        /// Devuelve una lista de Pais según la cantidad especificada de elementos.
        /// </summary>
        /// <param name="count">Cantidad de elementos a agregar a la lista.</param>
        /// <returns>Una lista paginada de paises.</returns>
        private static List<Pais> GetList(int count)
        {
            List<Pais> listPais = GetAll().GetRange(0, count);
            return listPais;
        }

        /// <summary>
        /// Devuelve una lista con todos los paises del mundo.
        /// </summary>
        /// <returns>Una lista de pais con todos los paises del mundo.</returns>
        private static List<Pais> GetAll()
        {
            List<Pais> paises = new List<Pais>()
            {
                new Pais() { CodigoIata = "AD", Nombre = "Andorra" },
                new Pais() { CodigoIata = "AE", Nombre = "United Arab Emirates" },
                new Pais() { CodigoIata = "AF", Nombre = "Afghanistan" },
                new Pais() { CodigoIata = "AG", Nombre = "Antigua" },
                new Pais() { CodigoIata = "AG", Nombre = "Barbuda" },
                new Pais() { CodigoIata = "AI", Nombre = "Anguilla" },
                new Pais() { CodigoIata = "AL", Nombre = "Albania" },
                new Pais() { CodigoIata = "AM", Nombre = "Armenia" },
                new Pais() { CodigoIata = "AO", Nombre = "Angola" },
                new Pais() { CodigoIata = "AR", Nombre = "Argentina" },
                new Pais() { CodigoIata = "AS", Nombre = "American Samoa" },
                new Pais() { CodigoIata = "AT", Nombre = "Austria" },
                new Pais() { CodigoIata = "AU", Nombre = "Australia" },
                new Pais() { CodigoIata = "AW", Nombre = "Aruba" },
                new Pais() { CodigoIata = "AZ", Nombre = "Azerbaijan" },
                new Pais() { CodigoIata = "BA", Nombre = "Bosnia-Herzegovina" },
                new Pais() { CodigoIata = "BB", Nombre = "Barbados" },
                new Pais() { CodigoIata = "BD", Nombre = "Bangladesh" },
                new Pais() { CodigoIata = "BE", Nombre = "Belgium" },
                new Pais() { CodigoIata = "BF", Nombre = "Burkina Faso" },
                new Pais() { CodigoIata = "BG", Nombre = "Bulgaria" },
                new Pais() { CodigoIata = "BH", Nombre = "Bahrain" },
                new Pais() { CodigoIata = "BI", Nombre = "Burundi" },
                new Pais() { CodigoIata = "BJ", Nombre = "Benin" },
                new Pais() { CodigoIata = "BM", Nombre = "Bermuda" },
                new Pais() { CodigoIata = "BN", Nombre = "Brunei" },
                new Pais() { CodigoIata = "BO", Nombre = "Bolivia" },
                new Pais() { CodigoIata = "BQ", Nombre = "Bonaire" },
                new Pais() { CodigoIata = "BQ", Nombre = "Saba" },
                new Pais() { CodigoIata = "BQ", Nombre = "St. Eustatius" },
                new Pais() { CodigoIata = "BR", Nombre = "Brazil" },
                new Pais() { CodigoIata = "BS", Nombre = "Bahamas" },
                new Pais() { CodigoIata = "BT", Nombre = "Bhutan" },
                new Pais() { CodigoIata = "BW", Nombre = "Botswana" },
                new Pais() { CodigoIata = "BY", Nombre = "Belarus" },
                new Pais() { CodigoIata = "BZ", Nombre = "Belize" },
                new Pais() { CodigoIata = "CA", Nombre = "Canada" },
                new Pais() { CodigoIata = "CD", Nombre = "Congo, Dem. Rep. of" },
                new Pais() { CodigoIata = "CG", Nombre = "Congo" },
                new Pais() { CodigoIata = "CH", Nombre = "Switzerland" },
                new Pais() { CodigoIata = "CI", Nombre = "Ivory Coast" },
                new Pais() { CodigoIata = "CK", Nombre = "Cook Islands" },
                new Pais() { CodigoIata = "CL", Nombre = "Chile" },
                new Pais() { CodigoIata = "CM", Nombre = "Cameroon" },
                new Pais() { CodigoIata = "CN", Nombre = "China," },
                new Pais() { CodigoIata = "CO", Nombre = "Colombia" },
                new Pais() { CodigoIata = "CR", Nombre = "Costa Rica" },
                new Pais() { CodigoIata = "CS", Nombre = "Serbia and Montenegro" },
                new Pais() { CodigoIata = "CV", Nombre = "Cape Verde" },
                new Pais() { CodigoIata = "CW", Nombre = "Curacao" },
                new Pais() { CodigoIata = "CY", Nombre = "Cyprus" },
                new Pais() { CodigoIata = "CZ", Nombre = "Czech Republic" },
                new Pais() { CodigoIata = "DE", Nombre = "Germany" },
                new Pais() { CodigoIata = "DJ", Nombre = "Djibouti" },
                new Pais() { CodigoIata = "DK", Nombre = "Denmark" },
                new Pais() { CodigoIata = "DM", Nombre = "Dominica" },
                new Pais() { CodigoIata = "DO", Nombre = "Dominican Republic" },
                new Pais() { CodigoIata = "DZ", Nombre = "Algeria" },
                new Pais() { CodigoIata = "EC", Nombre = "Ecuador" },
                new Pais() { CodigoIata = "EE", Nombre = "Estonia" },
                new Pais() { CodigoIata = "EG", Nombre = "Egypt" },
                new Pais() { CodigoIata = "ER", Nombre = "Eritrea" },
                new Pais() { CodigoIata = "ES", Nombre = "Spain" },
                new Pais() { CodigoIata = "ET", Nombre = "Ethiopia" },
                new Pais() { CodigoIata = "FI", Nombre = "Finland" },
                new Pais() { CodigoIata = "FJ", Nombre = "Fiji" },
                new Pais() { CodigoIata = "FM", Nombre = "Micronesia" },
                new Pais() { CodigoIata = "FO", Nombre = "Faeroe Islands" },
                new Pais() { CodigoIata = "FR", Nombre = "France" },
                new Pais() { CodigoIata = "GA", Nombre = "Gabon" },
                new Pais() { CodigoIata = "GB", Nombre = "Great Britain" },
                new Pais() { CodigoIata = "GD", Nombre = "Grenada" },
                new Pais() { CodigoIata = "GE", Nombre = "Georgia, Republic of" },
                new Pais() { CodigoIata = "GF", Nombre = "French Guiana" },
                new Pais() { CodigoIata = "GH", Nombre = "Ghana" },
                new Pais() { CodigoIata = "GI", Nombre = "Gibraltar" },
                new Pais() { CodigoIata = "GL", Nombre = "Greenland" },
                new Pais() { CodigoIata = "GM", Nombre = "Gambia" },
                new Pais() { CodigoIata = "GN", Nombre = "Guinea" },
                new Pais() { CodigoIata = "GP", Nombre = "Guadeloupe" },
                new Pais() { CodigoIata = "GP", Nombre = "St. Barthelemy" },
                new Pais() { CodigoIata = "GR", Nombre = "Greece" },
                new Pais() { CodigoIata = "GT", Nombre = "Guatemala" },
                new Pais() { CodigoIata = "GU", Nombre = "Guam" },
                new Pais() { CodigoIata = "GY", Nombre = "Guyana" },
                new Pais() { CodigoIata = "HK", Nombre = "Hong Kong" },
                new Pais() { CodigoIata = "HN", Nombre = "Honduras" },
                new Pais() { CodigoIata = "HR", Nombre = "Croatia" },
                new Pais() { CodigoIata = "HT", Nombre = "Haiti" },
                new Pais() { CodigoIata = "HU", Nombre = "Hungary" },
                new Pais() { CodigoIata = "ID", Nombre = "Indonesia" },
                new Pais() { CodigoIata = "IE", Nombre = "Ireland, Republic of" },
                new Pais() { CodigoIata = "IL", Nombre = "Israel" },
                new Pais() { CodigoIata = "IN", Nombre = "India" },
                new Pais() { CodigoIata = "IQ", Nombre = "Iraq" },
                new Pais() { CodigoIata = "IS", Nombre = "Iceland" },
                new Pais() { CodigoIata = "IT", Nombre = "Italy" },
                new Pais() { CodigoIata = "IT", Nombre = "San Marino" },
                new Pais() { CodigoIata = "IT", Nombre = "Vatican" },
                new Pais() { CodigoIata = "JM", Nombre = "Jamaica" },
                new Pais() { CodigoIata = "JO", Nombre = "Jordan" },
                new Pais() { CodigoIata = "JP", Nombre = "Japan" },
                new Pais() { CodigoIata = "KE", Nombre = "Kenya" },
                new Pais() { CodigoIata = "KG", Nombre = "Kyrgyzstan" },
                new Pais() { CodigoIata = "KH", Nombre = "Cambodia" },
                new Pais() { CodigoIata = "KN", Nombre = "St. Kitts and Nevis" },
                new Pais() { CodigoIata = "KR", Nombre = "Korea, South" },
                new Pais() { CodigoIata = "KW", Nombre = "Kuwait" },
                new Pais() { CodigoIata = "KY", Nombre = "Cayman Islands" },
                new Pais() { CodigoIata = "KZ", Nombre = "Kazakhstan" },
                new Pais() { CodigoIata = "LA", Nombre = "Laos" },
                new Pais() { CodigoIata = "LB", Nombre = "Lebanon" },
                new Pais() { CodigoIata = "LC", Nombre = "St. Lucia" },
                new Pais() { CodigoIata = "LI", Nombre = "Liechtenstein" },
                new Pais() { CodigoIata = "LK", Nombre = "Sri Lanka" },
                new Pais() { CodigoIata = "LR", Nombre = "Liberia" },
                new Pais() { CodigoIata = "LS", Nombre = "Lesotho" },
                new Pais() { CodigoIata = "LT", Nombre = "Lithuania" },
                new Pais() { CodigoIata = "LU", Nombre = "Luxembourg" },
                new Pais() { CodigoIata = "LV", Nombre = "Latvia" },
                new Pais() { CodigoIata = "LY", Nombre = "Libya" },
                new Pais() { CodigoIata = "MA", Nombre = "Morocco" },
                new Pais() { CodigoIata = "MC", Nombre = "Monaco" },
                new Pais() { CodigoIata = "MD", Nombre = "Moldova" },
                new Pais() { CodigoIata = "MF", Nombre = "St. Martin" },
                new Pais() { CodigoIata = "MG", Nombre = "Madagascar" },
                new Pais() { CodigoIata = "MH", Nombre = "Marshall Islands" },
                new Pais() { CodigoIata = "MK", Nombre = "Macedonia" },
                new Pais() { CodigoIata = "ML", Nombre = "Mali" },
                new Pais() { CodigoIata = "MN", Nombre = "Mongolia" },
                new Pais() { CodigoIata = "MO", Nombre = "Macau" },
                new Pais() { CodigoIata = "MP", Nombre = "Saipan" },
                new Pais() { CodigoIata = "MQ", Nombre = "Martinique" },
                new Pais() { CodigoIata = "MR", Nombre = "Mauritania" },
                new Pais() { CodigoIata = "MS", Nombre = "Montserrat" },
                new Pais() { CodigoIata = "MT", Nombre = "Malta" },
                new Pais() { CodigoIata = "MU", Nombre = "Mauritius" },
                new Pais() { CodigoIata = "MV", Nombre = "Maldives, Republic of" },
                new Pais() { CodigoIata = "MW", Nombre = "Malawi" },
                new Pais() { CodigoIata = "MX", Nombre = "Mexico" },
                new Pais() { CodigoIata = "MY", Nombre = "Malaysia" },
                new Pais() { CodigoIata = "MZ", Nombre = "Mozambique" },
                new Pais() { CodigoIata = "NA", Nombre = "Namibia" },
                new Pais() { CodigoIata = "NC", Nombre = "New Caledonia" },
                new Pais() { CodigoIata = "NE", Nombre = "Niger" },
                new Pais() { CodigoIata = "NG", Nombre = "Nigeria" },
                new Pais() { CodigoIata = "NI", Nombre = "Nicaragua" },
                new Pais() { CodigoIata = "NL", Nombre = "Netherlands" },
                new Pais() { CodigoIata = "NO", Nombre = "Norway" },
                new Pais() { CodigoIata = "NP", Nombre = "Nepal" },
                new Pais() { CodigoIata = "NZ", Nombre = "New Zealand" },
                new Pais() { CodigoIata = "OM", Nombre = "Oman" },
                new Pais() { CodigoIata = "PA", Nombre = "Panama" },
                new Pais() { CodigoIata = "PE", Nombre = "Peru" },
                new Pais() { CodigoIata = "PF", Nombre = "French Polynesia" },
                new Pais() { CodigoIata = "PG", Nombre = "Papua New Guinea" },
                new Pais() { CodigoIata = "PH", Nombre = "Philippines" },
                new Pais() { CodigoIata = "PK", Nombre = "Pakistan" },
                new Pais() { CodigoIata = "PL", Nombre = "Poland" },
                new Pais() { CodigoIata = "PS", Nombre = "Palestine" },
                new Pais() { CodigoIata = "PT", Nombre = "Portugal" },
                new Pais() { CodigoIata = "PW", Nombre = "Palau" },
                new Pais() { CodigoIata = "PY", Nombre = "Paraguay" },
                new Pais() { CodigoIata = "QA", Nombre = "Qatar" },
                new Pais() { CodigoIata = "RE", Nombre = "Reunion" },
                new Pais() { CodigoIata = "RO", Nombre = "Romania" },
                new Pais() { CodigoIata = "RU", Nombre = "Russia" },
                new Pais() { CodigoIata = "RW", Nombre = "Rwanda" },
                new Pais() { CodigoIata = "SA", Nombre = "Saudi Arabia" },
                new Pais() { CodigoIata = "SC", Nombre = "Seychelles" },
                new Pais() { CodigoIata = "SE", Nombre = "Sweden" },
                new Pais() { CodigoIata = "SG", Nombre = "Singapore" },
                new Pais() { CodigoIata = "SI", Nombre = "Slovenia" },
                new Pais() { CodigoIata = "SK", Nombre = "Slovak Republic" },
                new Pais() { CodigoIata = "SN", Nombre = "Senegal" },
                new Pais() { CodigoIata = "SR", Nombre = "Suriname" },
                new Pais() { CodigoIata = "SV", Nombre = "El Salvador" },
                new Pais() { CodigoIata = "SX", Nombre = "St. Maarten​" },
                new Pais() { CodigoIata = "SY", Nombre = "Syria" },
                new Pais() { CodigoIata = "SZ", Nombre = "Swaziland" },
                new Pais() { CodigoIata = "TC", Nombre = "Turks and Caicos Islands" },
                new Pais() { CodigoIata = "TD", Nombre = "Chad" },
                new Pais() { CodigoIata = "TG", Nombre = "Togo" },
                new Pais() { CodigoIata = "TH", Nombre = "Thailand" },
                new Pais() { CodigoIata = "TL", Nombre = "East Timor" },
                new Pais() { CodigoIata = "TN", Nombre = "Tunisia" },
                new Pais() { CodigoIata = "TO", Nombre = "Tonga" },
                new Pais() { CodigoIata = "TR", Nombre = "Turkey" },
                new Pais() { CodigoIata = "TT", Nombre = "Trinidad and Tobago" },
                new Pais() { CodigoIata = "TW", Nombre = "Taiwan," },
                new Pais() { CodigoIata = "TZ", Nombre = "Tanzania" },
                new Pais() { CodigoIata = "UA", Nombre = "Ukraine" },
                new Pais() { CodigoIata = "UG", Nombre = "Uganda" },
                new Pais() { CodigoIata = "US", Nombre = "Puerto Rico" },
                new Pais() { CodigoIata = "US", Nombre = "U.S.A." },
                new Pais() { CodigoIata = "UY", Nombre = "Uruguay" },
                new Pais() { CodigoIata = "UZ", Nombre = "Uzbekistan" },
                new Pais() { CodigoIata = "VC", Nombre = "St. Vincent" },
                new Pais() { CodigoIata = "VE", Nombre = "Venezuela" },
                new Pais() { CodigoIata = "VG", Nombre = "British Virgin Islands" },
                new Pais() { CodigoIata = "VI", Nombre = "U.S. Virgin Islands" },
                new Pais() { CodigoIata = "VN", Nombre = "Vietnam" },
                new Pais() { CodigoIata = "VU", Nombre = "Vanuatu" },
                new Pais() { CodigoIata = "WF", Nombre = "Wallis & Futuna Islands" },
                new Pais() { CodigoIata = "WS", Nombre = "Samoa" },
                new Pais() { CodigoIata = "YE", Nombre = "Yemen, The Republic of" },
                new Pais() { CodigoIata = "ZA", Nombre = "South African Republic" },
                new Pais() { CodigoIata = "ZM", Nombre = "Zambia" },
                new Pais() { CodigoIata = "ZW", Nombre = "Zimbabwe" },
            };

            return paises;
        }

        #endregion
    }
}