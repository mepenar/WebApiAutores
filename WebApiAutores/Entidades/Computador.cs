namespace WebApiAutores.Entidades
{
    public class Computador
    {
        public int Id { get; set; }
        public string Marca{ get; set; }
        public List<Componente> Componentes {get; set;}
    }
}
