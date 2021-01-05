using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountriesDeathsVsDensity2020PerMillion
    {
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }


        [JsonProperty("Population Density")]
        [JsonPropertyName("Population Density")]
        public double PopDensity { get; set; }


        [JsonProperty("deaths per million")]
        [JsonPropertyName("deaths per million")]
        public double DeathsPerMillion { get; set; }


        [JsonProperty("sick per million")]
        [JsonPropertyName("sick per million")]
        public double SickPerMillion { get; set; }
    }
}
