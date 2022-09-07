using GeoChatter.Core.Model.Map;
using GeoChatter.Model;
using GeoChatter.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessServerApiInterfaces
{
    public interface IGeoChatterClient
    {
         Task ReceiveGuess(Guess guess);
         Task ReceiveFlag(Player player, string flag);
         Task ReceiveColor(Player player, string color);
         Task SetMapFeatures(MapOptions options);

        Task StartGame(MapGameSettings gameSettings);
        Task StartRound(MapRoundSettings roundSettings);

        Task EndRound(List<MapResult> results);
        Task EndGame(List<MapResult> results);
        Task ExitGame();

        Task InitiateDisconnect();

    }
}
