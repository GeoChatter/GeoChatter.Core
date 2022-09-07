using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChatter.Model
{
 /// <summary>
 /// Response object for Api login
 /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// The generated session token
        /// </summary>
        public string SessionToken { get; set; }
        /// <summary>
        /// The generated and unique map identifiert
        /// </summary>
        public string MapIdentifier { get; set; }
        
        public LoginResult Result { get; set; }
    }

    public enum LoginResult
    {
        Success,
        Failure,
        OtherClient
    }
}
