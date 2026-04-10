using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;

namespace PlataformaDeGestionDeCursosOnline.Application.AutoMapperModels;
using AutoMapper;

public class ClaseMappingProfile : Profile
{
    public ClaseMappingProfile()
    {
        CreateMap<Clase,ClaseDTO>()
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material))
            .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ReverseMap();
    }
}