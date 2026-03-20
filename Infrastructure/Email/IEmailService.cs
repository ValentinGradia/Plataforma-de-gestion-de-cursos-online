namespace PlataformaDeGestionDeCursosOnline.Application.Abstractions.Email;

//Interfaz para el servicio de envío de correos electrónicos
public interface IEmailService
{
    Task EnviarEmailAsync(Domain.GlobalObjectValues.Email destinatario, string asunto, string cuerpo);
}