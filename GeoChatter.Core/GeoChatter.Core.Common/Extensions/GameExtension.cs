using GeoChatter.Model.Enums;
using GeoChatter.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeoChatter.Model;
using GeoChatter.Core.Common.Extensions;

namespace GeoChatter.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Game"/>
    /// </summary>
    public static class GameExtension
    {
        /// <summary>
        /// Get a list of guesses from the last finished round
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static Round GetCurrentOrFinishedRound([NotNull] this Game game)
        {
            if (game.CurrentRound <= -1)
            {
                return game.Rounds.Where(r => r.Results.Count != 0).MaxBy(r => r.TimeStamp);
            }
            return game.Rounds.FirstOrDefault(r => r.RoundNumber == game.CurrentRound - 1);
        }

        /// <summary>
        /// Creates a Game with the given parameter
        /// </summary>
        /// <param name="parent">Parent cache</param>
        /// <param name="min">The minimal bounds of the map</param>
        /// <param name="max">The maximal bounds of the map</param>
        /// <param name="channelName">Hosting channel name</param>
        /// <param name="numberOfRounds">The number of rounds, defaulted to 5</param>
        /// <param name="start">Whether to start the game or not, defaulted to true</param>
        /// <returns></returns>
        public static Game Create(IClientDbCache parent, Coordinates min, Coordinates max, string channelName, int numberOfRounds = 5, bool start = false)
        {
            Game returnVal = new Game()
            {
                Bounds = new Bounds() { Min = min?.Copy(), Max = max?.Copy() },
                Channel = channelName,
                Players = new List<Player>(),
                ParentCache = parent,
            };
            for (int i = 1; i <= numberOfRounds; i++)
            {
                returnVal.Rounds.Add(RoundExtensions.Create(returnVal, i));
            }
            if (start)
            {
                returnVal.Start = DateTime.Now;
            }

            return returnVal;
        }

        // TODO: Optimize by caching
        /// <summary>
        /// Add the results of every round together and sort them
        /// </summary>
        /// <param name="game"></param>
        /// <param name="until">inclusive REAL round number (not the round number within <paramref name="game"/>, but in a chain from <see cref="Round.RealRoundNumber"/>)</param>
        public static List<GameResult> GetCurrentStandings([NotNull] this Game game, int until = -1)
        {
            Game g = game;
            List<Round> rs = new List<Round>();

            while (g.Previous != null)
            {
                g = g.Previous;
            }

            while (g.Next != null)
            {
                rs.AddRange(g.Rounds.OrderBy(r => r.RoundNumber));
                g = g.Next;
            }

            rs.AddRange(game.Rounds.OrderBy(r => r.RoundNumber));

            if (until == -1)
            {
                until = rs.Count;
            }

            List<GameResult> res = new List<GameResult>();

            for (int i = 0; i < rs.Count; i++)
            {
                if (i == until)
                {
                    break;
                }

                Round round = rs[i];

                foreach (Guess guess in round.Guesses)
                {

                    GameResult result = res.FirstOrDefault(r => r.Player.PlatformId == guess.Player.PlatformId);
                    if (result != null)
                    {
                        result.Score += guess.Score;
                        result.Distance += guess.Distance;
                        result.Time += guess.Time;
                        result.GuessCount++;
                        result.Streak = guess.Streak;
                    }
                    else
                    {
                        res.Add(new GameResult()
                        {
                            Distance = guess.Distance,
                            Score = guess.Score,
                            Time = guess.Time,
                            Player = game.ParentCache.Players.FirstOrDefault(p => p.PlatformId == guess.Player.PlatformId),
                            Game = game,
                            GuessCount = 1,
                            Streak = guess.Streak,
                        });
                    }
                }

            }
            return res;
        }

        /// <summary>
        /// Serialize <paramref name="game"/> for JS
        /// </summary>
        /// <param name="game"></param>
        /// <param name="until">exclusive round number</param>
        /// <returns></returns>
        public static string GetCurrentStandingsAsJson([NotNull] this Game game, int until = -1)
        {
            string resultJson = string.Empty;

            List<GameResult> rs = game.GetCurrentStandings(until);
            foreach (GameResult result in rs)
            {
                resultJson += result.ToJson() + ",";
            }
            resultJson = resultJson.TrimEnd(',');
            string json = "{\"GameResults\": [" + resultJson + "]}";
            return json;
        }

        /// <summary>
        /// Creates a local game based on the info from GeoGuessr Api
        /// </summary>
        /// <param name="geoGuessrGame"></param>
        /// <param name="parent"></param>
        /// <param name="labelSettings"></param>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public static Game CreateFreshFromGeoGuessrGame([NotNull] this GeoGuessrGame geoGuessrGame, IClientDbCache parent, Dictionary<LabelType, bool> labelSettings, string channelName)
        {
            Game returnVal = new Game()
            {
                GeoGuessrId = geoGuessrGame.token,
                Mode = geoGuessrGame.mode == "streak" ? GameMode.STREAK : GameMode.DEFAULT,
                Channel = channelName,
                Bounds = new Bounds() { Min = new Coordinates(geoGuessrGame.bounds.min.lat, geoGuessrGame.bounds.min.lng), Max = new Coordinates() { Latitude = geoGuessrGame.bounds.max.lat, Longitude = geoGuessrGame.bounds.max.lng } },

                Players = new List<Player>(),

                CurrentRound = geoGuessrGame.round,

                Source = geoGuessrGame,
                IsUsStreak = geoGuessrGame.map == "us-state-streak",
                ParentCache = parent
            };

            labelSettings?.ForEach(ls => { if (!returnVal.LabelSettings.ContainsKey(ls.Key)) { returnVal.LabelSettings.Add(ls.Key, ls.Value); } });

            for (int i = 1; i <= geoGuessrGame.roundCount; i++)
            {
                Round round = RoundExtensions.Create(returnVal, i);
                if (geoGuessrGame.rounds.Count >= i)
                {
                    round.CorrectLocation = new Coordinates(geoGuessrGame.rounds[i - 1].lat, geoGuessrGame.rounds[i - 1].lng);
                }
                returnVal.Rounds.Add(round);
            }
            returnVal.Start = DateTime.Now;

            return returnVal;
        }

        /// <summary>
        /// Creates a local game based on the info from database or GeoGuessr Api
        /// </summary>
        /// <param name="geoGuessrGame"></param>
        /// <param name="labelSettings"></param>
        /// <param name="channelName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Game CreateFromGeoGuessrGame([NotNull] this GeoGuessrGame geoGuessrGame, IClientDbCache parent, Dictionary<LabelType, bool> labelSettings, string channelName, out GameFoundStatus status)
        {
            status = GameFoundStatus.NOTFOUND;
            if (parent.FindGame(geoGuessrGame.token, out status) is Game g)
            {
                labelSettings?.ForEach(ls => { if (!g.LabelSettings.ContainsKey(ls.Key)) { g.LabelSettings.Add(ls.Key, ls.Value); } });
                Round r = g.GetCurrentRound();
                if (r != null && g.CurrentRound <= (geoGuessrGame.rounds?.Count ?? 0))
                {
                    GGRound gr = geoGuessrGame.rounds[g.CurrentRound - 1];
                    r.CorrectLocation = new Coordinates(gr.lat, gr.lng);
                }
                else
                {
                    return null;
                }
                return g;
            }
            else
            {
                switch (status)
                {
                    case GameFoundStatus.FINISHED:
                        {
                            return null;
                        }
                }
                return geoGuessrGame?.CreateFreshFromGeoGuessrGame(parent, labelSettings, channelName);
            }
        }

        private static string GameToJSON([NotNull] this Game game, bool usePrev = false, bool useNext = false, bool firstRoundMultiguess = false)
        {
            if (game == null)
            {
                return "\"\"";
            }

            string s =
            $@"
            {{ 
                ""Id"": ""{game.GeoGuessrId}"",
                ""Streamer"": ""{game.Channel}"",
                ""Mode"": ""{game.Mode}"",
                ""GameSettings"": {{
                    ""TimeLimit"": {game.Source.timeLimit},
                    ""IsUSStreak"": {game.IsUsStreak.ToStringDefault()},
                    ""IsChallenge"": {(game.Source.type == "challenge").ToStringDefault()},
                    ""IsInfinite"": {game.IsPartOfInfiniteGame.ToStringDefault()},
                    ""IsTournament"": {game.IsPartOfTournamentGame.ToStringDefault()},
                    ""MoveEnabled"": {(!game.Source.forbidMoving).ToStringDefault()},
                    ""PanEnabled"": {(!game.Source.forbidRotating).ToStringDefault()},
                    ""ZoomEnabled"": {(!game.Source.forbidZooming).ToStringDefault()}
                }},
                ""GameMapInfo"": {{
                    ""ID"": ""{game.Source.map}"",
                    ""Name"": ""{game.Source.mapName}""
                }},
                ""IsFirstRoundMultiGuess"": {firstRoundMultiguess.ToStringDefault()},
                ""Previous"": {(usePrev && game.Previous != null ? game.Previous.GameToJSON(usePrev: true) : "\"\"")},
                ""Next"": {(useNext && game.Next != null ? game.Next.GameToJSON(useNext: true) : "\"\"")},
                ""RoundsPlayed"": [{string.Join(",",
                    game.Rounds
                        .Where(r => r.Results.Count > 0)
                        .Select(r => r.ToStandingsJson()))}]
            }}";

            return s;
        }

        /// <summary>
        /// Serialize <paramref name="game"/> without <see cref="Game.Results"/>
        /// </summary>
        /// <param name="game"></param>
        /// <param name="firstRoundMultiguess"></param>
        /// <returns></returns>
        public static string ToJson([NotNull] this Game game, bool firstRoundMultiguess = false)
        {
            return game.GameToJSON(true, true, firstRoundMultiguess);
        }

        /// <summary>
        /// Game results as json {GameResults:Array(GameResult)}
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static string ResultsToJson([NotNull] this Game game)
        {
            string resultJson = string.Empty;

            foreach (GameResult result in game.Results)
            {
                resultJson += result.ToJson() + ",";
            }
            resultJson = resultJson.TrimEnd(',');
            return $"{{\"GameResults\": [{resultJson}]}}";
        }
        /// <summary>
        /// Get current round (or round after last ended round)
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static Round GetCurrentRound([NotNull] this Game game)
        {
            return game.Rounds.FirstOrDefault(r => r.RoundNumber == game.CurrentRound);
        }

        /// <summary>
        /// Get current or last played round (or round after last ended round)
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static Round GetCurrentOrLastStartedRound([NotNull] this Game game)
        {
            return game.CurrentRound <= 0 ? game.Rounds.LastOrDefault(r => r.TimeStamp.Date != DateTime.MinValue.Date) : game.GetCurrentRound();
        }

        /// <summary>
        /// Get table options
        /// // TODO: Track this
        /// </summary>
        /// <returns></returns>
        public static ITableOptions GetTableOptions([NotNull] this Game game)
        {
            return game.ParentCache.TableOptions.Load();
        }


        private static void ProcessGameResult(Guess guess, ICollection<GameResult> res)
        {
            if (res.FirstOrDefault(r => r.Player.PlatformId == guess.Player.PlatformId) is GameResult gr)
            {
                gr.Distance += guess.Distance;
                gr.TimeTaken += guess.TimeTaken;
                gr.GuessCount++;
                gr.Score += guess.Score;
                gr.Streak = guess.Streak;
                gr.Player = guess.Player;
                gr.PlayerName = guess.PlayerName;
                gr.Game = guess.Round.Game;
            }
            else
            {
                GameResult grr = new GameResult()
                {
                    Distance = guess.Distance,
                    Game = guess.Round.Game,
                    GuessCount = 1,
                    Score = guess.Score,
                    Streak = guess.Streak,
                    Player = guess.Player,
                    PlayerName = guess.PlayerName,
                    TimeTaken = guess.TimeTaken
                };

                res.Add(grr);
            }
        }

        private static void ProcessGameResult(GameResult result, ICollection<GameResult> res)
        {
            if (res.FirstOrDefault(r => r.Player.PlatformId == result.Player.PlatformId) is GameResult gr)
            {
                gr.Distance += result.Distance;
                gr.TimeTaken += result.TimeTaken;
                gr.GuessCount += result.GuessCount;
                gr.Score += result.Score;
                gr.Streak = result.Streak;
                gr.Player = result.Player;
                gr.PlayerName = result.PlayerName;
                gr.Game = result.Game;
            }
            else
            {
                GameResult grr = new GameResult()
                {
                    Distance = result.Distance,
                    Game = result.Game,
                    GuessCount = result.GuessCount,
                    Score = result.Score,
                    Streak = result.Streak,
                    Player = result.Player,
                    PlayerName = result.PlayerName,
                    TimeTaken = result.TimeTaken
                };

                res.Add(grr);
            }
        }

        /// <summary>
        /// Add the results of every round together and sort them
        /// </summary>
        public static void CalculateResults([NotNull] this Game game, bool endOfInfiniteGame = false)
        {
            game.End = DateTime.Now;
            game.Results = new List<GameResult>();
            if (endOfInfiniteGame)
            {
                Game g = game;
                List<GameResult> res = new List<GameResult>();
                while (g != null)
                {
                    g.Results.ForEach(result => ProcessGameResult(result, res));
                    g = g.Previous;
                }

                game.Results = res;
            }

            game.Rounds
                .OrderBy(r => r.RoundNumber)
                .ForEach(r => r.Guesses
                    .ForEach(g => ProcessGameResult(g, game.Results)));

            game.Results = game.Results
                .BuildOrderBy(game.GetTableOptions().GetDefaultFiltersFor(game.Mode, GameStage.ENDGAME))
                .ToList();
        }
    }
}
