using AutoSicredi.Base;
using Newtonsoft.Json;

namespace AutoSicredi.Config
{
    [JsonObject("apiTestSettings")]
    public class ApiTestSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aut")]
        public string AUT { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
