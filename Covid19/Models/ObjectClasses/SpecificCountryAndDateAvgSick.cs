using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class SpecificCountryAndDateAvgSick
    {
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonProperty("Avg Sick")]
        [JsonPropertyName("Avg Sick")]
        public double AvgSick { get; set; }
    }
}
