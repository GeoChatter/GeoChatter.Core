namespace GeoChatter.Model
{
    public class MapOptions
    {
        public bool ShowStreamOverlay { get; set; }
        public bool ShowFlags { get; set; }

        public bool ShowBorders { get; set; }

        public string MapIdentifier { get; set; }
        public string Streamer { get; set; }

        public string InstalledFlagPacks { get; set; }

        public string GameMode { get; set; }
        public bool IsUSStreak { get; set; }
        public bool TemporaryGuesses { get; set; }
        public string TwitchChannelName { get; set; }
        public override string ToString()
        {
            string val = string.Empty;

            val += "MapIdentifier: " + MapIdentifier + "\r";
            val += "Streamer: " + Streamer + "\r";
            val += "TwitchChannelName: " + TwitchChannelName + "\r";
            val += "InstalledFlagPacks: " + InstalledFlagPacks + "\r";
            val += "GameMode: " + GameMode + "\r";
            val += "IsUSStreak: " + IsUSStreak + "\r";
            val += "EnableTemporaryGuesses: " + TemporaryGuesses + "\r";
            val += "ShowBorders: " + ShowBorders + "\r";
            val += "ShowFlags: " + ShowFlags + "\r";
            val += "ShowStreamOverlay: " + ShowStreamOverlay;
            return val;
        }
    }
}
