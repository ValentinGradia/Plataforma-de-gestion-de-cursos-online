using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.AutoMapperModels;

public class CursoMappingProfile : Profile
{
    public CursoMappingProfile()
    {
        CreateMap<Curso, CursoDTO>()
            .ForMember(dest => dest.IdCurso, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IdProfesor, opt => opt.MapFrom(src => src.IdProfesor))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Duracion, opt => opt.MapFrom(src => src.Duracion))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Temario, opt => opt.MapFrom(src => src.Temario))
            .ReverseMap();
    }
}

