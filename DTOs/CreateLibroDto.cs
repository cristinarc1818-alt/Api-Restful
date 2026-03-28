using System.ComponentModel.DataAnnotations;

namespace LibrosApi.DTOs;

public record CreateLibroDto
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, MinimumLength = 3)]
    public required string Titulo { get; set; }

    [Required]
    [StringLength(100)]
    public required string Autor { get; set; }

    [Range(1800, 2100, ErrorMessage = "Año inválido")]
    public int AñoPublicacion { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }
}