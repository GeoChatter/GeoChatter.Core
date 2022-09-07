using GeoChatter.Model.Enums;
using System;
using GeoChatter.Model;

namespace GeoChatter.Helpers
{
    /// <summary>
    /// General purpose helper methods
    /// </summary>
    public static class GameHelper
    {
        /// <summary>
        /// Get <paramref name="distanceKM"/> as <paramref name="defaultunits"/>
        /// </summary>
        /// <param name="distanceKM"></param>
        /// <param name="defaultunits"></param>
        /// <returns></returns>
        public static double GetConvertedDistance(double distanceKM, Units defaultunits)
        {
            return defaultunits == Units.miles ? distanceKM * 0.621371 : distanceKM;
        }

        /// <summary>
        /// Calculate scale of area bounded by <paramref name="bounds"/>
        /// </summary>
        public static double CalculateScale(Bounds bounds)
        {
            return bounds != null ? HaversineDistance(bounds.Min, bounds.Max) / 7.458421 : 0D;
        }

        private static double Haversine(double lat1, double lng1, double lat2, double lng2)
        {
            double R = 6371.071;
            double rlat1 = lat1 * (Math.PI / 180);
            double rlat2 = lat2 * (Math.PI / 180);
            double difflat = rlat2 - rlat1;
            double difflon = (lng2 - lng1) * (Math.PI / 180);
            double km = 2 * R * Math.Asin(Math.Sqrt((Math.Sin(difflat / 2) * Math.Sin(difflat / 2)) + (Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Sin(difflon / 2) * Math.Sin(difflon / 2))));
            return km;
        }

        /// <summary>
        /// Haversine distance of given points <paramref name="mk1"/> and <paramref name="mk2"/>
        /// </summary>
        /// <param name="mk1"></param>
        /// <param name="mk2"></param>
        /// <returns></returns>
        public static double HaversineDistance(GGMin mk1, GGMax mk2)
        {
            return mk1 == null || mk2 == null ? double.MaxValue : Haversine(mk1.lat, mk1.lng, mk2.lat, mk2.lng);
        }

        /// <summary>
        /// Haversine distance of given points <paramref name="mk1"/> and <paramref name="mk2"/>
        /// </summary>
        /// <param name="mk1"></param>
        /// <param name="mk2"></param>
        /// <returns></returns>
        public static double HaversineDistance(Coordinates mk1, Coordinates mk2)
        {
            return mk1 == null || mk2 == null ? double.MaxValue : Haversine(mk1.Latitude, mk1.Longitude, mk2.Latitude, mk2.Longitude);
        }

        /// <summary>
        /// Calculate default game score from <paramref name="distance"/> and map <paramref name="scale"/>
        /// </summary>
        /// <param name="distance">Distance between guess and correct point in kilometers (km)</param>
        /// <param name="scale">Map scale calculated by <see cref="CalculateScale(Bounds)"/></param>
        /// <returns></returns>
        public static double CalculateDefaultScore(double distance, double scale)
        {
            const double perfectScore = 5000;
            const double maxMetersForPerfect = 25E-3;

            return Math.Round(distance, 3) > maxMetersForPerfect ? Math.Round(perfectScore * Math.Pow(0.99866017, distance * 1000 / scale)) : perfectScore;
        }
    }
}
