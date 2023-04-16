using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Serilog;

namespace HttpClientPlay;

public class PostsProcessor
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PostsProcessor(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<PostModel> LoadPost(int postId = 1)
    {
        string baseUrl = "https://jsonplaceholder.typicode.com";
        HttpClient httpClient = _httpClientFactory.CreateClient(baseUrl);
        httpClient.BaseAddress = new Uri(baseUrl);

        using HttpResponseMessage response = await httpClient.GetAsync($"posts/{postId}");

        if (response.IsSuccessStatusCode)
        {
            var postString = await response.Content.ReadAsStringAsync();
            PostModel? post = JsonSerializer.Deserialize<PostModel>(postString);

            return post;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
