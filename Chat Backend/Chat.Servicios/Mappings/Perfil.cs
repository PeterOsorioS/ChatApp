using AutoMapper;
using Chat.DTOs.Modelos;
using Chat.Entidades.Modelos;

namespace Chat.Servicios.Mappings
{
    public class Perfil : Profile
    {
        public Perfil()
        {
            CreateMap<CrearUsuarioDTO, Usuario>()
                .ForMember(destino => destino.Clave, origen => origen.MapFrom(c => c.Clave))
                .ForSourceMember(c => c.ConfirmarClave, opcion => opcion.DoNotValidate());

            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO,Usuario>();
        }
    }
}
