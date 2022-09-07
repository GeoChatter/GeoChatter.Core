using GeoChatter.Core.Common.Extensions;
using GeoChatter.Model;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GeoChatter.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="GameResult"/> and <see cref="RoundResult"/>
    /// </summary>
    public static class ResultExtension
    {
        /// <summary>
        /// Game result to json
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToJson([NotNull] this GameResult result)
        {
            Player player = result.Game.ParentCache.GetPlayerByIDOrName(result.Player.PlatformId, result.Player.PlayerName, result.Player.DisplayName, result.Player.ProfilePictureUrl, result.Game.Channel, platform:result.Player.SourcePlatform);
            // TODO: Find a better way to determine this
            bool isStreamer = (result.Game.GetCurrentOrFinishedRound()?.Guesses.FirstOrDefault(g => g.IsStreamerGuess)?.Player.Id ?? -1) == player.Id;
            return @$"{{
    ""PlayerData"": {player?.GetPlayerDataJSON(false, isStreamer)},
    ""Distance"": {result.Distance.ToStringDefault()},
    ""CountryStreak"": {result.Streak},
    ""Score"": {result.Score.ToStringDefault()},
    ""GuessCount"": {result.GuessCount},
    ""TimeTaken"": {result.Time.ToStringDefault()}
}}";
        }
    }
}
