
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibrosApi.Models;

public class Libro 
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, MinimumLength = 3)]
    public required string Titulo { get; set; }

    [Required]
    [StringLength(100)]
    public required string Autor { get; set; }

    [Range(1800, 2100)]
    public int AñoPublicacion { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }

    [JsonIgnore] 
    public string? CampoInterno { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}