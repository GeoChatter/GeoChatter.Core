using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Model.Map
{
    public class MapRoundSettings
    {
        public int RoundNumber { get; set; }
        public bool IsMultiGuess { get; set; }
        public DateTime StartTime { get; set; }
        //public string PanoId { get; set; }
        //public double Heading { get; set; }
        //public double Pitch { get; set; }
        //public double Zoom { get; set; }
        //public double FOV { get; set; }
    }
}
