using GeoChatter.Core.Common.Extensions;
using GeoChatter.Helpers;
using GeoChatter.Model;
using GeoChatter.Model.Enums;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GeoChatter.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Player"/>
    /// </summary>
    public static class PlayerExtensions
    {
        /// <summary>
        /// Update player data
        /// </summary>
        /// <param name="original"></param>
        /// <param name="playerName"></param>
        /// <param name="displayname"></param>
        /// <param name="profilepic"></param>
        /// <param name="color"></param>
        public static void Update([NotNull] this Player original, string playerName, string displayname, string profilepic, string color)
        {
            if (!string.IsNullOrEmpty(playerName) && playerName != original.PlayerName)
            {
                original.PlayerName = playerName;
            }

            if (!string.IsNullOrEmpty(displayname) && displayname != original.DisplayName)
            {
                original.DisplayName = displayname;
            }

            if (!string.IsNullOrEmpty(profilepic) && profilepic != original.ProfilePictureUrl)
            {
                original.ProfilePictureUrl = profilepic;
            }

            if (!string.IsNullOrEmpty(color) && original.DisplayName != color)
            {
                original.Color = color;
            }
        }

        /// <summary>
        /// Updates all properties 
        /// </summary>
        /// <param name="playerToUpdate"></param>
        /// <param name="changedPlayer"></param>
        public static void Update([NotNull] this Player playerToUpdate, [NotNull] Player changedPlayer)
        {
            playerToUpdate.BestGame = changedPlayer.BestGame;
            playerToUpdate.BestRound = changedPlayer.BestRound;
            playerToUpdate.BestStreak = changedPlayer.BestStreak;
            playerToUpdate.Channel = changedPlayer.Channel;
            playerToUpdate.Color = changedPlayer.Color;
            playerToUpdate.CorrectCountries = changedPlayer.CorrectCountries;
            playerToUpdate.CountryStreak = changedPlayer.CountryStreak;
            playerToUpdate.DisplayName = changedPlayer.DisplayName;
            playerToUpdate.FirstGuessMade = changedPlayer.FirstGuessMade;
            playerToUpdate.Guesses = changedPlayer.Guesses;
            playerToUpdate.IdOfLastGame = changedPlayer.IdOfLastGame;
            playerToUpdate.IsBanned = changedPlayer.IsBanned;
            playerToUpdate.LastGame = changedPlayer.LastGame;
            playerToUpdate.LastGuess = changedPlayer.LastGuess;
            playerToUpdate.NoOf5kGuesses = changedPlayer.NoOf5kGuesses;
            playerToUpdate.NoOfGuesses = changedPlayer.NoOfGuesses;
            playerToUpdate.NumberOfCountries = changedPlayer.NumberOfCountries;
            playerToUpdate.Perfects = changedPlayer.Perfects;
            playerToUpdate.PlayerFlag = changedPlayer.PlayerFlag;
            playerToUpdate.PlayerFlagName = changedPlayer.PlayerFlagName;
            playerToUpdate.PlayerName = changedPlayer.PlayerName;
            playerToUpdate.ProfilePictureUrl = changedPlayer.ProfilePictureUrl;
            playerToUpdate.RoundNumberOfLastGuess = changedPlayer.RoundNumberOfLastGuess;
            playerToUpdate.StreakBefore = changedPlayer.StreakBefore;
            playerToUpdate.SumOfGuesses = changedPlayer.SumOfGuesses;
            playerToUpdate.TotalDistance = changedPlayer.TotalDistance;
            playerToUpdate.PlatformId = changedPlayer.PlatformId;
            playerToUpdate.Wins = changedPlayer.Wins;

        }
        /// <summary>
        /// Get personal stats message
        /// </summary>
        /// <param name="player"></param>
        /// <param name="distanceUnit"></param>
        /// <returns></returns>
        public static string GetStatsMessage([NotNull] this Player player, Units distanceUnit)
        {
            string msg = $"Current streak: {player.CountryStreak} | Best streak: {player.BestStreak}";

            if (player.NumberOfCountries > 0)
            {
                msg += $" | Correct countries: {player.CorrectCountries}/{player.NumberOfCountries} ({(player.CorrectCountries * 100 / player.NumberOfCountries).ToStringDefault()}%)";
            }

            if (player.NoOfGuesses > 0)
            {
                msg += $" | Avg. score: {Math.Round(player.SumOfGuesses / player.NoOfGuesses).ToStringDefault()}";
            }

            if (player.NoOf5kGuesses > 0)
            {
                msg += $" | Perfect rounds: {player.NoOf5kGuesses}";
            }

            msg += $" | Victories: {player.Wins} | Perfect games: {player.Perfects}";
            //string msg = $"@{player.PlayerName}'s stats are (15sec cooldown): Average score: {Math.Round(player.SumOfGuesses / player.NoOfGuesses).ToStringDefault()} | Streak : {player.CountryStreak} | Best round: {player.BestRound} | Best game: {player.BestGame}";
            if (player.TotalDistance > 0)
            {
                double dist = GameHelper.GetConvertedDistance(player.TotalDistance / player.NoOfGuesses, distanceUnit);

                msg += $" | Avg. distance: {dist.ToStringDefault("F")} {distanceUnit}";
            }
            return msg;
        }

        /// <summary>
        /// Get personal stats message
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string GetStatsJSON([NotNull] this Player player)
        {
            return $@"{{
    ""BestStreak"": {player.BestStreak.ToStringDefault()},
    ""CorrectCountries"": {player.CorrectCountries.ToStringDefault()},
    ""NumberOfCountries"": {player.NumberOfCountries.ToStringDefault()},
    ""SumOfGuesses"": {player.SumOfGuesses.ToStringDefault()},
    ""NoOfGuesses"": {player.NoOfGuesses.ToStringDefault()},
    ""NoOf5kGuesses"": {player.NoOf5kGuesses.ToStringDefault()},
    ""Wins"": {player.Wins.ToStringDefault()},
    ""Perfects"": {player.Perfects.ToStringDefault()},
    ""TotalDistance"": {player.TotalDistance.ToStringDefault()}
}}";
        }

        /// <summary>
        /// Get player data JSON
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string GetPlayerDataJSON([NotNull] this Player player, bool calculateBests = true, bool isStreamer = false)
        {
            return $@"{{
    ""Id"": ""{player.PlatformId}"",
    ""Platform"": {(int)player.SourcePlatform},
    ""Name"": ""{player.PlayerName}"",
    ""Display"": ""{player.FullDisplayName}"",
    ""FlagCode"": ""{player.PlayerFlag}"",
    ""FlagName"": ""{player.PlayerFlagName}"",
    ""MarkerImage"": ""{((player.SourcePlatform == Platforms.GeoGuessr)? "https://www.geoguessr.com/images/auto/144/144/ce/0/plain/"+ player.ProfilePictureUrl:player.ProfilePictureUrl)}"",
    ""MarkerData"": """",
    ""IsStreamer"": {isStreamer.ToStringDefault()},
    ""Color"": ""{player.Color}""
    {(calculateBests ? $",\"Bests\": {player.GetStatsJSON()}" : "")}
}}";
        }
    }
}
