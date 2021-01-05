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
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonProperty("Growth")]
        [JsonPropertyName("Growth")]
        public double Growth { get; set; }

        [JsonProperty("Cumulative Deaths")]
        [JsonPropertyName("Cumulative Deaths")]
        public int CumulativeDeaths { get; set; }

        [JsonProperty("Cumulative Cases")]
        [JsonPropertyName("Cumulative Cases")]
        public int CumulativeCases { get; set; }
    }
}
