using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountrySickAndDeathsAndGrowth
    {
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonProperty("growth")]
        [JsonPropertyName("growth")]
        public double Growth { get; set; }

        [JsonProperty("cumulativeDeaths")]
        [JsonPropertyName("cumulativeDeaths")]
        public int CumulativeDeaths { get; set; }

        [JsonProperty("cumulativeCases")]
        [JsonPropertyName("cumulativeCases")]
        public int CumulativeCases { get; set; }
    }
}
