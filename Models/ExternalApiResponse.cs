using System.Text.Json.Serialization;

namespace ApiAlertaDengue.Models
{
    public class ExternalApiResponse
    {
        [JsonPropertyName("SE")]
        public int SemanaEpidemologica { get; set; }

        [JsonPropertyName("casos_est")]
        public double CasosEstimados { get; set; }  

        [JsonPropertyName("casos")]
        public int CasosNotificados { get; set; }

        [JsonPropertyName("nivel")]
        public int Nivel { get; set; }
    }
}