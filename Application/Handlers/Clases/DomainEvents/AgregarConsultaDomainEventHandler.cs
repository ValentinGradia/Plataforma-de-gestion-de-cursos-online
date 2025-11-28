using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Email;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases.DomainEvents;

internal class AgregarConsultaDomainEventHandler : INotificationHandler<ConsultaCargada>
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;
    
    public AgregarConsultaDomainEventHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }
    
    public async Task Handle(ConsultaCargada notification, CancellationToken cancellationToken)
    {
        Task<Usuario> TaskUser = this._usuarioRepository.ObtenerPorIdAsync(notification.)
    }
}