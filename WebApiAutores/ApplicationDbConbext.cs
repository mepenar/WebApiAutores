using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
namespace WebApiAutores
{
    public class ApplicationDbConbext : DbContext
    {
        public ApplicationDbConbext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AutorLibro>().HasKey(al => new { al.AutorId, al.LibroId });
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<AutorLibro> AutoresLibros { get; set; }
        public DbSet<Computador> Computadores { get; set; }
        public DbSet<Componente> Componentes { get; set; }
        public DbSet<Observacion> Observacions { get; set; }    
    }
}

