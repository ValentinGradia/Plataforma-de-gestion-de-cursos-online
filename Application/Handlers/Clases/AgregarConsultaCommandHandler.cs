using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class AgregarConsultaCommandHandler : ICommandHandler<AgregarConsultaCommand>
{
    private readonly IClaseRepository _claseRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AgregarConsultaCommandHandler(IClaseRepository claseRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        this._claseRepository = claseRepository;
        this._usuarioRepository = usuarioRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task Handle(AgregarConsultaCommand request, CancellationToken cancellationToken)
    {
        Task<Usuario> TaskUser = this._usuarioRepository.ObtenerPorIdAsync(request.IdUsuario, cancellationToken);
        Task<Clase> TaskClase = this._claseRepository.ObtenerPorIdAsync(request.IdClase, cancellationToken);

        Usuario user = await TaskUser;
        Clase clase = await TaskClase;

        if (user is null || clase is null)
        {
            throw new NotFoundException();
        }
        
        clase.AgregarConsulta(request.Titulo,request.Descripcion, user.Id);

        await this._unitOfWork.SaveChangesAsync();
    }
}