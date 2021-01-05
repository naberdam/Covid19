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
        [JsonProperty("Country")]
        [JsonPropertyName("Country")]
        public string Country { get; set; }


        [JsonProperty("Growth")]
        [JsonPropertyName("Growth")]
        public double Growth { get; set; }        

        [JsonProperty("Death Per Million")]
        [JsonPropertyName("Death Per Million")]
        public double DeathPerMillion { get; set; }

        [JsonProperty("Sick Per Million")]
        [JsonPropertyName("Sick Per Million")]
        public double SickPerMillion { get; set; }
    }
}
