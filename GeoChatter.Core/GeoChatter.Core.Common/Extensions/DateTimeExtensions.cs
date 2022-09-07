using System;
using System.Globalization;

namespace GeoChatter.Extensions
{
    /// <summary>
    /// <see cref="DateTime"/> extension methods
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Date and time format <c>dd/MM/yyyy HH:mm:ss</c>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDialogFriendlyString(this DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Date format <c>yyyyMMdd</c>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToGeoChatterDate(this DateTime date)
        {
            return date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Date and time format <c>yyyyMMdd_HHmm</c>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToGeoChatterTimestamp(this DateTime date)
        {
            return date.ToString("yyyyMMdd_HHmm", CultureInfo.InvariantCulture);
        }
    }
}
