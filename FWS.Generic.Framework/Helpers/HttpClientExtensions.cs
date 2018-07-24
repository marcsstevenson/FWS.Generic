using Newtonsoft.Json;
using System.Net.Http;

namespace FWS.Generic.Framework.Helpers
{
    public static class HttpClientExtensions
    {
        public static HttpResponseMessage Post<TPayload>(this HttpClient client, string url, TPayload data)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(data));
            return AsyncHelper.RunSync(() => client.PostAsync(url, payload));
        }
    }
}
