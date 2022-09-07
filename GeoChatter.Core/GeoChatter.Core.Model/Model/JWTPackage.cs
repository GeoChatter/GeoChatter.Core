#pragma warning disable CS1591

using System;
using System.Collections.Generic;

namespace GeoChatter.Model
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Needs to be same as sent object's")]
    public sealed class JwtPackgage
    {
        public string bot { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string tkn { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string display { get; set; }
        public string pic { get; set; }

        public string hlx { get; set; }
        public string flag { get; set; }
        public string color { get; set; }
        public bool isRandom { get; set; }

        public string sourcePlatform { get; set; }
        public bool isTemporary { get; set; }

        public string src { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Needs to be same as sent object's")]
    public sealed class PubsubPerms
    {
        public List<string> listen { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Needs to be same as sent object's")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Needs to be same as sent object's")]
    public sealed class DecodedJwtPackage
    {
        public int exp { get; set; }

        public string opaque_user_id { get; set; }
        public string user_id { get; set; }
        public string channel_id { get; set; }
        public string role { get; set; }
        public bool is_unlinked { get; set; }
        public PubsubPerms pubsub_perms { get; set; }
    }

    public sealed class JsonGuess
    {
        public Guid ID { get; set; }
        public string Channel { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicUrl { get; set; }
    }

}

#pragma warning restore CS1591
