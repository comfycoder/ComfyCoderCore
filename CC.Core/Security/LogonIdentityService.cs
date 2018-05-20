using CC.Core.HttpClientRequests;

namespace CC.Core.Security
{
    /// <summary>
    /// Logon Identity Service
    /// </summary>
    public class LogonIdentityService : ILogonIdentityService
    {
        readonly IRequestMetadata _requestMetadata;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="requestMetadata">
        /// Claim Reader
        /// </param>
        public LogonIdentityService(
            IRequestMetadata requestMetadata
        )
        {
            _requestMetadata = requestMetadata;
        }

        /// <summary>
        /// Gets and identity object for the CU Member and indicates whether they have been authorized.
        /// </summary>
        /// <returns></returns>
        public LogonIdentity GetLogonIdentity()
        {
            LogonIdentity logonIdentity = null;

            var isAuthorized = _requestMetadata.IsAuthorized;

            var username = _requestMetadata.UserIdentifier;

            var profileId = _requestMetadata.ProfileIdentifier;

            logonIdentity = new LogonIdentity(isAuthorized, username, profileId);

            return logonIdentity;
        }
    }
}
