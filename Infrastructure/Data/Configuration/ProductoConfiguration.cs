using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");
            builder.Property(_ => _.Id).IsRequired();
            builder.Property(_ => _.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(_ => _.Precio).HasColumnType("decimal(18,2)");

            builder.HasOne(_ => _.Marca).WithMany(_ => _.Productos).HasForeignKey(_ => _.MarcaId);
            builder.HasOne(_ => _.Categoria).WithMany(_ => _.Productos).HasForeignKey(_ => _.CategoriaId);


        }
    }
}
