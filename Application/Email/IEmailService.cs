//Interfaz para el servicio de envío de correos electrónicos
namespace PlataformaDeGestionDeCursosOnline.Application.Email;

public interface IEmailService
{
    Task EnviarEmailAsync(Domain.Entities.Usuarios.ObjectValues.Email destinatario, string asunto, string cuerpo);
}