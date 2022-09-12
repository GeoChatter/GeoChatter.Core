using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Model.Map
{
    public class MapResult
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string ProfilePicUrl { get; set; }
        public string PlayerFlagName { get; set; }
        public string PlayerFlag { get; set; }

//Only round
        public bool WasRandom { get; set; }
        public double Score { get; set; }
        public double Distance { get; set; }
        public double TimeTaken { get; set; }
        public int Streak { get; set; }
//Only round
        public string CountryCode { get; set; }
//Only round
        public string ExactCountryCode { get; set; }
        public int GuessCount { get; set; }
        public bool IsStreamerResult { get; set; }

//Only round
        public bool GuessedBefore { get; set; }

        public string GameId { get; set; }
        
    }
}
