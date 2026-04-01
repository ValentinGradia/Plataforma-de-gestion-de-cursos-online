using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class ClaseConfiguration : IEntityTypeConfiguration<Clase>
{
    public void Configure(EntityTypeBuilder<Clase> builder)
    {
        builder.ToTable("clases");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.IdCurso)
            .HasColumnName("id_curso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(c => c.IdCurso) ;
        
        builder.Property(c => c.Material).HasColumnName("material").HasMaxLength(500).IsRequired();
        builder.Property(c => c.Estado).HasColumnName("estado").HasConversion<int>().IsRequired();
        builder.Property(c => c.Fecha).HasColumnName("fecha").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder.Ignore(c => c.Asistencias);
        builder.Ignore(c => c._consultas);
    }
}