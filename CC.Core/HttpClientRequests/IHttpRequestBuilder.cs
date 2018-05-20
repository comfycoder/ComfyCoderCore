using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CC.Core.HttpClientRequests
{
    public interface IHttpRequestBuilder
    {
        HttpRequestBuilder AddAcceptHeader(string acceptHeader);
        HttpRequestBuilder AddAllowAutoRedirect(bool allowAutoRedirect);
        HttpRequestBuilder AddBasicAuthUser(string basicAuthUser);
        HttpRequestBuilder AddBearerToken(string bearerToken);
        HttpRequestBuilder AddContent(HttpContent content);
        HttpRequestBuilder AddCustomHeaders(Dictionary<string, string> customHeaders);
        HttpRequestBuilder AddMethod(HttpMethod method);
        HttpRequestBuilder AddRequestUri(string requestUri);
        HttpRequestBuilder AddTimeout(TimeSpan timeout);
        Task<HttpResponseMessage> SendAsync();
    }
}