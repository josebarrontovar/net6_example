
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.Property(_ => _.Id)
                .IsRequired();
            builder.Property(_ => _.Nombre)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(_ => _.ApellidoPaterno)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(_ => _.Username)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(_ => _.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(_ => _.Roles)
                .WithMany(_ => _.Usuarios)
                .UsingEntity<UsuariosRoles>(
                _ => _.
                HasOne(_ => _.Rol)
                .WithMany(_ => _.UsuariosRoles)
                .HasForeignKey(_ => _.RolId),
                _ => _.
                HasOne(_ => _.Usuario)
                .WithMany(_ => _.UsuariosRoles)
                .HasForeignKey(_ => _.UsuarioId),
                _ =>
                {
                    _.HasKey(_ => new { _.UsuarioId, _.RolId });
                    ;
                });

        }
    }
}
