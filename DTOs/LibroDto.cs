namespace LibrosApi.DTOs;

public record LibroDto
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Autor { get; set; }
    public int AñoPublicacion { get; set; }
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}