using GeoChatter.Model.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using GeoChatter.Core.Model.Extensions;
namespace GeoChatter.Model
{
    /// <summary>
    /// Round result model for players
    /// </summary>
    public sealed class RoundResult : ISortableModel
    {
        /// <summary>
        /// DB id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Round this result is from
        /// </summary>
        [JsonIgnore]
        public Round Round { get; set; }
        /// <summary>
        /// Player who this result belongs to
        /// </summary>
        public Player Player { get; set; }
        //public Player Player { get; set; }
        /// <summary>
        /// Distance in kilometers (km)
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Round score
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Time taken in milliseconds for <see cref="Player"/> to make their last guess
        /// </summary>
        public double Time { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        public double TimeTaken { get => Time; set => Time = value; }

       
        [NotMapped, JsonIgnore]
        public double GuessPoint
        {
            get
            {
                Guess guess = this.GetGuessOf();
                return Round.ExactCountry.Code != Round.Country.Code
                                                ? guess.CountryExact.Code == Round.ExactCountry.Code
                                                    ? 2
                                                    : guess.Country.Code == Round.Country.Code ? 1 : 0
                                                : guess.Country.Code == Round.Country.Code ? 2 : 0;
            }
        }


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
        /// <summary>
        /// Current country streak
        /// </summary>
        public int Streak { get; set; }
    }
}
