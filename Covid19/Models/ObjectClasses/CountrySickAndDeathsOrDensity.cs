using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountrySickAndDeathsOrDensity
    {
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }


        [JsonProperty("popTotal")]
        [JsonPropertyName("popTotal")]
        public double PopTotal { get; set; }


        [JsonProperty("cumulativeDeaths")]
        [JsonPropertyName("cumulativeDeaths")]
        public int CumulativeDeaths { get; set; }


        [JsonProperty("popDensity")]
        [JsonPropertyName("popDensity")]
        public double PopDensity { get; set; }


        [JsonProperty("cumulativeCases")]
        [JsonPropertyName("cumulativeCases")]
        public int CumulativeCases { get; set; }

        [JsonProperty("deathPerMillion")]
        [JsonPropertyName("deathPerMillion")]
        public double DeathPerMillion { get; set; }

        [JsonProperty("sickPerMillion")]
        [JsonPropertyName("sickPerMillion")]
        public double SickPerMillion { get; set; }
    }
}
