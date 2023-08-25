using AutoMapper;
using Domain.Models;
using DTO.Historial;
using DTO.Usuario;
using Utils;

namespace ProyectoAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dto => dto.IdUsuario, ent =>
                ent.MapFrom(val => $"{val.IdUsuario}"))
                .ForMember(dto => dto.Email, ent =>
                ent.MapFrom(val => $"{val.Email}"))
                .ForMember(dto => dto.IdPais, ent =>
                ent.MapFrom(val => $"{val.IdPais}"))
                .ForMember(dto => dto.Edad, ent =>
                ent.MapFrom(val => UsuarioUtils.CalcularEdad(val.FechaNacimiento)));
            
            CreateMap<UsuarioForCreationDTO, Usuario>().
                ForMember(model => model.PasswordEncriptado, ent =>
                ent.MapFrom(dto => dto.Password));

            CreateMap<UsuarioForLoginDTO, Usuario>().
                ForMember(model => model.PasswordEncriptado, ent =>
                ent.MapFrom(dto => dto.Password));

            CreateMap<Usuario, UsuarioParaActualizarDTO>()
                .ForMember(dto => dto.Password, ent =>
                ent.MapFrom(model => model.PasswordEncriptado))
                .ForMember(dto => dto.Email, ent =>
                ent.MapFrom(model => model.Email));

        }
    }
}
