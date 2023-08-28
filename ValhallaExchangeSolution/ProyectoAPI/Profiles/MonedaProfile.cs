using AutoMapper;
using Domain.Models;
using DTO;

namespace ProyectoAPI.Profiles
{
    public class MonedaProfile: Profile
    {
        public MonedaProfile() {
            CreateMap<Moneda, MonedaDTO>();
        }
    }
}
