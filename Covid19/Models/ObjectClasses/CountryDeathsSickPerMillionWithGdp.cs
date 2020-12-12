using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class CountryDeathsSickPerMillionWithGdp
    {
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonProperty("deathPerMillion")]
        [JsonPropertyName("deathPerMillion")]
        public double DeathPerMillion { get; set; }

        [JsonProperty("sickPerMillion")]
        [JsonPropertyName("sickPerMillion")]
        public double SickPerMillion { get; set; }

        [JsonProperty("gdp")]
        [JsonPropertyName("gdp")]
        public double Gdp { get; set; }
    }
}
