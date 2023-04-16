using Serilog;
using System;
using System.Linq;
using System.Text.Json;

namespace HttpClientPlay
{
    public class GenericDataLoader<T>
    {
        readonly IHttpClientFactory _httpClientFactory;
        public GenericDataLoader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> LoadData(string path, int id = 1)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            Uri baseUri = new Uri("https://jsonplaceholder.typicode.com");
            string uriPath = string.Join("/", path, id);

            httpClient.BaseAddress = baseUri;

            using HttpResponseMessage response = await httpClient.GetAsync(uriPath);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                T? parsedData = JsonSerializer.Deserialize<T>(data);

                return parsedData;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
