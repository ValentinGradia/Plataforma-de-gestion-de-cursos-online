using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Application.AutoMapperModels;

public class ProfesorMappingProfile : Profile
{
    public ProfesorMappingProfile()
    {
        CreateMap<Profesor, ProfesorDTO>()
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.CursosACargo, opt => opt.Ignore())
            .ForMember(dest => dest.Especialidad, opt => opt.MapFrom(src => src.Especialidad))
            .ReverseMap();
    }
}

