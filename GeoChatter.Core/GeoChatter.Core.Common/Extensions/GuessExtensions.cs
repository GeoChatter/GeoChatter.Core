using GeoChatter.Core.Common.Extensions;
using GeoChatter.Helpers;
using GeoChatter.Model;
using System.Diagnostics.CodeAnalysis;

namespace GeoChatter.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Guess"/>
    /// </summary>
    public static class GuessExtensions
    {
        /// <summary>
        /// Calculate <see cref="Guess.Distance"/>, <see cref="Guess.Time"/> and <see cref="Guess.Score"/> for <paramref name="guess"/>
        /// </summary>
        /// <param name="guess"></param>
        public static void FinalizeGuessCalculations([NotNull] this Guess guess)
        {
            double scale = GameHelper.CalculateScale(guess.Round.Game.Bounds);
            guess.Distance = GameHelper.HaversineDistance(guess.Round.CorrectLocation, guess.GuessLocation);
            double guessTime = (guess.TimeStamp - guess.Round.TimeStamp).TotalMilliseconds;
            guess.Time = guessTime;

            guess.Score = GameHelper.CalculateDefaultScore(guess.Distance, scale);
        }

        /// <summary>
        /// Serialize <paramref name="guess"/> for JS
        /// </summary>
        /// <param name="guess">Guess instance</param>
        /// <param name="color">User name color</param>
        /// <param name="fname">User flag name</param>
        /// <param name="marker">User profile picture URL</param>
        /// <returns></returns>
        public static string ToJson([NotNull] this Guess guess)
        {
            string json = $@"{{
    ""GuessLocation"": {{
        ""Latitude"": {guess.GuessLocation.Latitude.ToStringDefault()},
        ""Longitude"": {guess.GuessLocation.Longitude.ToStringDefault()},
        ""CountryName"": ""{guess.Country.Name.EscapeJSON()}"",
        ""CountryCode"": ""{guess.Country.Code.EscapeJSON()}"",
        ""ExactCountryName"": ""{guess.CountryExact.Name.EscapeJSON()}"",
        ""ExactCountryCode"": ""{guess.CountryExact.Code.EscapeJSON()}"",
        ""Pano"": ""{guess.Pano ?? string.Empty}"",
        ""Heading"": {guess.Heading.ToStringDefault()},
        ""Pitch"": {guess.Pitch.ToStringDefault()},
        ""FOV"": {guess.FOV.ToStringDefault()},
        ""Zoom"": {guess.Zoom.ToStringDefault()}
    }},
    ""PlayerData"": {guess.Player.GetPlayerDataJSON(isStreamer: guess.IsStreamerGuess)},
    ""Distance"": {guess.Distance.ToStringDefault()},
    ""Score"": {guess.Score},
    ""GuessedBefore"": {guess.GuessedBefore.ToStringDefault()},
    ""GuessCount"": {guess.GuessCounter},
    ""IsStreamerGuess"": {guess.IsStreamerGuess.ToStringDefault()},
    ""Source"": ""{guess.Source.EscapeJSON()}"",
    ""Layer"": ""{guess.Layer.EscapeJSON()}"",
    ""WasRandom"": {guess.WasRandom.ToStringDefault()},
    ""RandomGuessArgs"": ""{guess.RandomGuessArgs.EscapeJSON()}"",
    ""CountryStreak"": {guess.Player.CountryStreak},
    ""TimeTaken"": {guess.Time.ToStringDefault()}
}}";
            return json;
        }
    }
}
