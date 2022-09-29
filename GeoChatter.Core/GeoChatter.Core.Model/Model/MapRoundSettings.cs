using System;
using System.Collections;
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
        public List<string> Layers { get; set; }
        public bool Is3dEnabled { get; set; }
        public bool BlackAndWhite { get; set; }
        public bool Blurry { get; set; }
        public bool Mirrored { get; set; }
        public bool UpsideDown { get; set; }
        public bool Sepia { get; set; }
	    public int MaxZoomLevel { get; set; }
        
    }
}
