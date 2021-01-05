using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountriesDeathsVsDensity2020
    {
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }


        [JsonProperty("Cumulative Deaths")]
        [JsonPropertyName("Cumulative Deaths")]
        public int CumulativeDeaths { get; set; }

        [JsonProperty("Cumulative Cases")]
        [JsonPropertyName("Cumulative Cases")]
        public int CumulativeCases { get; set; }


        [JsonProperty("Population Density")]
        [JsonPropertyName("Population Density")]
        public double PopDensity { get; set; }
        
    }
}
