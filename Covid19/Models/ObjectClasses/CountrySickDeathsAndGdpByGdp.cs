using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountrySickDeathsAndGdpByGdp
    {
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }


        [JsonProperty("cumulativeDeaths")]
        [JsonPropertyName("cumulativeDeaths")]
        public int CumulativeDeaths { get; set; }


        [JsonProperty("gdp")]
        [JsonPropertyName("gdp")]
        public double Gdp { get; set; }


        [JsonProperty("cumulativeCases")]
        [JsonPropertyName("cumulativeCases")]
        public int CumulativeCases { get; set; }
    }
}
