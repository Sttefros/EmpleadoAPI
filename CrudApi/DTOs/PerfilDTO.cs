using System.Text.Json.Serialization;

namespace CrudApi.DTOs
{
    public class PerfilDTO
    {
        [JsonPropertyName("idPerfil")]
        public int IdPerfil { get; set; }

        public string? Nombre { get; set; }
    }
}
