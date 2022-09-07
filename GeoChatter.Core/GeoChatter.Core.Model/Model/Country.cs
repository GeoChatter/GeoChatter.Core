using System;
using System.Collections.Generic;

namespace GeoChatter.Model
{
    /// <summary>
    /// Name mappings model
    /// </summary>
    public sealed class NameMapping
    {
        /// <summary>
        /// 2 letter or GeoChatter code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string Display { get; set; }

    }

    /// <summary>
    /// Location country model
    /// </summary>
    public sealed class Country
    {
        /// <summary>
        /// Unknown country name
        /// </summary>
        public const string UnknownCountryName = "Unknown";
        /// <summary>
        /// Unknown country code
        /// </summary>
        public const string UnknownCountryCode = "UNKNOWN";

        /// <summary>
        /// Creates a new instance of unknown country
        /// </summary>
        public static Country Unknown => new Country(UnknownCountryCode, UnknownCountryName);

        private string name;

        /// <summary>
        /// 
        /// </summary>
        public Country()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public Country(string code, string name)
        {
            Code = code;
            Name = name;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Country 2 digit ISO or custom code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Country name to be displayed
        /// </summary>
        public string Name
        {
            get => name;
            set => name = GetMappedCountryName(value);
        }

        /// <summary>
        /// Mapping of country names to be consistant
        /// </summary>
        public static Dictionary<string, NameMapping> MappedNames { get; set; } = new Dictionary<string, NameMapping>();

        /// <summary>
        /// Get mapping of <paramref name="original"/> if it exists in <see cref="MappedNames"/>
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string GetMappedCountryName(string original)
        {
            original = original?.Replace("&", "and", StringComparison.InvariantCulture) ?? UnknownCountryName;
            return MappedNames.ContainsKey(original) ? MappedNames[original].Display : original;
        }

        /// <summary>
        /// Compare <see cref="Name"/> and <see cref="Code"/> with another instance
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSame(Country other)
        {
            return other != null && other.Code == Code && other.Name == Name;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
