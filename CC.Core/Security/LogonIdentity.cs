using System;

namespace CC.Core.Security
{
    /// <summary>
    /// Authenticated user identity module for the CU Member.
    /// </summary>
    public class LogonIdentity
    {
        /// <summary>
        /// Indicates whether the Credit Union Member user has been granted permission 
        /// to obtain their financial account information.
        /// </summary>
        public bool IsAuthorized { get; private set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User profile identifier.
        /// </summary>
        public Guid ProfileId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LogonIdentity()
        {
            IsAuthorized = false;
            Username = null;
            ProfileId = Guid.Empty;
        }

        public LogonIdentity(bool isAuthorized, string username, Guid profileId)
        {
            IsAuthorized = isAuthorized;
            Username = username;
            ProfileId = profileId;
        }
    }
}
