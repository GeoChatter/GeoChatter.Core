using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Model.Map
{
    public class MapGameSettings
    {
        public string MapID { get; set; }
        public string MapName { get; set; }
        public bool IsInfinite { get; set; }
        public bool IsStreak { get; set; }

        public string GameType { get; set; }
        public string GameMode { get; set; }
        public string GameState { get; set; }
        public int RoundCount { get; set; }
        public int TimeLimit { get; set; }
        public bool ForbidMoving { get; set; }
        public bool ForbidZooming { get; set; }
        public bool ForbidRotating { get; set; }
        public string StreakType { get; set; }
        
    }
}
