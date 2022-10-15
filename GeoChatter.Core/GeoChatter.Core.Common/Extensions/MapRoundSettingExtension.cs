using GeoChatter.Core.Model.Map;
using GeoChatter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Common.Extensions
{
    public static class MapRoundSettingExtension
    {
        /// <summary>
        /// Get round as json
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public static string ToJson([NotNull] this MapRoundSettings settings)
        {
            string s = $@"
            {{
                ""BlackAndWhite"": ""{settings.BlackAndWhite.ToStringDefault()}"",
                ""Blurry"": {settings.Blurry.ToStringDefault()},
                ""Is3dEnabled"": {settings.Is3dEnabled.ToStringDefault()},
                ""IsMultiGuess"": {settings.IsMultiGuess.ToStringDefault()},
                ""MaxZoomLevel"": {settings.MaxZoomLevel.ToStringDefault()},
                ""Mirrored"": {settings.Mirrored.ToStringDefault()}, 
                ""RoundNumber"": {settings.RoundNumber.ToStringDefault()}, 
                ""Sepia"": {settings.Sepia.ToStringDefault()},
                ""StartTime"": {settings.StartTime.ToStringDefault()},
                ""UpsideDown"": {settings.UpsideDown.ToStringDefault()},
                ""Layers"": [{string.Join(",", settings.Layers
                                                .Select(g => "\""+g.EscapeJSON()+"\""))}]
            }}";
            return s;
        }

    }
}
