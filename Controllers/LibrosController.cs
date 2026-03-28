
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrosApi.Data;      
using LibrosApi.Models;    
using LibrosApi.DTOs;      

namespace LibrosApi.Controllers;  

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LibrosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<LibrosController> _logger;

    public LibrosController(
        ApplicationDbContext context,
        ILogger<LibrosController> logger)
    {
        _context = context;
        _logger = logger;
    }

   
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LibroDto>>> GetLibros()
    {
        var libros = await _context.Libros
            .OrderBy(l => l.Titulo)
            .Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                AñoPublicacion = l.AñoPublicacion,
                Descripcion = l.Descripcion,
                FechaCreacion = l.FechaCreacion
            })
            .ToListAsync();

        return Ok(libros);
    }

    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LibroDto>> GetLibro(int id)
    {
        var libro = await _context.Libros.FindAsync(id);

        if (libro is null)
        {
            _logger.LogWarning("Libro con ID {Id} no encontrado", id);
            return NotFound(new { message = $"Libro con ID {id} no encontrado" });
        }

        var libroDto = new LibroDto
        {
            Id = libro.Id,
            Titulo = libro.Titulo,
            Autor = libro.Autor,
            AñoPublicacion = libro.AñoPublicacion,
            Descripcion = libro.Descripcion,
            FechaCreacion = libro.FechaCreacion
        };

        return Ok(libroDto);
    }

    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LibroDto>> CreateLibro([FromBody] CreateLibroDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Datos inválidos",
                errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
            });
        }

        var libro = new Libro
        {
            Titulo = dto.Titulo,
            Autor = dto.Autor,
            AñoPublicacion = dto.AñoPublicacion,
            Descripcion = dto.Descripcion,
            CampoInterno = $"INT-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            FechaCreacion = DateTime.UtcNow
        };

        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();

        var libroDto = new LibroDto
        {
            Id = libro.Id,
            Titulo = libro.Titulo,
            Autor = libro.Autor,
            AñoPublicacion = libro.AñoPublicacion,
            Descripcion = libro.Descripcion,
            FechaCreacion = libro.FechaCreacion
        };

        return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libroDto);
    }

 
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLibro(int id, [FromBody] UpdateLibroDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Datos inválidos",
                errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
            });
        }

        var libro = await _context.Libros.FindAsync(id);
        if (libro is null)
        {
            return NotFound(new { message = $"Libro con ID {id} no encontrado" });
        }

        if (dto.Titulo is not null) libro.Titulo = dto.Titulo;
        if (dto.Autor is not null) libro.Autor = dto.Autor;
        if (dto.AñoPublicacion.HasValue) libro.AñoPublicacion = dto.AñoPublicacion.Value;
        if (dto.Descripcion is not null) libro.Descripcion = dto.Descripcion;

        await _context.SaveChangesAsync();

        return NoContent();
    }

   
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLibro(int id)
    {
        var libro = await _context.Libros.FindAsync(id);

        if (libro is null)
        {
            return NotFound(new { message = $"Libro con ID {id} no encontrado" });
        }

        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}