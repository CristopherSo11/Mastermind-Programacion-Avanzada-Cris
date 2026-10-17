using System.Text.Json.Serialization;

namespace Mastermind.BusinessLogic
{
    public class CheatCode
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; } = string.Empty;

        [JsonPropertyName("usosDisponibles")]
        public int UsosDisponibles { get; set; }
    }
}
