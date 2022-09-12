using GeoChatter.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeoChatter.Model
{
    /// <summary>
    /// Player model
    /// </summary>
    public sealed class Player
    {
        private string playerFlagName = string.Empty;
        private string displayName;

        /// <summary>
        /// DB id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Channel name of player was created for
        /// </summary>
        [Required]
        public string Channel { get; set; } = "DUMMY";
        /// <summary>
        /// Platform-specific ID of the player
        /// </summary>
        public string PlatformId { get; set; }

        /// <summary>
        /// The platform the user player is registered with
        /// </summary>
        public Platforms SourcePlatform { get; set; }
        /// <summary>
        /// <see cref="DisplayName"/> normalized lowercase
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Full display name for consistency. Shows <see cref="PlayerName"/> as alternative if special script characters present in <see cref="DisplayName"/>
        /// </summary>
        [NotMapped, JsonIgnore]
        public string FullDisplayName => PlayerName?.ToLowerInvariant() == DisplayName?.ToLowerInvariant() && !string.IsNullOrEmpty(DisplayName) && SourcePlatform != Platforms.Twitch
            ? DisplayName
            : $"{DisplayName}({PlayerName})";

        /// <summary>
        /// Flag code
        /// </summary>
        public string PlayerFlag { get; set; }

        /// <summary>
        /// Flag name
        /// </summary>
        public string PlayerFlagName
        {
            get => playerFlagName;
            set => playerFlagName = Country.GetMappedCountryName(value);
        }

        /// <summary>
        /// Current country streak
        /// </summary>
        public int CountryStreak { get; set; }
        /// <summary>
        /// Best country streak
        /// </summary>
        public int BestStreak { get; set; }
        /// <summary>
        /// Amount of guesses done in correct countries
        /// </summary>
        public int CorrectCountries { get; set; }
        /// <summary>
        /// Amount of guesses done in total
        /// </summary>
        public int NumberOfCountries { get; set; }
        /// <summary>
        /// Amount of game wins
        /// </summary>
        public int Wins { get; set; }
        /// <summary>
        /// Amount of perfect games
        /// </summary>
        public int Perfects { get; set; }
        /// <summary>
        /// Average score 
        /// </summary>
        public double OverallAverage => SumOfGuesses / (NoOfGuesses == 0 ? 1 : NoOfGuesses);

        /// <summary>
        /// Best score overall
        /// </summary>
        public double BestGame { get; set; }

        /// <summary>
        /// Best round score overall
        /// </summary>
        public double BestRound { get; set; }
        /// <summary>
        /// Sum of round scores
        /// </summary>
        public double SumOfGuesses
        {
            get;
            set;
        }
        /// <summary>
        /// Total distance of guesses in kilometers (km)
        /// </summary>
        public double TotalDistance
        {
            get;
            set;
        }

        /// <summary>
        /// Last guess coordinates in "{latitude} {longitude}" format
        /// </summary>
        public string LastGuess { get; set; }
        /// <summary>
        /// Number of guesses player's done
        /// </summary>
        public int NoOfGuesses { get; set; }
        /// <summary>
        /// Amount of perfect score rounds
        /// </summary>
        public int NoOf5kGuesses { get; set; }
        /// <summary>
        /// Last game participated
        /// </summary>
        [JsonIgnore]
        public Game LastGame { get; set; }
        /// <summary>
        /// Round number of last guess was made in
        /// </summary>
        public int RoundNumberOfLastGuess { get; set; }
        /// <summary>
        /// Original display name or <see cref="PlayerName"/> if empty
        /// </summary>
        [Required]
        public string DisplayName { get => string.IsNullOrWhiteSpace(displayName) ? PlayerName : displayName; set => displayName = value; }
        /// <summary>
        /// Profile picture URL
        /// </summary>
        [Required]
        public string ProfilePictureUrl { get; set; }
        /// <summary>
        /// User name color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Wheter the player is banned from all actions
        /// </summary>
        public bool IsBanned { get; set; }
        /// <summary>
        /// All guesses made for the player
        /// </summary>
        [JsonIgnore]
        public ICollection<Guess> Guesses { get; set; } = new List<Guess>();
        /// <summary>
        /// Player's streak before processing their last guess in a round
        /// </summary>
        public int StreakBefore { get; set; }
        /// <summary>
        /// Wheter player made their first guess for the current round
        /// </summary>
        public bool FirstGuessMade { get; set; }
        /// <summary>
        /// Game id of the last game participated
        /// </summary>
        public int IdOfLastGame { get; set; }
        /// <summary>
        /// Date of last modification
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return FullDisplayName;
        }
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="twitchId">Twitch ID</param>
        /// <param name="name">User name</param>
        /// <param name="displayName">Display name</param>
        /// <param name="profilePicUrl">Profile picture URL</param>
        /// <returns></returns>
        public static Player Create(string twitchId, string name, string displayName = "", string profilePicUrl = "")
        {
            Player p = new Player()
            {
                PlayerName = name,
                PlatformId = twitchId,
                CountryStreak = 0,
                BestGame = 0,
                BestRound = 0,
                ProfilePictureUrl = profilePicUrl,
                DisplayName = displayName
            };
            return p;

        }

        /// <summary>
        /// Reset all stats
        /// </summary>
        public void ResetStats()
        {
            CountryStreak = 0;
            BestGame = 0;
            CorrectCountries = 0;
            BestStreak = 0;
            BestRound = 0;
            BestGame = 0;
            NoOf5kGuesses = 0;
            Perfects = 0;
            SumOfGuesses = 0;
            Wins = 0;
            NoOfGuesses = 0;
            NumberOfCountries = 0;
        }

        /// <summary>
        /// See <see cref="Create(string, string, string, string)"/>
        /// </summary>
        /// <param name="twitchId"></param>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="profilePicUrl"></param>
        /// <returns></returns>
        public static Player Create(string twitchId, string name, string displayName, Uri profilePicUrl)
        {
            return Create(twitchId, name, displayName, profilePicUrl?.OriginalString ?? string.Empty);
        }
    }
}

