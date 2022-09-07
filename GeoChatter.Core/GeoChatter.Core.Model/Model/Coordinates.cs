namespace GeoChatter.Model
{
    /// <summary>
    /// Location coordinates model
    /// </summary>
    public sealed class Coordinates
    {
        /// <summary>
        /// 
        /// </summary>
        public Coordinates()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public Coordinates(double lat, double log)
        {
            Latitude = lat;
            Longitude = log;
        }
        /// <summary>
        /// Id for DB, no assignment needed
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Create a copy
        /// </summary>
        public Coordinates Copy()
        {
            return new Coordinates(Latitude, Longitude);
        }
    }
}
