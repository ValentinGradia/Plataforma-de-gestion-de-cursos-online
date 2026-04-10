using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Configurations;

internal class ClaseConfiguration : IEntityTypeConfiguration<Clase>
{
    public void Configure(EntityTypeBuilder<Clase> builder)
    {
        builder.ToTable("Clases");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.IdCurso)
            .HasColumnName("IdCurso")
            .IsRequired();
        
        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(c => c.IdCurso) ;
        
        builder.Property(c => c.Material).HasColumnName("Material").HasMaxLength(500).IsRequired();
        builder.Property(c => c.Estado).HasColumnName("Estado").HasConversion<int>().IsRequired();
        builder.Property(c => c.Fecha).HasColumnName("Fecha").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder.Ignore(c => c.Asistencias);
        builder.Ignore(c => c._consultas);
    }
}