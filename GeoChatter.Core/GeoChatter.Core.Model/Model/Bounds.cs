namespace GeoChatter.Model
{
    /// <summary>
    /// Map bounds
    /// </summary>
    public sealed class Bounds
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Coordinates Min { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Coordinates Max { get; set; }
    }
}
