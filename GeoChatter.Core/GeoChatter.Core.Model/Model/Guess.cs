using GeoChatter.Model.Enums;
using GeoChatter.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoChatter.Model
{
    /// <summary>
    /// Guess model
    /// </summary>
    public class Guess : ISortableModel
    {
        /// <summary>
        /// DB id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Time taken from the start of the <see cref="Round.TimeStamp"/> in milliseconds
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Time taken
        /// </summary>
        [NotMapped, JsonIgnore]
        public double TimeTaken
        {
            get => Time;
            set => Time = value;
        }

        /// <summary>
        /// Guessed location
        /// </summary>
        public Coordinates GuessLocation { get; set; }
        /// <summary>
        /// Distance in kilometers (km)
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Wheter this guess was not the first for <see cref="Player"/> in <see cref="Round"/>
        /// </summary>
        public bool GuessedBefore { get; set; }
        /// <summary>
        /// Wheter <see cref="Player"/> is the streamer
        /// </summary>
        public bool IsStreamerGuess { get; set; }
        /// <summary>
        /// Wheter <see cref="GuessLocation"/> was randomly created
        /// </summary>
        public bool WasRandom { get; set; }
        /// <summary>
        /// Wheter <see cref="GuessLocation"/> was randomly created
        /// </summary>
        public bool IsTemporary { get; set; }
        /// <summary>
        /// Round this guess belongs to
        /// </summary>
        [JsonIgnore]
        public Round Round { get; set; }

        /// <summary>
        /// Score calculated for the guess
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// What source the guess originated from: MAP, EXT, CHAT
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Which layer the guess was made from
        /// </summary>
        public string Layer { get; set; }
        /// <summary>
        /// Guess point for sorting
        /// </summary>
        [NotMapped, JsonIgnore]
        public string GuessPoint => CountryExact.Code;

        /// <summary>
        /// Owner of the guess
        /// </summary>
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
        /// Country streak after guess
        /// </summary>
        public int Streak { get; set; }



        /// <summary>
        /// Pano id of the guessed location or <see langword="null"/>
        /// </summary>
        public string Pano { get; set; }

        /// <summary>
        /// Heading in degrees
        /// </summary>
        public double Heading { get; set; }

        /// <summary>
        /// Pitch in degrees
        /// </summary>
        public double Pitch { get; set; }

        /// <summary>
        /// FOV in degrees
        /// </summary>
        public double FOV { get; set; }

        /// <summary>
        /// Zoom level. 
        /// </summary>
        public double Zoom { get; set; }

        /// <summary>
        /// Owner of the guess
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Time guess began the processing stage
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Country information of the guess
        /// </summary>
        public Country Country { get; set; } = Country.Unknown;

        /// <summary>
        /// Exact region information of the guess
        /// </summary>
        public Country CountryExact { get; set; } = Country.Unknown;
        /// <summary>
        /// Amount of guesses made in <see cref="Round"/> for <see cref="Player"/>
        /// </summary>
        public int GuessCounter { get; set; }
        /// <summary>
        /// Guess's current processing state
        /// </summary>
        public GuessState State { get; set; }
        /// <summary>
        /// Bot name <see cref="Round.Game"/> was responding to
        /// </summary>
        public string Bot { get; set; }
    }
}
