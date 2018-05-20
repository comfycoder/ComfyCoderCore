using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CC.Core.HttpClientRequests
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private HttpMethod _method = null;
        private string _requestUri = "";
        private HttpContent _content = null;
        private string _bearerToken = "";
        private string _basicAuthUser = "";
        private string _acceptHeader = "application/json";
        private TimeSpan _timeout = new TimeSpan(0, 0, 15);
        private bool _allowAutoRedirect = false;
        private Dictionary<string, string> _customHeaders = null;


        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }

        public HttpRequestBuilder AddCustomHeaders(Dictionary<string, string> customHeaders)
        {
            _customHeaders = customHeaders;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            _requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            _content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            _bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddBasicAuthUser(string basicAuthUser)
        {
            _basicAuthUser = basicAuthUser;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            _acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            _timeout = timeout;
            return this;
        }

        public HttpRequestBuilder AddAllowAutoRedirect(bool allowAutoRedirect)
        {
            _allowAutoRedirect = allowAutoRedirect;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = _method,
                RequestUri = new Uri(_requestUri)
            };

            if (_content != null)
            {
                request.Content = _content;
            }

            if (!string.IsNullOrEmpty(_bearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            }

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(_acceptHeader))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            }

            if (_customHeaders != null)
            {
                foreach (var header in _customHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Setup an Http client handler
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = _allowAutoRedirect,
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            var client = new System.Net.Http.HttpClient(handler);

            client.Timeout = _timeout;

            if (!string.IsNullOrEmpty(_basicAuthUser))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _basicAuthUser);
            }

            return await client.SendAsync(request);
        }

        private void EnsureArguments()
        {
            if (_method == null)
            {
                throw new ArgumentNullException("Method");
            }

            if (string.IsNullOrEmpty(_requestUri))
            {
                throw new ArgumentNullException("Request Uri");
            }
        }
    }

}
