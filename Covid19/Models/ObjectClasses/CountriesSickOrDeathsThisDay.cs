using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountriesSickOrDeathsThisDay
    {
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonProperty("New Cases")]
        [JsonPropertyName("New Cases")]
        public int NewCases { get; set; }
    }
}
