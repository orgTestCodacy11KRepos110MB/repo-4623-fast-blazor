﻿using Microsoft.Fast.Components.FluentUI.Utilities;
using Microsoft.JSInterop;

namespace Microsoft.Fast.Components.FluentUI.Infrastructure
{
    public class CacheStorageAccessor : JSModule
    {
        public CacheStorageAccessor(IJSRuntime js)
            : base(js, "./_content/Microsoft.Fast.Components.FluentUI/js/CacheStorageAccessor.js")
        {
        }

        public async ValueTask PutAsync(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            string requestMethod = requestMessage.Method.Method;
            string requestBody = await GetRequestBodyAsync(requestMessage);
            string responseBody = await responseMessage.Content.ReadAsStringAsync();

            await InvokeVoidAsync("put", requestMessage.RequestUri!, requestMethod, requestBody, responseBody);
        }

        public async ValueTask<string> PutAndGetAsync(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            string requestMethod = requestMessage.Method.Method;
            string requestBody = await GetRequestBodyAsync(requestMessage);
            string responseBody = await responseMessage.Content.ReadAsStringAsync();

            await InvokeVoidAsync("put", requestMessage.RequestUri!, requestMethod, requestBody, responseBody);

            return responseBody;
        }

        public async ValueTask<string> GetAsync(HttpRequestMessage requestMessage)
        {
            string requestMethod = requestMessage.Method.Method;
            string requestBody = await GetRequestBodyAsync(requestMessage);
            string result = await InvokeAsync<string>("get", requestMessage.RequestUri!, requestMethod, requestBody);

            return result;
        }

        public async ValueTask RemoveAsync(HttpRequestMessage requestMessage)
        {
            string requestMethod = requestMessage.Method.Method;
            string requestBody = await GetRequestBodyAsync(requestMessage);

            await InvokeVoidAsync("remove", requestMessage.RequestUri!, requestMethod, requestBody);
        }

        private static async ValueTask<string> GetRequestBodyAsync(HttpRequestMessage requestMessage)
        {
            string requestBody = string.Empty;
            if (requestMessage.Content is not null)
            {
                requestBody = await requestMessage.Content.ReadAsStringAsync();
            }

            return requestBody;
        }
    }
}
