using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Application.AutoMapperModels;

public class EstudianteMappingProfile : Profile
{
    public EstudianteMappingProfile()
    {
        CreateMap<Estudiante, EstudianteDTO>()
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.CursosInscritosActualmente, opt => opt.Ignore())
            .ForMember(dest => dest.HistorialDeCursos, opt => opt.Ignore())
            .ReverseMap();
    }
}

