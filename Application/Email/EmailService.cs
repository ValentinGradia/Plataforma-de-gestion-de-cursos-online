using System.Net;
using System.Net.Mail;

namespace PlataformaDeGestionDeCursosOnline.Application.Email;

public class EmailService : IEmailService
{
    public async Task EnviarEmailAsync(Domain.Entities.Usuarios.ObjectValues.Email destinatario, string asunto, string cuerpo)
    {
        if (destinatario is null) throw new ArgumentNullException(nameof(destinatario));
        if (string.IsNullOrWhiteSpace(asunto)) throw new ArgumentException("El asunto es obligatorio.", nameof(asunto));
        if (string.IsNullOrWhiteSpace(cuerpo)) throw new ArgumentException("El cuerpo es obligatorio.", nameof(cuerpo));

        var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
        var smtpPortRaw = Environment.GetEnvironmentVariable("SMTP_PORT");
        var smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
        var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS");
        var smtpFrom = Environment.GetEnvironmentVariable("SMTP_FROM");
        var smtpEnableSslRaw = Environment.GetEnvironmentVariable("SMTP_ENABLE_SSL");

        if (string.IsNullOrWhiteSpace(smtpHost) ||
            string.IsNullOrWhiteSpace(smtpPortRaw) ||
            string.IsNullOrWhiteSpace(smtpUser) ||
            string.IsNullOrWhiteSpace(smtpPass) ||
            string.IsNullOrWhiteSpace(smtpFrom))
        {
            throw new InvalidOperationException("Configuracion SMTP incompleta. Defini SMTP_HOST, SMTP_PORT, SMTP_USER, SMTP_PASS y SMTP_FROM.");
        }

        if (!int.TryParse(smtpPortRaw, out var smtpPort))
        {
            throw new InvalidOperationException("SMTP_PORT no es un numero valido.");
        }

        var enableSsl = bool.TryParse(smtpEnableSslRaw, out var parsedSsl) && parsedSsl;

        using var message = new MailMessage
        {
            From = new MailAddress(smtpFrom),
            Subject = asunto,
            Body = cuerpo,
            IsBodyHtml = false
        };

        message.To.Add(destinatario.ToString() ?? throw new InvalidOperationException("No se pudo obtener el email del destinatario."));

        using var smtpClient = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = enableSsl
        };

        await smtpClient.SendMailAsync(message);
    }
}