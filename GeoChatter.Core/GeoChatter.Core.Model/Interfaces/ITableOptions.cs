using GeoChatter.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GeoChatter.Model.Interfaces
{
    /// <summary>
    /// Guess model
    /// </summary>
    public interface ITableOptions
    {
        /// <summary>
        /// List of options
        /// </summary>
        public List<GameOptions> Options { get; set; }

        /// <summary>
        /// Save this instance to table options file
        /// </summary>
        public void Save();
        /// <summary>
        /// Get options from TableOptions.json file
        /// </summary>
        /// <returns></returns>
        public ITableOptions Load();
        /// <summary>
        /// Get sorting filters using <see cref="Options"/> for given mode and stage
        /// <para>These filters can be used with <see cref="CoreExtensions.BuildOrderBy{T}(IEnumerable{T}, Tuple{string, ListSortDirection}[])"/></para>
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public Tuple<string, ListSortDirection>[] GetFiltersFor(GameMode mode, GameStage stage = GameStage.ENDROUND);
        /// <summary>
        /// Get default sorting filters using <see cref="Options"/> for given mode and stage
        /// <para>These filters can be used with <see cref="CoreExtensions.BuildOrderBy{T}(IEnumerable{T}, Tuple{string, ListSortDirection}[])"/></para>
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public Tuple<string, ListSortDirection>[] GetDefaultFiltersFor(GameMode mode, GameStage stage = GameStage.ENDROUND);
    }
}
