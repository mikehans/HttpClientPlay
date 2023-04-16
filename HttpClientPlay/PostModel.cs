using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace HttpClientPlay
{
    public class PostModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
