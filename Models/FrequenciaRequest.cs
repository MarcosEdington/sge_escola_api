using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sge_escola_api.Models
{
    public class FrequenciaRequest
    {
        [JsonPropertyName("turmaId")]
        public string TurmaId { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("presencas")]
        public List<RegistroFrequencia> Presencas { get; set; }
    }

    public class RegistroFrequencia
    {
        [JsonPropertyName("alunoId")]
        public int AlunoId { get; set; }

        [JsonPropertyName("presente")]
        public bool Presente { get; set; }
    }
}