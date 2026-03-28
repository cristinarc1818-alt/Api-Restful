using System.ComponentModel.DataAnnotations;

namespace LibrosApi.DTOs;

public record UpdateLibroDto
{
    [StringLength(200, MinimumLength = 3)]
    public string? Titulo { get; set; }

    [StringLength(100)]
    public string? Autor { get; set; }

    [Range(1800, 2100)]
    public int? AñoPublicacion { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }
}