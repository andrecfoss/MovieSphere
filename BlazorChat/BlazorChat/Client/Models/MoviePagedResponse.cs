using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorChat.Models
{
    public class MoviePagedResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<PopularMovie> Results { get; set; } = null;

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
