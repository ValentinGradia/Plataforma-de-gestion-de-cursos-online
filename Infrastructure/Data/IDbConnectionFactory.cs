using System.Data;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data;

//Nuestra interfaz para conexiones a la base de datos
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

