namespace Training.Persona.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>Datos de una Persona.</summary>
    public class Persona
    {
        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="Persona"/> class.</summary>
        public Persona()
        {
            this.Bienes = new List<Bien>();
            this.RegionalData = new RegionalData();
        }

        #endregion

        #region Declarations

        /// <summary>Identificador de la entidad.</summary>
        public int Id { get; set; }

        /// <summary>Apellido y nombre/s.</summary>
        public string NombreCompleto { get; set; }

        /// <summary>Dirección de correo electrónico.</summary>
        public string EMail { get; set; }

        /// <summary>Fecha de creación de este registro (UTC).</summary>
        public DateTime FechaCreo { get; set; }

        /// <summary>Fecha de la última modificación de este registro (UTC).</summary>
        public DateTime FechaActualizo { get; set; }

        /// <summary>Importe total de los ahorros.</summary>
        public decimal TotalAhorro { get; set; }

        /// <summary>Porcentaje de ahorro sobre el sueldo.</summary>
        public decimal PorcAhorro { get; set; }

        /// <summary>Observaciones o comentarios.</summary>
        public string Obs { get; set; }

        /// <summary>Dirección completa.</summary>
        public string Direccion { get; set; }

        /// <summary>Latitud de la dirección.</summary>
        public decimal Lat { get; set; }

        /// <summary>Longitud de la dirección.</summary>
        public decimal Lon { get; set; }

        /// <summary>Estado de la persona.</summary>
        public AccountStatus Estado { get; set; }

        /// <summary>Fecha de nacimiento.</summary>
        public DateTime FechaNacimiento { get; set; }

        /// <summary>Determina se se le envían o no notificaciones.</summary>
        public bool RecibirNotificaciones { get; set; }

        /// <summary>Información regional.</summary>
        public RegionalData RegionalData { get; set; }

        /// <summary>Bienes personales.</summary>
        public IList<Bien> Bienes { get; set; }

        /// <summary>Tipo de sexo (usar clase Sexo).</summary>
        public string Sexo { get; set; }

        /// <summary>Password para autenticación.</summary>
        public string Password { get; set; }
        
        #endregion
    }
}