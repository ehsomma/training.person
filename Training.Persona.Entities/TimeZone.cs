namespace Training.Persona.Entities
{
    /// <summary>
    /// A time zone offset from Coordinated Universal Time (UTC) by a whole number of hours (UTC−12 to UTC+14).
    /// </summary>
    public class TimeZone
    {
        #region Declarations

        /// <summary>
        /// A string containing the Id of the time zone (e.g. "America/New_York"). These Ids are defined in the IANA Time Zone Database.
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>A string containing the long form name of the time zone. (e.g. "Eastern Daylight Time").</summary>
        public string TimeZoneName { get; set; }

        /// <summary>The amount of time (in seconds) to add to UTC to get standard time in this time zone (the value is not affected by daylight saving time).</summary>
        public string RawOffset { get; set; }

        /// <summary>The offset for daylight-savings time (in seconds).</summary>
        public string DstOffset { get; set; }

        #endregion
    }
}