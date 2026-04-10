using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlataformaDeGestionDeCursosOnline.Infrastructure;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Extensions;

//Creamos una extension que va a permitir ejecutar los archivos de migracion que a futuro voy a crear.
public static class ApplicationBuilderExtensions
{
    public static async void ApplyMigrations(this IApplicationBuilder app)
    {
        //Primero creamos un contexto de servicio para poder  ejecutar las migraciones
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            try
                // Aquí puedes resolver tus servicios de migración y ejecutar las migraciones
            {
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de que las migraciones fallen
                var logger = loggerFactory.CreateLogger("Migration");
                logger.LogError(ex, "Error en  migracion");
            }
        }
    }
}