using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CC.Core.HttpClientRequests
{
    public class HttpRequestFactory
    {
        private void SetHeaderInfo(HttpRequestBuilder builder, Dictionary<string, string> customHeaders = null)
        {
            if (customHeaders != null)
            {
                builder.AddCustomHeaders(customHeaders);
            }
        }

        private void SetUserAuthInfo(HttpRequestBuilder builder, UserAuthType userAuthType, string userAuthInfo)
        {
            if (userAuthType == UserAuthType.BearerToken)
            {
                builder.AddBearerToken(userAuthInfo);
            }
            else if (userAuthType == UserAuthType.BasicAuth)
            {
                builder.AddBasicAuthUser(userAuthInfo);
            }
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
            => await Get(requestUri, UserAuthType.None, "");

        public async Task<HttpResponseMessage> Get(string requestUri, UserAuthType userAuthType, string userAuthInfo, Dictionary<string, string> customHeaders = null)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri);

            SetUserAuthInfo(builder, userAuthType, userAuthInfo);

            SetHeaderInfo(builder, customHeaders);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Post(string requestUri, object value)
            => await Post(requestUri, value, UserAuthType.None, "");

        public async Task<HttpResponseMessage> Post(
            string requestUri, object value, UserAuthType userAuthType, string userAuthInfo)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));

            SetUserAuthInfo(builder, userAuthType, userAuthInfo);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Put(string requestUri, object value)
            => await Put(requestUri, value, UserAuthType.None, "");

        public async Task<HttpResponseMessage> Put(
            string requestUri, object value, UserAuthType userAuthType, string userAuthInfo)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));

            SetUserAuthInfo(builder, userAuthType, userAuthInfo);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Patch(string requestUri, object value)
            => await Patch(requestUri, value, UserAuthType.None, "");

        public async Task<HttpResponseMessage> Patch(
            string requestUri, object value, UserAuthType userAuthType, string userAuthInfo)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value));

            SetUserAuthInfo(builder, userAuthType, userAuthInfo);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Delete(string requestUri)
            => await Delete(requestUri, UserAuthType.None, "");

        public async Task<HttpResponseMessage> Delete(
            string requestUri, UserAuthType userAuthType, string userAuthInfo)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri);

            SetUserAuthInfo(builder, userAuthType, userAuthInfo);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName)
            => await PostFile(requestUri, filePath, apiParamName, UserAuthType.None, "");

        public async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName, UserAuthType userAuthType, string userAuthInfo)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new FileContent(filePath, apiParamName));

            SetUserAuthInfo(builder, userAuthType, userAuthInfo);

            return await builder.SendAsync();
        }
    }

}
