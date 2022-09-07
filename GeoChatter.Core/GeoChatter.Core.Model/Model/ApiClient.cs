using System.Runtime.Serialization;
namespace GeoChatter.Model
{
    /// <summary>
    /// API client model
    /// </summary>
    public sealed class ApiClient
    {
        /// <summary>
        /// Channel name
        /// </summary>
        [DataMember(Name = "channelname")]
        public string ChannelName { get; set; }
        /// <summary>
        /// Channel id
        /// </summary>
        [DataMember(Name = "channelid")]
        public string ChannelId { get; set; }
        /// <summary>
        /// Bot name
        /// </summary>
        [DataMember(Name = "mapidentifier")]
        public string MapIdentifier { get; set; }
        /// <summary>
        /// Version string
        /// </summary>
        [DataMember(Name = "version")]
        public string Version { get; set; }
        /// <summary>
        /// Login session token
        /// </summary>
        [DataMember(Name = "loginToken")]
        public string LoginToken { get; set; }
        /// <summary>
        /// Overlay on map site
        /// </summary>
        [DataMember(Name = "enableOverlay")]
        public bool EnableOverlay { get; set; }


        /// <summary>
        /// Installed flag pack names
        /// </summary>
        [DataMember(Name = "installedFlagPacks")]
        public string InstalledFlagPacks { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ChannelId} ({MapIdentifier}) ({Version})";
        }

    }
}
