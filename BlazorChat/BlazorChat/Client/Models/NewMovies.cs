using BlazorChat.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorChat.Client.Models
{
    public class NewMovies
    {

            [JsonPropertyName("dates")]
            public Date Dates { get; set; }

            [JsonPropertyName("page")]
            public int Page { get; set; }

            [JsonPropertyName("results")]
            public IEnumerable<PopularMovie> Results { get; set; } = null;

            [JsonPropertyName("total_pages")]
            public int TotalPages { get; set; }

            [JsonPropertyName("total_results")]
            public int TotalResults { get; set; }
        

        public class Date
        {
            [JsonPropertyName("maximum")]
            public string Maximum { get; set; }

            [JsonPropertyName("minimum")]
            public string Minimum { get; set; }

        }





    }
}
