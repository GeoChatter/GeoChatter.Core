using GeoChatter.Core.Model.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace GeoChatter.Model
{
    /// <summary>
    /// Round flags/options
    /// </summary>
    [Flags]
    public enum RoundOption
    {
        /// <summary>
        /// Default round options, nothing special
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// Multiguessing enabled
        /// </summary>
        MULTIGUESS = 1 << 0,
    }

    /// <summary>
    /// Game round model
    /// </summary>
    public sealed class Round
    {
        /// <summary>
        /// DB id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Correct location
        /// </summary>
        public Coordinates CorrectLocation { get; set; }
        /// <summary>
        /// Game this round is from
        /// </summary>
        [NotMapped, JsonIgnore]
        public Game Game { get; set; }
        /// <summary>
        /// Round number within <see cref="Game"/>, starting from 1
        /// </summary>
        public int RoundNumber { get; set; }
        /// <summary>
        /// Round flags
        /// </summary>
        public RoundOption Flags { get; set; } = RoundOption.DEFAULT;

        /// <summary>
        /// Wheter round is a multiguess enabled round
        /// </summary>
        [NotMapped, JsonIgnore]
        public bool IsMultiGuess => (Flags & RoundOption.MULTIGUESS) > 0;
        /// <summary>
        /// The round settings for the map
        /// </summary>
        public MapRoundSettings MapRoundSettings { get; set; }
        /// <summary>
        /// All guesses made during this round
        /// </summary>
        public ICollection<Guess> Guesses { get; set; } = new List<Guess>();

        [NotMapped]
        private ICollection<RoundResult> results = new List<RoundResult>();
        /// <summary>
        /// Results of the round if it has completed
        /// </summary>
        public ICollection<RoundResult> Results
        {
            get
            {
                if (results == null || !results.Any())
                {
                    //CalculateResults();
                }


                return results;
            }
            set => results = value;
        }
        /// <summary>
        /// Country information of <see cref="CorrectLocation"/>
        /// </summary>
        public Country Country { get; set; } = Country.Unknown;

        /// <summary>
        /// Exact region information of <see cref="CorrectLocation"/>
        /// </summary>
        public Country ExactCountry { get; set; } = Country.Unknown;

        /// <summary>
        /// Time the round was first started
        /// </summary>
        public DateTime TimeStamp { get; set; }

        [NotMapped, JsonIgnore]
        public GGRound Source
        {
            get
            {
                GGRound original = Game.Source.rounds.Count >= RoundNumber ?
                Game.Source.rounds[RoundNumber - 1]
                : null;
                return original;
            }
        }

/// <summary>
/// Create a round
/// </summary>
        public Round() { }

        /// <summary>
        /// Get real round number, in case its a part of an infinite game
        /// </summary>
        /// <returns></returns>
        public int RealRoundNumber()
        {
            return Game.IsPartOfInfiniteGame ? ((Game.PositionInChainFromStart - 1) * 5) + RoundNumber : RoundNumber;
        }
    }
}
