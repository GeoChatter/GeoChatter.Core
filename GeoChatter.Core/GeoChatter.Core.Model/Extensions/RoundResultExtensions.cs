using GeoChatter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Model.Extensions
{
    public static class RoundResultExtensions
    {

        public static Guess GetGuessOf(this RoundResult result)
        {
            return result.Round.Guesses.FirstOrDefault(g => g.Player.PlatformId == result.Player.PlatformId && g.Player.SourcePlatform == result.Player.SourcePlatform);
        }
    }
}
