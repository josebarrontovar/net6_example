

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Marca
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public ICollection<Producto>? Productos { get; set; }
    }
}
