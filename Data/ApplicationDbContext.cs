using LibrosApi.Models;  
using Microsoft.EntityFrameworkCore;

namespace LibrosApi.Data; 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

   
    public DbSet<Libro> Libros => Set<Libro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Libro>().HasData(
            new Libro
            {
                Id = 1,
                Titulo = "Cien años de soledad",
                Autor = "Gabriel García Márquez",
                AñoPublicacion = 1967,
                Descripcion = "Obra cumbre del realismo mágico"
            },
            new Libro
            {
                Id = 2,
                Titulo = "1984",
                Autor = "George Orwell",
                AñoPublicacion = 1949,
                Descripcion = "Distopía sobre el totalitarismo"
            },
            new Libro
            {
                Id = 3,
                Titulo = "El principito",
                Autor = "Antoine de Saint-Exupéry",
                AñoPublicacion = 1943,
                Descripcion = "Clásico infantil con mensaje filosófico"
            }
        );

        
    }
}