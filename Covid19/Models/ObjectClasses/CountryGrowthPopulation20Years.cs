using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountryGrowthPopulation20Years
    {
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonProperty("dividePopulation")]
        [JsonPropertyName("dividePopulation")]
        public double DividePopulation { get; set; }
    }
}
