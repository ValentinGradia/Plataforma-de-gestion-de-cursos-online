using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Application.AutoMapperModels;

public class InscripcionMappingProfile : Profile
{
    public InscripcionMappingProfile()
    {
        CreateMap<Inscripcion, InscripcionDTO>()
            .ForMember(dest => dest.IdInscripcion, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IdCurso, opt => opt.MapFrom(src => src.IdCurso))
            .ForMember(dest => dest.FechaInscripcion, opt => opt.MapFrom(src => src.FechaInscripcion))
            .ForMember(dest => dest.Activa, opt => opt.MapFrom(src => src.Activa))
            .ReverseMap();
    }
}

