using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Email;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases.DomainEvents;

//Componente que consume el domain event de consulta cargada
internal class AgregarConsultaDomainEventHandler : INotificationHandler<ConsultaCargada>
{

    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IEmailService _emailService;
    
    public AgregarConsultaDomainEventHandler(IEstudianteRepository estudianteRepository, IEmailService emailService)
    {
        _estudianteRepository = estudianteRepository;
        _emailService = emailService;
    }
    
    //Solo una responsabilidad, cuando se carga la consulta -> avisamos al usuario via email que ya fue cargada.
    public async Task Handle(ConsultaCargada notification, CancellationToken cancellationToken)
    {
        Usuario usuario = await this._estudianteRepository.ObtenerPorIdAsync(notification.IdUsuarioQueCargoLaConsulta, cancellationToken);

        if (usuario is null)
            throw new NotFoundException();
        
        await this._emailService.EnviarEmailAsync(
            usuario.Email,
            "Consulta cargada",
            $"{usuario.Nombre}, Tu consulta ya fue cargada en la clase " +
            cancellationToken
        );
    
    }
}