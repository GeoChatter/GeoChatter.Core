using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace GeoChatter.Model
{
    /// <summary>
    /// Chat message model
    /// </summary>
    public class ChatMessage
    {
        private string msg;
        /// <summary>
        /// Sender id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Sender name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public string Message
        {
            get => msg;
            set
            {
                if (!string.IsNullOrEmpty(msg) && msg != value)
                {
                    Modified = true;
                }

                msg = value;
            }
        }
        /// <summary>
        /// Wheter there was any modifications post
        /// </summary>
        [NotMapped, JsonIgnore]
        public bool Modified { get; set; }
    }
}
