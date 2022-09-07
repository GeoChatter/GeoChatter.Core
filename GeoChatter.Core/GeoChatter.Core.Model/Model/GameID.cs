namespace GeoChatter.Core.Model
{
    /// <summary>
    /// Game id from url
    /// </summary>
    public class GameID
    {
        /// <summary>
        /// Game id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Wheter game is a challenge
        /// </summary>
        public bool IsChallenge { get; set; }
    }
}
