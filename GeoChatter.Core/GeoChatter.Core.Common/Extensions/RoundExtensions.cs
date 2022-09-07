using GeoChatter.Core.Common.Extensions;
using GeoChatter.Helpers;
using GeoChatter.Model;
using GeoChatter.Model.Enums;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static GeoChatter.Model.Delegates.GeneralDelegates;

namespace GeoChatter.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Round"/>
    /// </summary>
    public static class RoundExtensions
    {
        /// <summary>
        /// add a guess to the current round
        /// </summary>
        /// <param name="round"></param>
        /// <param name="guess"></param>
        /// <param name="createexp"></param>
        /// <param name="doubleConvert"></param>
        /// <param name="calculator"></param>
        /// <param name="otherrounds"></param>
        public static void AddGuess([NotNull] this Round round,
                                    Guess guess,
                                    Func<Guess, ICollection<Round>, object> createexp,
                                    GetAsDouble doubleConvert,
                                    Script<object> calculator = null,
                                    ICollection<Round> otherrounds = null)
        {
            GCUtils.ThrowIfNull(guess, nameof(guess));

            guess.Round = round;
            
            guess.FinalizeGuessCalculations();
            if (calculator != null)
            {
                try
                {
                    object res = calculator.RunAsync(createexp(guess, otherrounds)).Result.ReturnValue;
                    if (doubleConvert(res, out double d))
                    {
                        guess.Score = d;
                    }
                }
                catch { }
            }

            if (guess.GuessCounter == 1)
            {
                round.Guesses.Add(guess);
            }
        }

        public static string ToStandingsJson([NotNull] this Round round)
        {
            return $"{{\"Round\": {round.ToJson()}, \"Standings\":{round.Game.GetCurrentStandingsAsJson(round.RealRoundNumber())} }}";
        }

        /// <summary>
        /// Get round as json
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public static string ToJson([NotNull] this Round round)
        {
            string s = $@"
            {{
                ""PreferredExportFormat"": ""{round.Game.ParentCache.PreferredExportFormat}"",
                ""AlertOnExportSuccess"": {round.Game.ParentCache.AlertOnExportSuccess.ToStringDefault()},
                ""AutoExportGameResults"": {round.Game.ParentCache.AutoExportGames.ToStringDefault()},
                ""AutoExportRoundResults"": {round.Game.ParentCache.AutoExportRounds.ToStringDefault()},
                ""AutoExportRoundStandings"": {round.Game.ParentCache.AutoExportStandings.ToStringDefault()},
                ""RoundNumber"": {round.RoundNumber}, 
                ""MultiGuessEnabled"": {round.IsMultiGuess.ToStringDefault()}, 
                ""CorrectLocation"": {round.CorrectLocationToJson()},
                ""Guesses"": [{string.Join(",", round
                                                .GetGuessesOrderedByDefaultFilters()
                                                .Select(g => g.ToJson()))}]
            }}";
            return s;
        }

        /// <summary>
        /// Get guesses ordered by current table sorting filters
        /// </summary>
        /// <param name="round"></param>
        /// <returns>New list with guesses ordered</returns>
        public static List<Guess> GetGuessesOrderedByDefaultFilters([NotNull] this Round round)
        {
            return round.Guesses.Count <= 1
                ? round.Guesses.ToList()
                : round.Guesses
                    .BuildOrderBy(round.Game.GetTableOptions().GetDefaultFiltersFor(round.Game.Mode, GameStage.ENDROUND))
                    .ToList();
        }

        /// <summary>
        /// Create a round for a game
        /// </summary>
        /// <param name="game">The game to which the round belongs</param>
        /// <param name="roundNumber">The number of the round</param>
        /// <returns></returns>
        public static Round Create(Game game, int roundNumber)
        {
            Round returnVal = new Round()
            {
                RoundNumber = roundNumber,
                Game = game
            };
            return returnVal;
        }

        /// <summary>
        /// Orders the guess results by score
        /// </summary>
        public static void CalculateResults([NotNull] this Round round)
        {
            try
            {
                if (round.Guesses.Any())
                {
                    round.Guesses
                        .BuildOrderBy(round.Game.GetTableOptions().GetDefaultFiltersFor(round.Game.Mode, GameStage.ENDROUND))
                        .ForEach(y => round.Results.Add(new RoundResult()
                        {
                            Distance = y.Distance,
                            Score = y.Score,
                            Time = y.Time,
                            Streak = y.Streak,
                            Player = round.Game.ParentCache.GetPlayerByIDOrName(y.Player.PlatformId, y.Player.PlayerName, y.Player.DisplayName, y.Player.ProfilePictureUrl, channelName: round.Game.Channel, platform: y.Player.SourcePlatform),
                            Round = y.Round
                        })
                        );
                }
            }
            catch
            {
                throw new InvalidOperationException("Scoreboard has multiple entries of a same player");
            }
        }
        /// <summary>
        /// Serialize <paramref name="round"/> for JS
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public static string CorrectLocationToJson([NotNull] this Round round)
        {
            GGRound original = round.Game.Source.rounds.Count >= round.RoundNumber ?
                round.Game.Source.rounds[round.RoundNumber - 1]
                : null;

            string json = @$"{{
    ""CountryName"":""{round.Country?.Name}"",
    ""CountryCode"":""{round.Country?.Code}"",
    ""ExactCountryName"":""{round.ExactCountry?.Name}"",
    ""ExactCountryCode"":""{round.ExactCountry?.Code}"",
    ""Latitude"": {round.CorrectLocation?.Latitude.ToStringDefault()},
    ""Longitude"": {round.CorrectLocation?.Longitude.ToStringDefault()},
    ""Pano"": ""{original?.panoId ?? ""}"",
    ""Heading"": {original?.heading.ToStringDefault() ?? "0"},
    ""Pitch"": {original?.pitch.ToStringDefault() ?? "0"},
    ""Zoom"": {original?.zoom.ToStringDefault() ?? "0"},
    ""FOV"": {(180D / Math.Pow(2, original?.zoom ?? 0)).ToStringDefault()}
}}";
            return json;
        }

    }
}
