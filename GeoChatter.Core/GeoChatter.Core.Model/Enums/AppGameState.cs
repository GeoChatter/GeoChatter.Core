namespace GeoChatter.Model.Enums
{
    /// <summary>
    /// App's stages
    /// </summary>
    [System.Flags]
    public enum AppGameState
    {
        /// <summary>
        /// Any game stage
        /// </summary>
        ANYTIME = 1 << 0,
        /// <summary>
        /// Not in a game
        /// </summary>
        NOTINGAME = 1 << 1,
        /// <summary>
        /// In a game, round stage
        /// </summary>
        INROUND = 1 << 2,
        /// <summary>
        /// In a game, round summary stage
        /// </summary>
        ROUNDEND = 1 << 3,
        /// <summary>
        /// In a game, game summary stage
        /// </summary>
        GAMEEND = 1 << 4,
    }

    /// <summary>
    /// Client connection states
    /// </summary>
    public enum ConnectionState
    {
        /// <summary>
        /// Unknown state
        /// </summary>
        UNKNOWN = -2,
        /// <summary>
        /// Disconnected
        /// </summary>
        DISCONNECTED = -1,
        /// <summary>
        /// Connecting
        /// </summary>
        CONNECTING = 0,
        /// <summary>
        /// Reconnecting
        /// </summary>
        RECONNECTING = 1,
        /// <summary>
        /// Connected
        /// </summary>
        CONNECTED = 2,
    }
}
