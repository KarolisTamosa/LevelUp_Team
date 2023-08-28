using AutoMapper;
using Domain.Models;
using DTO.Historial;

namespace ProyectoAPI.Profiles
{
    public class HistorialProfile : Profile
    {
        public HistorialProfile()
        {
            CreateMap<Historial, HistorialGetDTO>()
                .ForMember(dto => dto.CodigoMonedaOrigen, ent =>
                ent.MapFrom(val => $"{val.MonedaOrigen.Codigo}"))
                .ForMember(dto => dto.ValorMonedaOrigen, ent =>
                ent.MapFrom(val => $"{val.MonedaOrigen.ValorEnDolares}"))
                .ForMember(dto => dto.CodigoMonedaDestino, ent =>
                ent.MapFrom(val => $"{val.MonedaDestino.Codigo}"))
                .ForMember(dto => dto.ValorMonedaDestino, ent =>
                ent.MapFrom(val => $"{val.MonedaDestino.ValorEnDolares}"));


            //CreateMap<ApiMonedasDTO, Moneda>()
            //    .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}")); ;


        }

    }
}
