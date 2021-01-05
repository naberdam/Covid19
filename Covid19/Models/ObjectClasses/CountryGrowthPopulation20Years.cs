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
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonProperty("Divide Population")]
        [JsonPropertyName("Divide Population")]
        public double DividePopulation { get; set; }
    }
}
