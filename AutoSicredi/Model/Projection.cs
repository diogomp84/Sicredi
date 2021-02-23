using System.Text.Json.Serialization;

namespace AutoSicredi.Model
{
    public class Projection
    {
        [JsonPropertyName("tempo")]
        public string Tempo;

        [JsonPropertyName("valor")]
        public string Valor;
    }
}
