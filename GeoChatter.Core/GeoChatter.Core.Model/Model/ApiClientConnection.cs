using System;
using System.ComponentModel.DataAnnotations;

namespace GeoChatter.Model
{
    /// <summary>
    /// TODO: Why not use <see cref="ApiClient"/>
    /// </summary>
    public class ApiClientConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiClientConnection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string MapID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChannelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableOverlay { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ChannelId} ({MapID}) ({ClientVersion}) ({ConnectionId})";
        }
    }

}
