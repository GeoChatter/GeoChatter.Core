#if !DEBUG
using System.Runtime.CompilerServices;
#endif

using GeoChatter.Core.Common.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace GeoChatter.Helpers
{
    /// <summary>
    /// Utility methods
    /// </summary>
    public static class GCUtils
    {
        /// <summary>
        /// Open given URL in default browser or explorer
        /// </summary>
        /// <param name="url"></param>
        public static void OpenURL(string url)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
        }

        /// <summary>
        /// If <paramref name="obj"/> is <see langword="null"/>, throws <see cref="System.ArgumentNullException"/>, otherwise does nothing.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nameOfObj"></param>
        /// <param name="message"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static void ThrowIfNull([NotNull] object obj, string nameOfObj = null, string message = null)
        {
            if (obj == null)
            {
                nameOfObj ??= "<unknown>";
                throw new System.ArgumentNullException(nameOfObj, message ?? $"Expected non-null value for '{nameOfObj}'.");
            }
        }

        /// <summary>
        /// If <see cref="string.IsNullOrWhiteSpace(string)"/> is <see langword="true"/>, throws <see cref="System.ArgumentNullException"/>, otherwise does nothing.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nameOfObj"></param>
        /// <param name="message"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static void ThrowIfNullEmptyOrWhiteSpace([NotNull] string obj, string nameOfObj = null, string message = null)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                nameOfObj ??= "<unknown>";
                throw new System.ArgumentNullException(nameOfObj, message ?? $"Expected a non-null, non-empty or non-whitespace string for '{nameOfObj}'.");
            }
        }

        /// <summary>
        /// Assert <paramref name="val"/> to be in range <c>[-180, 180]</c>
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double ModulLongitude(double val)
        {
            val %= 360;
            if (val < -180)
            {
                val += 360;
            }
            else if (val > 180)
            {
                val -= 360;
            }
            return val;
        }

        /// <summary>
        /// Assert <paramref name="val"/> to be in range <c>[-85, 85]</c>
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double ModulLatitude(double val)
        {
            if (val < -85)
            {
                val = -85;
            }
            else if (val > 85)
            {
                val = 85;
            }
            return val;
        }

        /// <summary>
        /// If <paramref name="latStr"/> and <paramref name="lngStr"/> are valid coordinates, returns <see langword="true"/>, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="latStr">Latitude from string</param>
        /// <param name="lngStr">Longitude from string</param>
        /// <param name="lat">Latitude parsed and modulated from <paramref name="latStr"/></param>
        /// <param name="lng">Longitude parsed and modulated from <paramref name="lngStr"/></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool ValidateAndFixCoordinates(string latStr, string lngStr, out double lat, out double lng)
        {
            lat = 0D;
            lng = 0D;

            bool validCoordinates = latStr?.TryParseDoubleDefault(out lat) is true
                && lngStr?.TryParseDoubleDefault(out lng) is true
                && double.IsFinite(lat)
                && double.IsFinite(lng);

            if (validCoordinates)
            {
                lat = ModulLatitude(lat);
                lng = ModulLongitude(lng);
            }
            return validCoordinates;
        }

    }
}
