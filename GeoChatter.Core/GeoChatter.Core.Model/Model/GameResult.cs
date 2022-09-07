using GeoChatter.Model.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using GeoChatter.Core.Model.Extensions;
namespace GeoChatter.Model
{
    /// <summary>
    /// Model for the game result of a player
    /// </summary>
    public sealed class GameResult : ISortableModel
    {
        private double time;

        /// <summary>
        /// 
        /// </summary>
        public GameResult()
        {

        }
        /// <summary>
        /// DB id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Amount of guesses made
        /// </summary>
        public int GuessCount { get; set; }
        /// <summary>
        /// Player reference
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Game reference
        /// </summary>
        [JsonIgnore]
        public Game Game { get; set; }
        /// <summary>
        /// Total guess score by country features hit
        /// </summary>
        [NotMapped, JsonIgnore]
        public double GuessPoint => Game.Rounds
                    .Select(r => r.Results.FirstOrDefault(rr => rr.Player.PlatformId == Player.PlatformId))
                    .Where(r => r != null)
                    .Select(r => r.GuessPoint)
                    .Sum();

        /// <summary>
        /// Total guess score by country features hit
        /// </summary>
        [NotMapped, JsonIgnore]
        public double CorrectTotal => Game.Rounds
                    .Select(r => r.Results.FirstOrDefault(rr => rr.Player.PlatformId == Player.PlatformId))
                    .Where(roundresult =>
                    {
                        if (roundresult == null)
                        {
                            return false;
                        }

                        Guess guess = roundresult.GetGuessOf();
                        return guess.Country.Code == roundresult.Round.Country.Code &&
                                    guess.Country.Name == roundresult.Round.Country.Name &&
                                    (!Game.IsUsStreak || (guess.CountryExact.Code == roundresult.Round.ExactCountry.Code &&
                                        guess.CountryExact.Name == roundresult.Round.ExactCountry.Name));
                    })
                    .Count();
        /// <inheritdoc/>
        public double Distance { get; set; }
        /// <summary>
        /// Game score
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Country streak after guess
        /// </summary>
        public int Streak { get; set; }
        /// <summary>
        /// Total time spent in milliseconds before guessing, can be <see cref="long.MaxValue"/> max
        /// </summary>
        public double Time { get => time; set => time = value > long.MaxValue ? long.MaxValue : (long)value; }
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        public double TimeTaken { get => Time; set => Time = value; }
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        public string PlayerName
        {
            get => Player?.PlayerName;
            set
            {
                if (Player != null)
                {
                    Player.PlayerName = value;
                }
            }
        }
    }
}
