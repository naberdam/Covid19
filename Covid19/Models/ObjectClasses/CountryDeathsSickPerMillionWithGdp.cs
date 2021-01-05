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
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonProperty("Death Per Million")]
        [JsonPropertyName("Death Per Million")]
        public double DeathPerMillion { get; set; }

        [JsonProperty("Sick Per Million")]
        [JsonPropertyName("Sick Per Million")]
        public double SickPerMillion { get; set; }

        [JsonProperty("Gdp")]
        [JsonPropertyName("Gdp")]
        public double Gdp { get; set; }
    }
}
