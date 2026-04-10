using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure;

public class DesignTimeDbContextFactory 
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Try to find Presentation directory reliably
        string currentDir = Directory.GetCurrentDirectory();
        
        // Determinar dinámicamente dónde está el directorio de Presentation
        string basePath = currentDir;
        
        if (currentDir.EndsWith("Infrastructure")) 
        {
            basePath = Path.Combine(currentDir, "../Presentation");
        }
        else if (!currentDir.EndsWith("Presentation"))
        {
            basePath = Path.Combine(currentDir, "Presentation");
        }
        
        basePath = Path.GetFullPath(basePath);
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        
        var connection = configuration.GetConnectionString("Database");

        //  Configuración de EF
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("Database"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}