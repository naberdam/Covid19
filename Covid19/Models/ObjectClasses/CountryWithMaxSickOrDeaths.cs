using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountryWithMaxSickOrDeaths
    {
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonProperty("Max Sick Or Deaths")]
        [JsonPropertyName("Max Sick Or Deaths")]
        public int MaxSickOrDeaths { get; set; }
    }
}
