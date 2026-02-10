using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Email;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases.DomainEvents;

//Componente que consume el domain event de consulta cargada
internal class AgregarConsultaDomainEventHandler : INotificationHandler<ConsultaCargada>
{

    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IEmailService _emailService;
    private readonly ICursoRepository _cursoRepository;
    
    public AgregarConsultaDomainEventHandler(IEstudianteRepository estudianteRepository, IEmailService emailService, ICursoRepository cursoRepository)
    {
        _estudianteRepository = estudianteRepository;
        _emailService = emailService;
        _cursoRepository = cursoRepository;
    }
    
    //Solo una responsabilidad, cuando se carga la consulta -> avisamos al usuario via email que ya fue cargada.
    public async Task Handle(ConsultaCargada notification, CancellationToken cancellationToken)
    {
        Usuario usuario = await this._estudianteRepository.ObtenerPorIdAsync(notification.IdUsuarioQueCargoLaConsulta, cancellationToken);
        Curso curso = await this._cursoRepository.ObtenerPorIdAsync(notification.IdCurso, cancellationToken);
        
        if (usuario is null)
            throw new NotFoundException();
        
        await this._emailService.EnviarEmailAsync(
            usuario.Email,
            $"Consulta en curso {curso.Nombre}",
            $"{usuario.Nombre}, Tu consulta ya fue cargada en la clase " + notification.fechaConsulta +
            cancellationToken
        );
    
    }
}