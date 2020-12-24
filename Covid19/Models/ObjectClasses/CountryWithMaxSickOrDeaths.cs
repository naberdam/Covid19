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
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonProperty("maxSickOrDeaths")]
        [JsonPropertyName("maxSickOrDeaths")]
        public int MaxSickOrDeaths { get; set; }
    }
}
