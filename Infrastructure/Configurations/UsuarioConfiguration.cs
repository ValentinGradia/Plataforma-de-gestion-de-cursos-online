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
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nombre).HasColumnName("Nombre").HasMaxLength(50).IsRequired();
        builder.Property(u => u.Apellido).HasColumnName("Apellido").HasMaxLength(100).IsRequired();

        builder.OwnsOne(u => u.Direccion, direccion =>
        {
            direccion.Property(d => d.Pais)
                .HasColumnName("Pais")
                .HasMaxLength(100)
                .IsRequired();

            direccion.Property(d => d.Ciudad)
                .HasColumnName("Ciudad")
                .HasMaxLength(100)
                .IsRequired();

            direccion.Property(d => d.Calle)
                .HasColumnName("Calle")
                .HasMaxLength(150)
                .IsRequired();

            direccion.Property(d => d.Altura)
                .HasColumnName("Altura")
                .IsRequired();
        });

        builder.Property(u => u.Email)
            .HasColumnName("Email")
            .HasMaxLength(150)
            .HasConversion(
                email => email.valorEmail,
                valor => Email.CrearEmail(valor))
            .IsRequired();

        builder.Property(u => u.Contraseña)
            .HasColumnName("Contrasea")
            .HasMaxLength(255)
            .HasConversion(
                contrasea => contrasea.ValorContraseña,
                valor => Contraseña.CrearContraseña(valor))
            .IsRequired();
        
        builder.Property(u => u.Dni)
            .HasColumnName("Dni")
            .HasMaxLength(20)
            .HasConversion(
                dni => dni.Valor,
                valor => DNI.CrearDNI(valor))
            .IsRequired();

        //Distinguimos subtipos de usuario mediante un discriminator
        builder.HasDiscriminator<Roles>("Rol")
            .HasValue<Estudiante>(Roles.Estudiante)
            .HasValue<Profesor>(Roles.Profesor);
        
        //Casteamos el enum a string para que sea ms legible en la base de datos
        builder
            .Property("Rol")
            .HasConversion<string>();

        builder.Property(u => u.FechaRegistro)
            .HasColumnName("FechaRegistro")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();
        
        builder.Property("Especialidad")
            .HasColumnName("Especialidad")
            .HasMaxLength(100)
            .IsRequired(false);
        

        // La propiedad Especialidad se configura en ProfesorConfiguration.
    }
}