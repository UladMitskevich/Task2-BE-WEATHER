namespace WEATHER.API.Dto
{
    /// <summary>
    /// Represents the data transfer object for weather information.
    ///should be Guids for Copuntry and City but omitted intentionally for simplicity
    /// </summary>
    public class WeatherDto: IAuditableDto// : IDto, IAUditableDto etc...
    {
        /// <summary>
        /// The unique identifier of the weather data
        /// </summary>
        public int Id { get; set; }//GUIUD but now int
        
        /// <summary>
        /// The name of the city
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// The name of the city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        ///Min Temperature
        /// </summary>
        public decimal MinTemperature { get; set; }
        /// <summary>
        /// Max Temperature
        /// </summary>
        public decimal MaxTemperature { get; set; }
        ///// <summary>
        ///// Created date of the weather data
        ///// </summary>
        //public DateTime Created { get; set; }
        /// <summary>
        /// The date when the weather data was last modified
        /// </summary>
        public DateTime Modified { get; set; }
        //etc auditable fields
    }
}
