namespace GeoChatter.Model.Interfaces
{
    /// <summary>
    /// Guess model
    /// </summary>
    public interface ISortableModel
    {
        /// <summary>
        /// Time taken
        /// </summary>
        public double TimeTaken { get; set; }

        /// <summary>
        /// Distance in kilometers (km)
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Score calculated for the guess
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Owner of the guess
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Country streak after guess
        /// </summary>
        public int Streak { get; set; }
    }
}
