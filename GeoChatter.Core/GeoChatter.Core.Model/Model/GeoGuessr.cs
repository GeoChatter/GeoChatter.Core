using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS1591
namespace GeoChatter.Model
{

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GeoGuessrGame
    {
        public int Id { get; set; }
        public string token { get; set; }
        public string type { get; set; }
        public string mode { get; set; }
        public string state { get; set; }
        public int roundCount { get; set; }
        public int timeLimit { get; set; }
        public bool forbidMoving { get; set; }
        public bool forbidZooming { get; set; }
        public bool forbidRotating { get; set; }
        public string streakType { get; set; }
        public string map { get; set; }
        public string mapName { get; set; }
        public int panoramaProvider { get; set; }
        public GGBounds bounds { get; set; }
        public int round { get; set; }
        public List<GGRound> rounds { get; set; }
        public GGPlayer player { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGMin
    {
        public int Id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGMax
    {
        public int Id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGBounds
    {
        public int Id { get; set; }
        public GGMin min { get; set; }
        public GGMax max { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGRound
    {
        public int Id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string panoId { get; set; }
        public double heading { get; set; }
        public double pitch { get; set; }
        public double zoom { get; set; }
        public string streakLocationCode { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGTotalScore
    {
        public int Id { get; set; }
        public string amount { get; set; }
        public string unit { get; set; }
        public double percentage { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGMeters
    {
        public int Id { get; set; }
        public string amount { get; set; }
        public string unit { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGMiles
    {
        public int Id { get; set; }
        public string amount { get; set; }
        public string unit { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGTotalDistance
    {
        public int Id { get; set; }
        public GGMeters meters { get; set; }
        public GGMiles miles { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGPin
    {
        public int Id { get; set; }
        public string url { get; set; }
        public string anchor { get; set; }
        public bool isDefault { get; set; }
    }

    public sealed class GGGuess
    {
        public int Id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public bool timedOut { get; set; }
        public bool timedOutWithGuess { get; set; }
        public GGRoundScore roundScore { get; set; }
        public double roundScoreInPercentage { get; set; }
        public int roundScoreInPoints { get; set; }
        public GGTotalDistance distance { get; set; }
        public double distanceInMeters { get; set; }
        public string streakLocationCode { get; set; }
        public int time { get; set; }
    }

    public sealed class GGRoundScore
    {
        public int Id { get; set; }
        public string amount { get; set; }
        public string unit { get; set; }
        public double percentage { get; set; }
    }

    /// <summary>
    /// Model GeoGuessr API uses
    /// </summary>
    public sealed class GGPlayer
    {
        [Key]
        public int GcId { get; set; }
        public GGTotalScore totalScore { get; set; }
        public GGTotalDistance totalDistance { get; set; }
        public double totalDistanceInMeters { get; set; }
        public int totalTime { get; set; }
        public int totalStreak { get; set; }
        public List<GGGuess> guesses { get; set; }
        public bool isLeader { get; set; }
        public int currentPosition { get; set; }
        public GGPin pin { get; set; }
        public string id { get; set; }
        public string nick { get; set; }
        public bool isVerified { get; set; }
    }

    public sealed class GGImages
    {
        public object backgroundLarge { get; set; }
        public bool incomplete { get; set; }
    }

    public sealed class GGOnboarding
    {
        public object tutorialToken { get; set; }
        public string tutorialState { get; set; }
    }

    public sealed class GGBr
    {
        public int level { get; set; }
        public int division { get; set; }
        public int streak { get; set; }
    }

    public sealed class GGTitle
    {
        public int id { get; set; }
        public int tierId { get; set; }
    }

    public sealed class GGDivision
    {
        public int id { get; set; }
        public int divisionId { get; set; }
        public int tierId { get; set; }
        public int type { get; set; }
        public int startRating { get; set; }
        public int endRating { get; set; }
    }

    public sealed class GGBrRank
    {
        public int rating { get; set; }
        public int? rank { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public GGDivision division { get; set; }
    }

    public sealed class GGCsRank
    {
        public int rating { get; set; }
        public int? rank { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public GGDivision division { get; set; }
    }

    public sealed class GGCompetitionMedals
    {
        public int bronze { get; set; }
        public int silver { get; set; }
        public int gold { get; set; }
        public int platinum { get; set; }
    }

    public sealed class GGStreaks
    {
        public int brCountries { get; set; }
        public int brDistance { get; set; }
        public int csCities { get; set; }
        public int duels { get; set; }
    }

    public sealed class GGProgress
    {
        public int xp { get; set; }
        public int level { get; set; }
        public int levelXp { get; set; }
        public int nextLevel { get; set; }
        public int nextLevelXp { get; set; }
        public GGTitle title { get; set; }
        public GGBrRank brRank { get; set; }
        public GGCsRank csRank { get; set; }
        public GGDuelsRank duelsRank { get; set; }
        public GGCompetitionMedals competitionMedals { get; set; }
        public GGStreaks streaks { get; set; }
    }

    public sealed class GGCompetitive
    {
        public int elo { get; set; }
        public int rating { get; set; }
        public int lastRatingChange { get; set; }
        public GGDivision division { get; set; }
    }

    public sealed class GGCreator
    {
        public object email { get; set; }
        public string nick { get; set; }
        public DateTime created { get; set; }
        public bool isProUser { get; set; }
        public bool consumedTrial { get; set; }
        public bool isVerified { get; set; }
        public GGPin pin { get; set; }
        public int color { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string countryCode { get; set; }
        public GGOnboarding onboarding { get; set; }
        public GGBr br { get; set; }
        public object streakProgress { get; set; }
        public object explorerProgress { get; set; }
        public int dailyChallengeProgress { get; set; }
        public GGProgress progress { get; set; }
        public GGCompetitive competitive { get; set; }
        public DateTime lastNameChange { get; set; }
    }

    public sealed class GGAvatar
    {
        public string background { get; set; }
        public string decoration { get; set; }
        public string ground { get; set; }
        public string landscape { get; set; }
    }

    public sealed class GGBrowsableMap
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string playUrl { get; set; }
        public bool published { get; set; }
        public bool banned { get; set; }
        public GGImages images { get; set; }
        public Bounds bounds { get; set; }
        public object customCoordinates { get; set; }
        public string coordinateCount { get; set; }
        public object regions { get; set; }
        public GGCreator creator { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int numFinishedGames { get; set; }
        public object likedByUser { get; set; }
        public int averageScore { get; set; }
        public GGAvatar avatar { get; set; }
        public string difficulty { get; set; }
        public int difficultyLevel { get; set; }
        public object highscore { get; set; }
        public bool isUserMap { get; set; }
        public bool highlighted { get; set; }
        public bool free { get; set; }
        public bool inExplorerMode { get; set; }
        public int maxErrorDistance { get; set; }
        public int likes { get; set; }
    }

    public sealed class GGExplorableMap
    {
        public int bestScore { get; set; }
        public string medal { get; set; }
    }

    public sealed class GGExplorer
    {
        public Dictionary<string, GGExplorableMap> Maps { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGData
    {
        public string map { get; set; }
    }

    public sealed class GGActivity
    {
        public string activityType { get; set; }
        public DateTime timestamp { get; set; }
        public GGData data { get; set; }
    }

    public sealed class GGFriend
    {
        public string userId { get; set; }
        public string url { get; set; }
        public string nick { get; set; }
        public GGPin pin { get; set; }
        public bool isProUser { get; set; }
        public bool isVerified { get; set; }
        public GGProgress progress { get; set; }
        public bool isOnline { get; set; }
        public GGActivity activity { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public sealed class GGStreakProgress
    {
        public int bronze { get; set; }
        public int silver { get; set; }
        public int gold { get; set; }
        public int platinum { get; set; }
    }

    public sealed class GGExplorerProgress
    {
        public int bronze { get; set; }
        public int silver { get; set; }
        public int gold { get; set; }
        public int platinum { get; set; }
    }

    public sealed class GGUser
    {
        public object email { get; set; }
        public string nick { get; set; }
        public DateTime created { get; set; }
        public bool isProUser { get; set; }
        public bool consumedTrial { get; set; }
        public bool isVerified { get; set; }
        public GGPin pin { get; set; }
        public int color { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string countryCode { get; set; }
        public GGOnboarding onboarding { get; set; }
        public GGBr br { get; set; }
        public GGStreakProgress streakProgress { get; set; }
        public GGExplorerProgress explorerProgress { get; set; }
        public int dailyChallengeProgress { get; set; }
        public GGProgress progress { get; set; }
        public GGCompetitive competitive { get; set; }
        public DateTime lastNameChange { get; set; }
    }

    public sealed class GGPlayingRestriction
    {
        public int restriction { get; set; }
        public bool canPlayGame { get; set; }
        public string description { get; set; }
        public object ticket { get; set; }
        public object periodicAllowanceMetadata { get; set; }
    }

    public sealed class GGEmailNotificationSettings
    {
        public bool sendLeagueNotifications { get; set; }
        public bool sendDailyChallengeNotifications { get; set; }
        public bool sendGeneralNotifications { get; set; }
        public DateTime lastCampaignSent { get; set; }
        public string unsubscribeToken { get; set; }
    }

    public sealed class GGProfile
    {
        public GGUser user { get; set; }
        public GGPlayingRestriction playingRestriction { get; set; }
        public string email { get; set; }
        public bool isEmailChangeable { get; set; }
        public bool isEmailVerified { get; set; }
        public GGEmailNotificationSettings emailNotificationSettings { get; set; }
        public bool isBanned { get; set; }
        public int distanceUnit { get; set; }
        public bool hideCustomAvatars { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGBadge
    {
        public string id { get; set; }
        public string name { get; set; }
        public string hint { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
        public bool hasLevels { get; set; }
        public string category { get; set; }
        public bool applyRounding { get; set; }
        public int level { get; set; }
        public DateTime awarded { get; set; }
    }

    public sealed class GGAchievements
    {
        public int totalMedals { get; set; }
        public List<GGBadge> recentBadges { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGMapSettings
    {
        public int initialHealth { get; set; }
        public int roundTime { get; set; }
        public string mapSlug { get; set; }
        public bool forbidMoving { get; set; }
        public bool forbidZooming { get; set; }
        public bool forbidRotating { get; set; }
        public string duelRoundOptions { get; set; }
        public int? duration { get; set; }
        public int? roundInterval { get; set; }
        public int? lives { get; set; }
        public int? maxLives { get; set; }
        public int? lifebonusFrequency { get; set; }
        public int? lifebonusAmountMin { get; set; }
        public int? lifebonusAmountMax { get; set; }
        public int? checkpointFrequency { get; set; }
        public int? checkpointLifeRefillAmount { get; set; }
        public int? initialAlternatives { get; set; }
        public int? alternativesIncreasePerRound { get; set; }
        public int? initialLives { get; set; }
        public int? reservationWindowTime { get; set; }
        public List<object> powerUps { get; set; }
        public bool? resetLivesEachRound { get; set; }
        public int? extraLivesEachRound { get; set; }
        public int? guessCooldown { get; set; }
    }

    public sealed class GGMap
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string playUrl { get; set; }
        public bool published { get; set; }
        public bool banned { get; set; }
        public GGImages images { get; set; }
        public Bounds bounds { get; set; }
        public object customCoordinates { get; set; }
        public object coordinateCount { get; set; }
        public object regions { get; set; }
        public GGCreator creator { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int numFinishedGames { get; set; }
        public object likedByUser { get; set; }
        public int averageScore { get; set; }
        public GGAvatar avatar { get; set; }
        public object difficulty { get; set; }
        public int difficultyLevel { get; set; }
        public object highscore { get; set; }
        public bool isUserMap { get; set; }
        public bool highlighted { get; set; }
        public bool free { get; set; }
        public bool inExplorerMode { get; set; }
        public int maxErrorDistance { get; set; }
        public int likes { get; set; }
    }

    public sealed class GGCompetition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string startType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isPublished { get; set; }
        public GGMapSettings settings { get; set; }
        public string difficulty { get; set; }
        public List<object> tags { get; set; }
        public bool didParticipate { get; set; }
        public string medal { get; set; }
        public GGMap map { get; set; }
        public object competitionResult { get; set; }
    }

    public sealed class GGCompetitions
    {
        public List<GGCompetition> competitions { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public sealed class GGPersonalized
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string playUrl { get; set; }
        public bool published { get; set; }
        public bool banned { get; set; }
        public GGImages images { get; set; }
        public GGBounds bounds { get; set; }
        public object customCoordinates { get; set; }
        public string coordinateCount { get; set; }
        public object regions { get; set; }
        public object creator { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int numFinishedGames { get; set; }
        public object likedByUser { get; set; }
        public int averageScore { get; set; }
        public GGAvatar avatar { get; set; }
        public string difficulty { get; set; }
        public int difficultyLevel { get; set; }
        public object highscore { get; set; }
        public bool isUserMap { get; set; }
        public bool highlighted { get; set; }
        public bool free { get; set; }
        public bool inExplorerMode { get; set; }
        public int maxErrorDistance { get; set; }
        public int likes { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGSentBy
    {
        public string id { get; set; }
        public string nick { get; set; }
        public bool isVerified { get; set; }
        public GGAvatar avatar { get; set; }
    }

    public sealed class GGNotification
    {
        public string id { get; set; }
        public int type { get; set; }
        public GGSentBy sentBy { get; set; }
        public DateTime sentAt { get; set; }
        public bool isRead { get; set; }
        public string payload { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGStyle
    {
        public string background { get; set; }
    }

    public sealed class GGOpenGame
    {
        public string gameId { get; set; }
        public string gameType { get; set; }
        public int totalSpots { get; set; }
        public int numOpenSpots { get; set; }
        public List<GGPlayer> players { get; set; }
        public object creator { get; set; }
        public DateTime createdAt { get; set; }
        public object isParticipating { get; set; }
        public string mapSlug { get; set; }
        public string mapName { get; set; }
    }

    public sealed class GGActiveGame
    {
        public string gameId { get; set; }
        public string gameType { get; set; }
        public int totalSpots { get; set; }
        public int numOpenSpots { get; set; }
        public List<GGPlayer> players { get; set; }
        public object creator { get; set; }
        public DateTime createdAt { get; set; }
        public object isParticipating { get; set; }
        public string mapSlug { get; set; }
        public string mapName { get; set; }
    }

    public sealed class GGLeaderboard
    {
        public List<GGEntry> entries { get; set; }
        public int paginateFrom { get; set; }
        public object me { get; set; }
    }

    public class GGPartyGame
    {
        public string id { get; set; }
        public string shareLink { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public GGCreator creator { get; set; }
        public GGStyle style { get; set; }
        public List<object> players { get; set; }
        public List<GGOpenGame> openGames { get; set; }
        public List<GGActiveGame> activeGames { get; set; }
        public List<object> bannedPlayers { get; set; }
        public bool requiresPassword { get; set; }
        public int playerCount { get; set; }
        public int numSpotsRemaining { get; set; }
        public int maxNumSpots { get; set; }
        public GGLeaderboard leaderboard { get; set; }
    }

    public sealed class GGPartyGameCreateResponse : GGPartyGame
    {
        public CreatedPartyGameResponse createdGame { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Avatar
    {
        public string fullBodyPath { get; set; }
    }

    public class Competitive
    {
        public int rating { get; set; }
        public Division division { get; set; }
    }

    public class Division
    {
        public int id { get; set; }
        public int divisionId { get; set; }
        public int tierId { get; set; }
        public int type { get; set; }
        public int startRating { get; set; }
        public int endRating { get; set; }
    }

    public class GGGameOptions
    {
        public int initialHealth { get; set; }
        public int roundTime { get; set; }
        public int maxRoundTime { get; set; }
        public int maxNumberOfRounds { get; set; }
        public bool forbidMoving { get; set; }
        public bool forbidZooming { get; set; }
        public bool forbidRotating { get; set; }
        public string mapSlug { get; set; }
        public bool disableMultipliers { get; set; }
        public bool disableHealing { get; set; }
    }

    public class Host
    {
        public string playerId { get; set; }
        public string nick { get; set; }
        public bool isVerified { get; set; }
        public string avatarPath { get; set; }
        public int level { get; set; }
        public int titleTierId { get; set; }
        public string division { get; set; }
        public string performanceStreak { get; set; }
        public Rank rank { get; set; }
        public string team { get; set; }
        public Competitive competitive { get; set; }
        public Avatar avatar { get; set; }
    }

    public class Rank
    {
        public int? rank { get; set; }
        public Division? division { get; set; }
    }

    public class CreatedPartyGameResponse
    {
        public string gameLobbyId { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string gameType { get; set; }
        public string status { get; set; }
        public int numPlayersJoined { get; set; }
        public int totalSpots { get; set; }
        public int numOpenSpots { get; set; }
        public int minPlayersRequired { get; set; }
        public List<string> playerIds { get; set; }
        public List<GGPlayer> players { get; set; }
        public string visibility { get; set; }
        public object closingTime { get; set; }
        public DateTime timestamp { get; set; }
        public string owner { get; set; }
        public Host host { get; set; }
        public bool isAutoStarted { get; set; }
        public bool canBeStartedManually { get; set; }
        public bool isRated { get; set; }
        public string competitionId { get; set; }
        public string partyId { get; set; }
        public GGGameOptions gameOptions { get; set; }
        public DateTime createdAt { get; set; }
        public string shareLink { get; set; }
        public List<object> teams { get; set; }
        public object groupEventId { get; set; }
        public object quizId { get; set; }
        public bool hostParticipates { get; set; }
        public bool isSinglePlayer { get; set; }
        public object tripId { get; set; }
        public object blueprintId { get; set; }
        public object tournament { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public sealed class GGEntry
    {
        public GGUser user { get; set; }
        public int gold { get; set; }
        public int silver { get; set; }
        public int bronze { get; set; }
        public int position { get; set; }
        public int numberOfGamesPlayed { get; set; }
    }

    public sealed class GGParty
    {
        public string id { get; set; }
        public string shareLink { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public GGCreator creator { get; set; }
        public GGStyle style { get; set; }
        public List<GGPlayer> players { get; set; }
        public List<GGOpenGame> openGames { get; set; }
        public List<object> activeGames { get; set; }
        public List<object> bannedPlayers { get; set; }
        public bool requiresPassword { get; set; }
        public int playerCount { get; set; }
        public int numSpotsRemaining { get; set; }
        public int maxNumSpots { get; set; }
        public GGLeaderboard leaderboard { get; set; }
    }

    public sealed class GGPartyLobby
    {
        public GGParty party { get; set; }
        public bool hasJoined { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public sealed class GGPartyLobbyLeaderboard
    {
        public List<GGEntry> entries { get; set; }
        public int paginateFrom { get; set; }
        public object me { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGFriendships
    {
        public List<string> friendIds { get; set; }
        public List<string> receivedFriendRequests { get; set; }
        public List<string> sentFriendRequests { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGMaxGameScore
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public int percentage { get; set; }
    }

    public sealed class GGAverageGameScore
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public double percentage { get; set; }
    }

    public sealed class GGMaxRoundScore
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public int percentage { get; set; }
    }

    public sealed class GGClosestDistance
    {
        public GGMeters meters { get; set; }
        public GGMiles miles { get; set; }
    }

    public sealed class GGAverageDistance
    {
        public GGMeters meters { get; set; }
        public GGMiles miles { get; set; }
    }

    public sealed class GGValue
    {
        public int gamesPlayed { get; set; }
        public int wins { get; set; }
        public double averagePosition { get; set; }
        public int maxStreak { get; set; }
        public DateTime maxStreakDate { get; set; }
    }

    public sealed class GGBattleRoyaleStat
    {
        public string key { get; set; }
        public GGValue value { get; set; }
    }

    public sealed class GGDailyChallengesRolling7Days
    {
        public DateTime date { get; set; }
        public int totalScore { get; set; }
        public int bestRoundScore { get; set; }
        public int totalTime { get; set; }
        public int longestTime { get; set; }
        public int shortestTime { get; set; }
        public double closestDistance { get; set; }
        public double totalDistance { get; set; }
    }

    public sealed class GGStreakMedal
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public sealed class GGStreakRecord
    {
        public string key { get; set; }
        public GGValue value { get; set; }
    }

    public sealed class GGStats
    {
        public int gamesPlayed { get; set; }
        public int roundsPlayed { get; set; }
        public GGMaxGameScore maxGameScore { get; set; }
        public GGAverageGameScore averageGameScore { get; set; }
        public GGMaxRoundScore maxRoundScore { get; set; }
        public int streakGamesPlayed { get; set; }
        public GGClosestDistance closestDistance { get; set; }
        public GGAverageDistance averageDistance { get; set; }
        public string averageTime { get; set; }
        public int timedOutGuesses { get; set; }
        public List<GGBattleRoyaleStat> battleRoyaleStats { get; set; }
        public int dailyChallengeStreak { get; set; }
        public int dailyChallengeCurrentStreak { get; set; }
        public List<GGDailyChallengesRolling7Days> dailyChallengesRolling7Days { get; set; }
        public int dailyChallengeMedal { get; set; }
        public List<GGStreakMedal> streakMedals { get; set; }
        public List<GGStreakRecord> streakRecords { get; set; }
    }

    public sealed class GGOngoingGame
    {
        public string token { get; set; }
        public string map { get; set; }
        public string mapSlug { get; set; }
        public GGTotalScore score { get; set; }
        public DateTime dateTime { get; set; }
        public List<GGGuess> guesses { get; set; }
        public int rounds { get; set; }
        public int round { get; set; }
        public int type { get; set; }
        public int mode { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGSubscription
    {
        public string id { get; set; }
        public int type { get; set; }
        public int payProvider { get; set; }
        public DateTime created { get; set; }
        public string plan { get; set; }
        public string planId { get; set; }
        public double cost { get; set; }
        public string currency { get; set; }
        public string started { get; set; }
        public DateTime startedAt { get; set; }
        public string trialEnd { get; set; }
        public DateTime trialEndingAt { get; set; }
        public string periodEndsAt { get; set; }
        public DateTime periodEndingAt { get; set; }
        public bool canceled { get; set; }
        public int interval { get; set; }
        public int memberLimit { get; set; }
        public int product { get; set; }
        public bool isActive { get; set; }
        public bool isInTrialPeriod { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGDivisionV4
    {
        public int id { get; set; }
        public int divisionId { get; set; }
        public int tierId { get; set; }
        public string name { get; set; }
        public int minimumRank { get; set; }
    }

    public sealed class GGBattleRoyaleRank
    {
        public int? rank { get; set; }
        public int rating { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public GGDivisionV4 division { get; set; }
    }

    public sealed class GGBattleRoyaleDistance
    {
        public int numGamesPlayed { get; set; }
        public double avgPosition { get; set; }
        public int numWins { get; set; }
        public double winRatio { get; set; }
        public double avgGuessDistance { get; set; }
        public int numGuesses { get; set; }
        public int streak { get; set; }
    }

    public sealed class GGBattleRoyaleCountry
    {
        public int numGamesPlayed { get; set; }
        public double avgPosition { get; set; }
        public int numWins { get; set; }
        public double winRatio { get; set; }
        public int numGuesses { get; set; }
        public double avgCorrectGuesses { get; set; }
        public int streak { get; set; }
    }

    public sealed class GGBattleRoyaleMedals
    {
        public int medalCountGold { get; set; }
        public int medalCountSilver { get; set; }
        public int medalCountBronze { get; set; }
    }

    public sealed class GGCompetitiveCityStreaks
    {
        public int numGamesPlayed { get; set; }
        public int avgPosition { get; set; }
        public int numWins { get; set; }
        public int winRatio { get; set; }
        public int numGuesses { get; set; }
        public double avgCorrectGuesses { get; set; }
        public int streak { get; set; }
    }

    public sealed class GGCompetitiveStreaksRank
    {
        public object rank { get; set; }
        public int rating { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public GGDivisionV4 division { get; set; }
    }

    public sealed class GGCompetitiveStreaksMedals
    {
        public int medalCountGold { get; set; }
        public int medalCountSilver { get; set; }
        public int medalCountBronze { get; set; }
    }

    public sealed class GGDuels
    {
        public int numGamesPlayed { get; set; }
        public double avgPosition { get; set; }
        public int numWins { get; set; }
        public double winRatio { get; set; }
        public int avgGuessDistance { get; set; }
        public int numGuesses { get; set; }
        public int streak { get; set; }
    }

    public sealed class GGDuelsRank
    {
        public int? rank { get; set; }
        public int rating { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public GGDivisionV4 division { get; set; }
    }

    public sealed class GGDuelsMedals
    {
        public int medalCountGold { get; set; }
        public int medalCountSilver { get; set; }
        public int medalCountBronze { get; set; }
    }

    public sealed class GGCurrentLevel
    {
        public int level { get; set; }
        public int xpStart { get; set; }
    }

    public sealed class GGNextLevel
    {
        public int level { get; set; }
        public int xpStart { get; set; }
    }

    public sealed class GGCurrentTitle
    {
        public int id { get; set; }
        public int tierId { get; set; }
        public int minimumLevel { get; set; }
        public string name { get; set; }
    }

    public sealed class GGLifeTimeXpProgression
    {
        public int xp { get; set; }
        public GGCurrentLevel currentLevel { get; set; }
        public GGNextLevel nextLevel { get; set; }
        public GGCurrentTitle currentTitle { get; set; }
    }

    public sealed class GGTotalMedals
    {
        public int medalCountGold { get; set; }
        public int medalCountSilver { get; set; }
        public int medalCountBronze { get; set; }
    }

    public sealed class GGStatsV4
    {
        public GGBattleRoyaleRank battleRoyaleRank { get; set; }
        public GGBattleRoyaleDistance battleRoyaleDistance { get; set; }
        public GGBattleRoyaleCountry battleRoyaleCountry { get; set; }
        public GGBattleRoyaleMedals battleRoyaleMedals { get; set; }
        public GGCompetitiveCityStreaks competitiveCityStreaks { get; set; }
        public GGCompetitiveStreaksRank competitiveStreaksRank { get; set; }
        public GGCompetitiveStreaksMedals competitiveStreaksMedals { get; set; }
        public GGDuels duels { get; set; }
        public GGDuelsRank duelsRank { get; set; }
        public GGDuelsMedals duelsMedals { get; set; }
        public GGLifeTimeXpProgression lifeTimeXpProgression { get; set; }
        public GGTotalMedals totalMedals { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public sealed class GGMapAvatar
    {
        public string background { get; set; }
        public string decoration { get; set; }
        public string ground { get; set; }
        public string landscape { get; set; }
    }

    public sealed class GGSearchResult
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int type { get; set; }
        public object imageUrl { get; set; }
        public int likes { get; set; }
        public string creatorId { get; set; }
        public string creator { get; set; }
        public DateTime updated { get; set; }
        public bool isVerified { get; set; }
        public GGMapAvatar mapAvatar { get; set; }
    }

    public sealed class GGPostGame
    {
        public string map { get; set; }
        public string type { get; set; }
        public int timeLimit { get; set; }
        public bool forbidMoving { get; set; }
        public bool forbidZooming { get; set; }
        public bool forbidRotating { get; set; }
    }

    public sealed class GG_SignInPost
    {
        public string email { get; set; }
    }

    // 

    public class CreatedLobby
    {
        public string gameLobbyId { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string gameType { get; set; }
        public string status { get; set; }
        public int numPlayersJoined { get; set; }
        public int totalSpots { get; set; }
        public int numOpenSpots { get; set; }
        public int minPlayersRequired { get; set; }
        public List<string> playerIds { get; set; }
        public List<GGPlayer> players { get; set; }
        public string visibility { get; set; }
        public object closingTime { get; set; }
        public DateTime timestamp { get; set; }
        public string owner { get; set; }
        public Host host { get; set; }
        public bool isAutoStarted { get; set; }
        public bool canBeStartedManually { get; set; }
        public bool isRated { get; set; }
        public string competitionId { get; set; }
        public string partyId { get; set; }
        public GGGameOptions gameOptions { get; set; }
        public DateTime createdAt { get; set; }
        public string shareLink { get; set; }
        public List<object> teams { get; set; }
        public object groupEventId { get; set; }
        public object quizId { get; set; }
        public bool hostParticipates { get; set; }
        public bool isSinglePlayer { get; set; }
        public object tripId { get; set; }
        public object blueprintId { get; set; }
        public object tournament { get; set; }
    }

    public class Creator
    {
        public string id { get; set; }
        public string name { get; set; }
        public string avatarUrl { get; set; }
        public int titleTierId { get; set; }
        public bool isVerified { get; set; }
    }

    public class Leaderboard
    {
        public List<object> entries { get; set; }
        public int paginateFrom { get; set; }
        public object me { get; set; }
    }

    public class Party
    {
        public string id { get; set; }
        public string shareLink { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public Creator creator { get; set; }
        public GGStyle style { get; set; }
        public List<GGPlayer> players { get; set; }
        public List<object> openGames { get; set; }
        public List<object> activeGames { get; set; }
        public List<object> bannedPlayers { get; set; }
        public bool requiresPassword { get; set; }
        public int playerCount { get; set; }
        public int numSpotsRemaining { get; set; }
        public int maxNumSpots { get; set; }
        public Leaderboard leaderboard { get; set; }
    }

    // 

    public class CreatePartyLobbyResponse
    {
        public Party party { get; set; }
        public CreatedLobby createdLobby { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Br
    {
        public int level { get; set; }
        public int division { get; set; }
        public int streak { get; set; }
    }

    public class BrRank
    {
        public int rating { get; set; }
        public int? rank { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public Division division { get; set; }
    }

    public class CompetitionMedals
    {
        public int bronze { get; set; }
        public int silver { get; set; }
        public int gold { get; set; }
        public int platinum { get; set; }
    }
    public class CsRank
    {
        public int rating { get; set; }
        public int? rank { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public Division division { get; set; }
    }

    public class DuelsRank
    {
        public int rating { get; set; }
        public int? rank { get; set; }
        public int gamesLeftBeforeRanked { get; set; }
        public Division division { get; set; }
    }

    public class Onboarding
    {
        public object tutorialToken { get; set; }
        public string tutorialState { get; set; }
    }

    public class Pin
    {
        public string url { get; set; }
        public string anchor { get; set; }
        public bool isDefault { get; set; }
    }

    public class Progress
    {
        public int xp { get; set; }
        public int level { get; set; }
        public int levelXp { get; set; }
        public int nextLevel { get; set; }
        public int nextLevelXp { get; set; }
        public Title title { get; set; }
        public BrRank brRank { get; set; }
        public CsRank csRank { get; set; }
        public DuelsRank duelsRank { get; set; }
        public CompetitionMedals competitionMedals { get; set; }
        public Streaks streaks { get; set; }
    }

    public class GGSignInResponse
    {
        public string nick { get; set; }
        public DateTime created { get; set; }
        public bool isProUser { get; set; }
        public bool consumedTrial { get; set; }
        public bool isVerified { get; set; }
        public Pin pin { get; set; }
        public string fullBodyPin { get; set; }
        public int color { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string countryCode { get; set; }
        public Onboarding onboarding { get; set; }
        public Br br { get; set; }
        public object streakProgress { get; set; }
        public object explorerProgress { get; set; }
        public int dailyChallengeProgress { get; set; }
        public Progress progress { get; set; }
        public Competitive competitive { get; set; }
        public DateTime lastNameChange { get; set; }
        public bool isBanned { get; set; }
        public object nameChangeAvailableAt { get; set; }
        public Avatar avatar { get; set; }
    }

    public class Streaks
    {
        public int brCountries { get; set; }
        public int brDistance { get; set; }
        public int csCities { get; set; }
        public int duels { get; set; }
    }

    public class Title
    {
        public int id { get; set; }
        public int tierId { get; set; }
    }

    //////////
    public class PartyCreateGame
    {
        public string gameType { get; set; } = "Duels";
        public string client { get; set; } = "web";
    }

    public class PartyBan
    {
        public string userId { get; set; }
        public bool ban { get; set; }
    }

    public class PartyDiscardGame
    {
        public string gameLobbyId { get; set; }
    }

    public class GGSignInBody
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
#pragma warning restore CS1591