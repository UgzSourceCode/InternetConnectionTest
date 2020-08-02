using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.DTO
{
    [JsonObject("AppConfig")]
    public class ConfigDTO
    {
        [JsonProperty("Hosts")]
        public List<string> Hosts { get; set; }
        [JsonProperty("RepeatConfig")]
        public RepeatConfigDTO RepeatConfig { get; set; }
    }
}
