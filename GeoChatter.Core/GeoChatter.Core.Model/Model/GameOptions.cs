using GeoChatter.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace GeoChatter.Model
{

    /// <summary>
    /// Game options for display and sorting
    /// </summary>
    public class GameOptions
    {
        /// <summary>
        /// Game mode for column
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName("Game mode")]
        public string Mode { get; set; }
        /// <summary>
        /// Game stages for column
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Game Stages")]
        public List<StageOptions> Stages { get; set; } = new List<StageOptions>();
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Mode} ({string.Join(",", Stages)})";
        }
    }

    /// <summary>
    /// Stage options for <see cref="GameOptions"/>
    /// </summary>
    public class StageOptions
    {
        /// <summary>
        /// Stage name from <see cref="GameStage"/>
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName("Stage")]
        public string Stage { get; set; }
        /// <summary>
        /// List of columns for the stage
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Columns")]
        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();

        /// <summary>
        /// Scoreboard position: pixels from top
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Top")]
        public double Top { get; set; }

        /// <summary>
        /// Scoreboard position: pixels from left
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Left")]
        public double Left { get; set; }

        /// <summary>
        /// Scoreboard size: width in pixels
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Width")]
        public double Width { get; set; }

        /// <summary>
        /// Scoreboard size: height in pixels
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Height")]
        public double Height { get; set; }

        /// <summary>
        /// Wheter scoreboard is minimized
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Minimized")]
        public bool IsMinimized { get; set; }

        /// <summary>
        /// Wheter scoreboard has row numbers
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("RowNumbers")]
        public bool ShowRowNumbers { get; set; }

        /// <summary>
        /// Minimap layer preference
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("MinimapLayer")]
        public string MinimapLayer { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Stage;
        }
    }
    /// <summary>
    /// Table column model for scoreboard
    /// </summary>
    public class TableColumn
    {
        /// <summary>
        /// Column index
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Position")]
        public int Position { get; set; }
        /// <summary>
        /// Column data field name
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Internal value name")]
        public string DataField { get; set; }
        /// <summary>
        /// Column display name
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Display name")]
        public string Name { get; set; }
        /// <summary>
        /// Column width in px
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Width in pixels")]
        public double Width { get; set; }
        /// <summary>
        /// Sort index
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Sorting index (-1 to ignore)")]
        public int SortIndex { get; set; } = -1;
        /// <summary>
        /// Sort index
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Sorting order (asc, desc or leave empty)")]
        public string SortOrder { get; set; } = string.Empty;
        /// <summary>
        /// Default sort index
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Default Sorting index")]
        public int DefaultSortIndex { get; set; } = -1;
        /// <summary>
        /// Default sort index
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Default Sorting order")]
        public string DefaultSortOrder { get; set; } = string.Empty;
        /// <summary>
        /// Wheter column should start visible
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Visible")]
        public bool Visible { get; set; }
        /// <summary>
        /// Sortable
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Sortable")]
        public bool Sortable { get; set; }
        /// <summary>
        /// Wheter to allow the column in multi guess games
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [DisplayName("Visible in MultiGuess game")]
        public bool AllowedWithMultiGuess { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }

    }

}
