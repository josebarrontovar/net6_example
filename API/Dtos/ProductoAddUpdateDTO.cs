namespace API.Dtos
{
    public class ProductoAddUpdateDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
        public ProductoAddUpdateDTO()
        {

        }
    }
}
