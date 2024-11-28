using Chat.DTOs.Modelos;

namespace Chat.Servicios.Interfaz
{
    public interface IUsuarioServicio
    {
        public Task<string> AutenticarUsuario(InicioSesionDTO inicioSesionDTO);
        public Task<string> CrearUsuario(CrearUsuarioDTO crearUsuarioDTO);
        public IEnumerable<UsuarioDTO> ListaUsuarios();
        public UsuarioDTO ObtenerUsuarioPorID(int id);
        public UsuarioDTO ObtenerUsuarioPorCorreo(string correo);
        public Task EliminarPorId(int id);
    }
}
