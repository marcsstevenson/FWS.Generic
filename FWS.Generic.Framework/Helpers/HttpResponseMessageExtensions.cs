using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FWS.Generic.Framework.Helpers
{
    public static class HttpResponseMessageExtensions
    {
        public static T GetData<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.GetErrorMessage());

            var data = AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
            if (string.IsNullOrEmpty(data))
                throw new Exception("Request returned no content.");

            return JsonConvert.DeserializeObject<T>(data);
        }

        public static string GetErrorMessage(this HttpResponseMessage response)
        {
            return AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
        }

        public static async Task<T> PostDataAsync<T, TPayload>(string action, string url, TPayload postData) where T : class
        {
            try
            {
                var client = GetClient();

                client.BaseAddress = new Uri(url);

                if (typeof(T) == typeof(MemoryStream))
                {

                    var response = client.Post(action, postData);

                    var strResponse = await response.Content.ReadAsStreamAsync();

                    return strResponse as T;
                }

                if (typeof(T) == typeof(Tuple<bool, string, long>))
                {
                    var result = client.Post(action, postData);

                    var strResponse = await result.Content.ReadAsStringAsync();

                    if (!strResponse.IsValidJson())
                        return new Tuple<bool, string, long>(result.IsSuccessStatusCode, strResponse,
                            0) as T;

                    var resultStatus = JsonConvert.DeserializeObject<ResultStatus>(strResponse,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

                    return new Tuple<bool, string, long>(result.IsSuccessStatusCode, null,
                        resultStatus.FileId) as T;
                }
                else
                {
                    var response = client.Post(action, postData);

                    var strResponse = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<T>(strResponse,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    return result;
                }
            }
            catch (WebException)
            {
                return null;
            }
        }
        
        public static T PostData<T, TPayload>(string action, string url, TPayload postData) where T : class
        {
            try
            {
                var client = GetClient();

                client.BaseAddress = new Uri(url);

                if (typeof(T) == typeof(MemoryStream))
                {

                    var response = client.Post(action, postData);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var sw = new StreamWriter(ms);
                        try
                        {
                            sw.Write(response.Content);
                            sw.Flush();//otherwise you are risking empty stream
                            ms.Seek(0, SeekOrigin.Begin);

                            return ms as T;
                            // Test and work with the stream here. 
                            // If you need to start back at the beginning, be sure to Seek again.
                        }
                        finally
                        {
                            sw.Dispose();
                        }
                    }

                }

                if (typeof(T) == typeof(Tuple<bool, string, long?>))
                {

                    var response = client.Post(action, postData);


                    // Read the stream to a string, and write the string to the console.
                    var biRes = response.Content.ReadAsStringAsync().Result;

                    if (!biRes.IsValidJson())
                        return new Tuple<bool, string, long?>(response.IsSuccessStatusCode, biRes,
                            null) as T;

                    var resultStatus = JsonConvert.DeserializeObject<ResultStatus>(biRes);

                    return new Tuple<bool, string, long?>(response.IsSuccessStatusCode, null,
                        resultStatus.FileId) as T;

                }
                else
                {
                    var response = client.Post(action, postData);

                    var biRes = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<T>(biRes);
                }
            }
            catch (WebException)
            {
                return null;
            }
        }

        public static bool IsValidJson(this string strInput)
        {
            strInput = strInput.Trim();
            if ((!strInput.StartsWith("{") || !strInput.EndsWith("}")) &&
                (!strInput.StartsWith("[") || !strInput.EndsWith("]"))) return false;
            try
            {
                JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static HttpClient GetClient()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    public class ResultStatus
    {
        public long FileId { get; set; }
    }
}
