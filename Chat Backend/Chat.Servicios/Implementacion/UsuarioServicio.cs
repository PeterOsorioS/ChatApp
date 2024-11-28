using AutoMapper;
using Chat.Datos.Data.Repositorios.IRepositorios;
using Chat.DTOs.Modelos;
using Chat.Entidades.Modelos;
using Chat.Excepciones.Excepcion;
using Chat.Servicios.Interfaz;
using Chat.Servicios.Utilidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Chat.Servicios.Implementacion
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly JwtUtilidad _Token;
        private readonly IMapper _mapper;
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UsuarioServicio(JwtUtilidad Token, IMapper mapper,
            IContenedorTrabajo contenedorTrabajo,
            IPasswordHasher<Usuario> passwordHasher
            )
        {
            _Token = Token;
            _mapper = mapper;
            _contenedorTrabajo = contenedorTrabajo;
            _passwordHasher = passwordHasher;
        }



        public async Task<string> AutenticarUsuario(InicioSesionDTO UsuarioDTO)
        {

            var usuario = await _contenedorTrabajo.Usuario.GetByEmailAsync(UsuarioDTO.Correo); 

            var verificacionClave = _passwordHasher.VerifyHashedPassword(usuario, usuario.Clave, UsuarioDTO.Clave);
            if (usuario == null || 
                verificacionClave == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Error en las credenciales del usuario.");
            }

            return _Token.ObtenerToken(usuario);
        }

        public async Task<string> CrearUsuario(CrearUsuarioDTO UsuarioDTO)
        {
            var usuarioExiste = await _contenedorTrabajo.Usuario.GetFirstOrDefaultAsync(
                u => u.NombreUsuario == UsuarioDTO.NombreUsuario || u.Correo == UsuarioDTO.Correo);

            if (usuarioExiste != null)
            {
                string error = usuarioExiste.NombreUsuario == UsuarioDTO.NombreUsuario
                    ? "nombre de usuario" : "correo";
                throw new BadRequestException($"Ya existe un usuario con ese {error}.");
            }

            // Mapear el DTO al modelo de la base de datos
            var nuevoUsuario = _mapper.Map<Usuario>(UsuarioDTO);
            nuevoUsuario.Clave = _passwordHasher.HashPassword(nuevoUsuario, UsuarioDTO.Clave);

            await _contenedorTrabajo.Usuario.AddAsync(nuevoUsuario);
            await _contenedorTrabajo.SaveAsync();

            return _Token.ObtenerToken(nuevoUsuario);
        }

        public IEnumerable<UsuarioDTO> ListaUsuarios()
        {
            var usuarios = _contenedorTrabajo.Usuario.GetAll();
            if (usuarios == null)
            {
                throw new BadRequestException("Error no existen usuarios.");
            }
            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return usuariosDTO;
        }

        public UsuarioDTO ObtenerUsuarioPorID(int id)
        {
            var usuario = _contenedorTrabajo.Usuario.GetById(id);
            if (usuario == null)
            {
                throw new BadRequestException("Error no existe el usuario");
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return usuarioDTO;
        }

        public UsuarioDTO ObtenerUsuarioPorCorreo(string correo)
        {
            var usuario = _contenedorTrabajo.Usuario.GetByEmail(correo);
            if (usuario == null)
            {
                throw new BadRequestException("Error no existe el usuario");
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return usuarioDTO;
        }

        public async Task EliminarPorId(int id)
        {

            var usuario = _contenedorTrabajo.Usuario.GetById(id);
            if (usuario == null)
            {
                throw new BadRequestException("Error no existe el usuario");
            }
            _contenedorTrabajo.Usuario.Remove(usuario);
            await _contenedorTrabajo.SaveAsync();
        }
    }
}
