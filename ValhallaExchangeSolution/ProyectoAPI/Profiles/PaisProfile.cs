using AutoMapper;
using Domain.Models;
using DTO;

namespace ProyectoAPI.Profiles
{
    public class PaisProfile : Profile
    {
        public PaisProfile() {
            CreateMap<Pais, PaisesDTO>();
        }
    }
}
