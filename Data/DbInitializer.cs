
using LibrosApi.Models;  
using Microsoft.EntityFrameworkCore;

namespace LibrosApi.Data;  

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
       
        if (context.Libros.Any())
            return;

        var libros = new List<Libro>
        {
            new()
            {
                Titulo = "Cien años de soledad",
                Autor = "Gabriel García Márquez",
                AñoPublicacion = 1967,
                Descripcion = "Obra maestra del realismo mágico",
                CampoInterno = "INT-001", 
                FechaCreacion = DateTime.UtcNow
            },
            new()
            {
                Titulo = "1984",
                Autor = "George Orwell",
                AñoPublicacion = 1949,
                Descripcion = "Distopía sobre vigilancia y control",
                CampoInterno = "INT-002",  
                FechaCreacion = DateTime.UtcNow
            },
            new()
            {
                Titulo = "El principito",
                Autor = "Antoine de Saint-Exupéry",
                AñoPublicacion = 1943,
                Descripcion = "Fábula filosófica para todas las edades",
                CampoInterno = "INT-003",  
                FechaCreacion = DateTime.UtcNow
            }
        };

        context.Libros.AddRange(libros);
        context.SaveChanges();
    }
}