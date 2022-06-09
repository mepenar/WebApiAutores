using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Controllers.Entidades
{
    public class Autor: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength:5, ErrorMessage = "el campo {0} no debe tenre mas de {1} caracteres")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        //[Range(18, 120)]
        //[NotMapped]
        //public int Edad { get; set; }
        //[CreditCard]
        //[NotMapped]
        //public string TarjetaCreditp{ get; set; }
        //[Url]
        //[NotMapped]
        //public string Url{ get; set; }
        //public int Mayor{ get; set; }
        //public int Menor{ get; set; }
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúsucla",
                        new string[] { nameof(Nombre) });
                }
            }

            //if (Mayor < Menor) yield return new ValidationResult("El campo no puede ser mas grande que el campo mayor", new string[] { nameof(Menor) });
        }
    }
}
