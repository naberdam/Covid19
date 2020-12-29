using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountrySickAndDeathsPerMillionAndGrowth
    {
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }


        [JsonProperty("growth")]
        [JsonPropertyName("growth")]
        public double Growth { get; set; }        

        [JsonProperty("deathPerMillion")]
        [JsonPropertyName("deathPerMillion")]
        public double DeathPerMillion { get; set; }

        [JsonProperty("sickPerMillion")]
        [JsonPropertyName("sickPerMillion")]
        public double SickPerMillion { get; set; }
    }
}
