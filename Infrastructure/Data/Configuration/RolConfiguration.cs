
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol");
            builder.Property(_ => _.Id)
                .IsRequired();
            builder.Property(_ => _.Nombre)
                .IsRequired()
                .HasMaxLength(200);
           
        }
    }
}
