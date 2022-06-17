﻿using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
namespace WebApiAutores
{
    public class ApplicationDbConbext : DbContext
    {
        public ApplicationDbConbext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<AutorLibro> AutoresLibros { get; set; }
    }
}

