using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Core.Model
{
    public class ClientVerificationResult
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string RequestIp { get; set; }
        public ClientVerificationState ClientVerificationState { get; set; }

        public DateTime RequestTimeStamp { get; set; }

        public string DuelId { get; set; }
    }

    public enum ClientVerificationState
    {
       Waiting,
       Verified,
       Missing,
       VerificationFailed
    }
}
