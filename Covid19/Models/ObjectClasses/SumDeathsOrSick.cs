using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19.Models.ObjectClasses
{
    public class SumDeathsOrSick
    {
        [JsonProperty("sum")]
        [JsonPropertyName("sum")]
        public int Sum { get; set; }
    }
}
