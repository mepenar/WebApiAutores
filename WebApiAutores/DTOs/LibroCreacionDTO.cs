using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class LibroCreacionDTO
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "el campo {0} no debe tenre mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
