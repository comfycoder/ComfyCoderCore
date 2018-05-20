using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Core.HttpClientRequests
{
    /// <summary>
    /// Used to retrieve header variables and user claims.
    /// </summary>
    public class RequestMetadata : IRequestMetadata
    {
        readonly IHostingEnvironment _env;
        readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Is the user authorized.
        /// </summary>
        public bool IsAuthorized
        {
            get
            {
                return GetClaimBooleanValue(ClaimType.IsAuthorized);
            }
        }

        public string UserIdentifier
        {
            get
            {
                return GetClaimStringValue(ClaimType.UserIdentifier);
            }
        }

        public Guid ProfileIdentifier
        {
            get
            {
                var userId = GetClaimStringValue(ClaimType.ProfileIdentifier);
                Guid profileId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    Guid.TryParse(userId, out profileId);
                }
                return profileId;
            }
        }

        /// <summary>
        /// Correlation message identifier.
        /// </summary>
        public string MessageId
        {
            get
            {
                return GetHeaderValue("MessageID");
            }
        }

        /// <summary>
        /// Correlation transaction identifier.
        /// </summary>
        public string TransactionId
        {
            get
            {
                return GetHeaderValue("TransactionID");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="env">
        /// A HostingEnvironment object instance.
        /// </param>
        /// <param name="httpContextAccessor">
        /// An HttpContextAccessor object instance.
        /// </param>
        public RequestMetadata(IHostingEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        /// <summary>
        /// Returns request values as a dictionary.
        /// </summary>
        /// <returns>
        /// A Dictionary object instance.
        /// </returns>
        public Dictionary<string, string> GetMetadataDictionary()
        {
            var properties = new Dictionary<string, string>();

            properties.Add("Correlation_MessageId", this.MessageId);
            properties.Add("Correlation_TransactionId", this.TransactionId);
            properties.Add("Identity_IsAuthorized", this.IsAuthorized.ToString());
            properties.Add("Identity_UserIdentifier", this.UserIdentifier);

            return properties;
        }

        /// <summary>
        /// Internal routine providing safe reading of header values.
        /// </summary>
        /// <param name="keyName">
        /// Name of an HTTP request header variable.
        /// </param>
        /// <returns>
        /// An an HTTP request header variable value.
        /// </returns>
        public string GetHeaderValue(string keyName)
        {
            string value = string.Empty;

            if (_httpContextAccessor?.HttpContext != null)
            {
                if (String.IsNullOrWhiteSpace(value) && !String.IsNullOrWhiteSpace(keyName))
                {
                    IHeaderDictionary headersDictionary = _httpContextAccessor.HttpContext?.Request?.Headers;

                    if (headersDictionary != null && headersDictionary.Keys.Contains(keyName))
                    {
                        var headers = headersDictionary[keyName];

                        if (headers.Any())
                        {
                            value = headers[0];
                        }
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Get a claim string value by the input claim type name.
        /// </summary>
        /// <param name="claimType">
        /// Name of the claim type.
        /// </param>
        /// <returns>
        /// Claim value as a string.
        /// </returns>
        public string GetClaimStringValue(string claimType)
        {
            string result = string.Empty;

            var user = _httpContextAccessor?.HttpContext?.User;

            if (user != null)
            {
                var claim = user.Claims.Where(c => c.Type == claimType).FirstOrDefault();

                if (claim != null)
                {
                    if (!string.IsNullOrWhiteSpace(claim.Value))
                    {
                        result = claim.Value;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get a claim integer value by the input claim type name.
        /// </summary>
        /// <param name="claimType">
        /// Name of the claim type.
        /// </param>
        /// <returns>
        /// Claim value as an integer.
        /// </returns>
        public int GetClaimIntValue(string claimType)
        {
            int result = 0;

            var user = _httpContextAccessor?.HttpContext?.User;

            if (user != null)
            {
                var claim = user.Claims.Where(c => c.Type == claimType).FirstOrDefault();

                if (claim != null)
                {
                    if (!string.IsNullOrWhiteSpace(claim.Value))
                    {
                        int.TryParse(claim.Value, out result);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get a claim boolean value by the input claim type name.
        /// </summary>
        /// <param name="claimType">
        /// Name of the claim type.
        /// </param>
        /// <returns>
        /// Claim value as a boolean.
        /// </returns>
        public bool GetClaimBooleanValue(string claimType)
        {
            bool result = false;

            var user = _httpContextAccessor?.HttpContext?.User;

            if (user != null)
            {
                var claim = user.Claims.Where(c => c.Type == claimType).FirstOrDefault();

                if (claim != null)
                {
                    if (!string.IsNullOrWhiteSpace(claim.Value))
                    {
                        var value = claim.Value.ToLower();

                        switch (value)
                        {
                            case "true":
                            case "yes":
                            case "on":
                            case "1":
                                result = true;
                                break;

                            case "false":
                            case "no":
                            case "off":
                            case "0":
                                result = false;
                                break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
