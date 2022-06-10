﻿using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        [Required]
        public string Titulo{ get; set; }
        public List<Comentario> Comenatrios{ get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }
    }
}
