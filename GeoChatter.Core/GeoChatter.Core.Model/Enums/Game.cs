namespace GeoChatter.Model.Enums
{

    /// <summary>
    /// Game stages
    /// </summary>
    public enum GameStage
    {
        /// <summary>
        /// Round being played
        /// </summary>
        INROUND,
        /// <summary>
        /// Round summary screen
        /// </summary>
        ENDROUND,
        /// <summary>
        /// Game summary screen
        /// </summary>
        ENDGAME
    }

    /// <summary>
    /// Status used to determine the result of a game search in db
    /// </summary>
    public enum GameFoundStatus
    {
        /// <summary>
        /// A game was found
        /// </summary>
        FOUND,
        /// <summary>
        /// No game was found
        /// </summary>
        NOTFOUND,
        /// <summary>
        /// A game was found but it was finished
        /// </summary>
        FINISHED
    }

}
