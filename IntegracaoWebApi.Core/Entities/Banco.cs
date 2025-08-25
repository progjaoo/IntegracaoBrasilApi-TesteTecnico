using System.Text.Json.Serialization;

namespace IntegracaoWebApi.Core.Entities
{
    public class Banco
    {
        public int Id { get; set; }

        [JsonPropertyName("ispb")]
        public string? Ispb { get; set; }

        [JsonPropertyName("name")]
        public string? Nome { get; set; }

        [JsonPropertyName("code")]
        public int? Codigo { get; set; }

        [JsonPropertyName("fullName")]
        public string? NomeCompleto { get; set; }
    }
}
