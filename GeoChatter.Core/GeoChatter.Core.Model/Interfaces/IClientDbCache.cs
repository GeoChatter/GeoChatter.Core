using GeoChatter.Model;
using GeoChatter.Model.Enums;
using System.Collections.Generic;

namespace GeoChatter.Model.Interfaces
{
    /// <summary>
    /// Guess model
    /// </summary>
    public interface IClientDbCache
    {
        /// <summary>
        /// Table options to use
        /// </summary>
        public ITableOptions TableOptions { get; set; }

        /// <summary>
        /// Next game's game options
        /// </summary>
        public GameOption NextGameGameOptions { get; set; }

        /// <summary>
        /// Next round's round options
        /// </summary>
        public RoundOption NextRoundRoundOptions { get; set; }
        /// <summary>
        /// xlsx or csv
        /// </summary>
        public string PreferredExportFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AlertOnExportSuccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AutoExportGames { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AutoExportRounds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AutoExportStandings { get; set; }

        /// <summary>
        /// Players cache
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Get game by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="status">Status wheter game was found and if not found the reason</param>
        /// <returns></returns>
        public Game FindGame(string gameId, out GameFoundStatus status);

        /// <summary>
        /// Get player by Twitch ID or name
        /// </summary>
        /// <param name="id">Twitch ID</param>
        /// <param name="name">Backup Twitch Name</param>
        /// <param name="channelName">Channel name</param>
        /// <returns></returns>
        public Player GetPlayerByTwitchIDOrName(string id, string name = "", string channelName = "");
        public Player GetPlayerByYtIDOrName(string id, string name = "", string displayName = "", string profilePicUrl = "", string channelName = "");
        public Player GetPlayerByIDOrName(string id, string name = "", string displayName = "", string profilePicUrl = "", string channelName = "", bool isStreamer = false, Platforms platform = Platforms.Unknown);
    }
}
