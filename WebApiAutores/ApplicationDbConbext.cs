using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;

namespace WebApiAutores
{
    public class ApplicationDbConbext : DbContext
    {
        public ApplicationDbConbext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}

