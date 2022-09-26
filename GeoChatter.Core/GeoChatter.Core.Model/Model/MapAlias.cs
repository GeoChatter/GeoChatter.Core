using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Model.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MapAlias
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StreamerGGId { get; set; }
    }
}
