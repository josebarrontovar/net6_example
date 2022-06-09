

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public ICollection<Producto>? Productos { get; set; }
    }
}
