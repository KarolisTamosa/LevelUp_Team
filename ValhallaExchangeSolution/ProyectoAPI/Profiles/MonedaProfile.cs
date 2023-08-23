using AutoMapper;
using Domain.Models;
using DTO;

namespace ProyectoAPI.Profiles
{
    public class MonedaProfile: Profile
    {
        public MonedaProfile() {
            CreateMap<MonedaDTO, Moneda>();
            
            
            //CreateMap<ApiMonedasDTO, Moneda>()
            //    .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}")); ;


        }
    }
}
