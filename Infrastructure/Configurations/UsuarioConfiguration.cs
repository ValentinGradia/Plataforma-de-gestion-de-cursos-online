using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();
        builder.Property(u => u.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();

        builder.OwnsOne(u => u.Direccion, direccion =>
        {
            direccion.Property(d => d.Pais)
                .HasColumnName("pais")
                .HasMaxLength(100)
                .IsRequired();

            direccion.Property(d => d.Ciudad)
                .HasColumnName("ciudad")
                .HasMaxLength(100)
                .IsRequired();

            direccion.Property(d => d.Calle)
                .HasColumnName("calle")
                .HasMaxLength(150)
                .IsRequired();

            direccion.Property(d => d.Altura)
                .HasColumnName("altura")
                .IsRequired();
        });

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .HasConversion(
                email => email.valorEmail,
                valor => Email.CrearEmail(valor))
            .IsRequired();

        builder.Property(u => u.Contraseña)
            .HasColumnName("contraseña")
            .HasMaxLength(255)
            .HasConversion(
                contraseña => contraseña.ValorContraseña,
                valor => Contraseña.CrearContraseña(valor))
            .IsRequired();
        
        builder.Property(u => u.Dni)
            .HasColumnName("dni")
            .HasMaxLength(20)
            .HasConversion(
                dni => dni.Valor,
                valor => DNI.CrearDNI(valor))
            .IsRequired();

        //Distinguimos subtipos de usuario mediante un discriminator
        builder.HasDiscriminator<Roles>("rol")
            .HasValue<Estudiante>(Roles.Estudiante)
            .HasValue<Profesor>(Roles.Profesor);

        builder.Property(u => u.FechaRegistro)
            .HasColumnName("fecha_registro")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // La propiedad Especialidad se configura en ProfesorConfiguration.
    }
}