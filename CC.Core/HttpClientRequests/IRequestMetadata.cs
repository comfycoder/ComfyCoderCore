using System;
using System.Collections.Generic;

namespace CC.Core.HttpClientRequests
{
    public interface IRequestMetadata
    {
        bool IsAuthorized { get; }
        string MessageId { get; }
        string TransactionId { get; }
        string UserIdentifier { get; }
        Guid ProfileIdentifier { get; }

        bool GetClaimBooleanValue(string claimType);
        int GetClaimIntValue(string claimType);
        string GetClaimStringValue(string claimType);
        string GetHeaderValue(string keyName);
        Dictionary<string, string> GetMetadataDictionary();
    }
}