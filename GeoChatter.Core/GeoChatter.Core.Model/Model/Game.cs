using GeoChatter.Core.Model.Map;
using GeoChatter.Model.Enums;
using GeoChatter.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace GeoChatter.Model
{
    /// <summary>
    /// Geoguessr game mode
    /// </summary>
    public enum GameMode
    {
        /// <summary>
        /// Default 5 round game
        /// </summary>
        DEFAULT,
        /// <summary>
        /// Streaks mode
        /// </summary>
        STREAK
    }

    /// <summary>
    /// Game flags/options
    /// </summary>
    [Flags]
    public enum GameOption
    {
        /// <summary>
        /// Default game options, nothing special
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// Is in a chain of infinite games
        /// </summary>
        INFINITE = 1 << 0,

        /// <summary>
        /// Is in a chain of tournament games
        /// </summary>
        TOURNAMENT = 1 << 1,
    }

    /// <summary>
    /// Game model
    /// </summary>
    public sealed class Game
    {
        /// <summary>
        /// Gamemode name for US State streaks
        /// </summary>
        public const string USStreaksGame = "usstatestreak";
        private List<Player> players;

        /// <summary>
        /// Parent dbcache instance
        /// </summary>
        [NotMapped, JsonIgnore]
        public IClientDbCache ParentCache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Game()
        {
            Players = new List<Player>();
            Rounds = new List<Round>();
            Results = new List<GameResult>();
        }

        /// <summary>
        /// Previous game in the chain
        /// </summary>
        [JsonIgnore]
        public Game Previous { get; set; }

        /// <summary>
        /// Next game in the chain
        /// </summary>
        public Game Next { get; set; }

        /// <summary>
        /// DB game id, no need to assign
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Channel the game was hosted by
        /// </summary>
        public string Channel { get; set; }

        public MapGameSettings MapGameSettings { get; set; }

        /// <summary>
        /// Time of the game started
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Time of the last round ending
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// GeoGuessr game id
        /// </summary>
        public string GeoGuessrId { get; set; }

        /// <summary>
        /// Wheter a <see cref="GameMode.STREAK"/> game is US streaks
        /// </summary>
        public bool IsUsStreak { get; set; }

        /// <summary>
        /// Wheter game is part of an infinite chain of games
        /// </summary>
        [NotMapped, JsonIgnore]
        public bool IsPartOfInfiniteGame => (Flags & GameOption.INFINITE) > 0;

        /// <summary>
        /// Wheter game is part of a tournament chain of games
        /// </summary>
        [NotMapped, JsonIgnore]
        public bool IsPartOfTournamentGame => (Flags & GameOption.TOURNAMENT) > 0;

        /// <summary>
        /// Wheter game is part of a chain of games
        /// </summary>
        [NotMapped, JsonIgnore]
        public bool IsInAChainOfGames => (Flags & (GameOption.INFINITE | GameOption.TOURNAMENT)) > 0;

        /// <summary>
        /// Label settings
        /// </summary>
        [NotMapped, JsonIgnore]
        public Dictionary<LabelType, bool> LabelSettings { get; } = new Dictionary<LabelType, bool>();

        /// <summary>
        /// Map's bounds
        /// </summary>
        public Bounds Bounds { get; set; }

        /// <summary>
        /// Game option flags
        /// </summary>
        public GameOption Flags { get; set; } = GameOption.DEFAULT;

        /// <summary>
        /// Game mode
        /// </summary>
        public GameMode Mode { get; set; } = GameMode.DEFAULT;

        /// <summary>
        /// Current round number starting from 1.
        /// <para>-2 for finished infinite games</para>
        /// <para>-1 for finished games</para>
        /// </summary>
        public int CurrentRound { get; set; }

        /// <summary>
        /// Position 1-based in a game chain for infinite games
        /// </summary>
        public int PositionInChainFromStart { get; set; } = 1;

        /// <summary>
        /// All players 
        /// </summary>
        public ICollection<Player> Players
        {
            get
            {
                players?.RemoveAll(item => item == null);
                return players;
            }
            set
            {
                if (value is List<Player> p)
                {

                    p.RemoveAll(item => item == null);
                    players = p;
                }
                else
                {
                    players = new List<Player>(value);
                }
            }
        }

        /// <summary>
        /// Game rounds
        /// </summary>
        public ICollection<Round> Rounds { get; set; }

        /// <summary>
        /// Game results
        /// </summary>
        public ICollection<GameResult> Results { get; set; }

        /// <summary>
        /// Base game instance
        /// </summary>
        public GeoGuessrGame Source { get; set; }

    }
}
