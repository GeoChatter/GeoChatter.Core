using GeoChatter.Core.Model.Map;
using GeoChatter.Model;
using GeoChatter.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessServerApiInterfaces
{
    public interface IGeoChatterHub
    {
        void ClientSuccess(bool success);
        Task<LoginResponse> Login(ApiClient client);
       void Logoff(ApiClient client);
       void KeepAlive(ApiClient client);
        Task<MapOptions> MapLogin(string botname);
        //Task StartGame(ApiGame game);
        //Task SartRound(ApiRound round);
        //Task EndRound(Guid id);
        //Task EndGame(Guid id);
        //Task ShowSummary(Guid gameId);
        Task SaveGame(Game game);

        Task SaveGuess(Guess guess);
        Task<Player> SyncPlayer(Player player);
        Task<List<Player>> SyncPlayers(List<Player> players);
        Task<List<Player>> SyncPlayersById(List<int> playerIds);
        Task<List<Player>> SyncPlayersByNames(List<string> playerNames);
        Task<Player> SyncPlayerById(int playerId);
        Task<Player> SyncPlayerByName(string player);
        void SavePlayers(List<Player> players);
        void SavePlayer(Player player);
        void ReportGuessState(int id, GuessState guessState);
        Task SendMessage(string user, string message);

        Task<IEnumerable<string>> GetCgTrollIds();
        Task<Dictionary<string, string>> GetApiKeys();
        Task<bool> IsSummaryEnabled();


        Task<bool> UploadLog(LogFile logFile);

        Task UpdateMapOptions(MapOptions options);


        Task<List<LogFile>> GetLogFiles();
        Task<LogFile> GetLogFile(int id);


        void StartGame(MapGameSettings gameSettings);
        void StartRound(MapRoundSettings roundSettings);

        void EndRound(List<MapResult> results);
        void EndGame(List<MapResult> results);
        void ExitGame();

        Task<string> GetServerLog();

        void DisconnectClients(string userId);

    }
}
