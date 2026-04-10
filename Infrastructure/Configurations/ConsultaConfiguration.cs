using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.ToTable("Consultas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.IdClase)
            .HasColumnName("IdClase")
            .IsRequired();

        builder.HasOne<Clase>()
            .WithMany()
            .HasForeignKey(c => c.IdClase);
        
        builder.Property(c => c.IdEstudiante)
            .HasColumnName("IdEstudiante")
            .IsRequired();
        
        builder.HasOne<Estudiante>()
            .WithMany()
            .HasForeignKey(c => c.IdEstudiante);

        builder.Property(c => c.Titulo)
            .HasColumnName("Titulo")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Descripcion)
            .HasColumnName("Descripcion")
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(c => c.FechaConsulta)
            .HasColumnName("FechaConsulta")
            .IsRequired();
    }
}

