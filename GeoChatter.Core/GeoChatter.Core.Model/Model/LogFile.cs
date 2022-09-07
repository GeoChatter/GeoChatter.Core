using System;

namespace GeoChatter.Model
{
    /// <summary>
    /// Custom log data model for uploads
    /// </summary>
    public sealed class LogFile
    {
        /// <summary>
        /// DB id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Channel log was sent from
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// File name log's saved to
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Upload date
        /// </summary>
        public DateTime UploadDate { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public byte[] Data { get; set; }
    }
}
