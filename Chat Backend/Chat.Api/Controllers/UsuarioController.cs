using Chat.DTOs.Modelos;
using Chat.Excepciones.Excepcion;
using Chat.Servicios.Implementacion;
using Chat.Servicios.Interfaz;
using Chat.Servicios.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            try
            {
                var respuesta = _usuarioServicio.ListaUsuarios();
                return Ok(respuesta);
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("{correo}")]
        public IActionResult ObtenerUsuarioPorCorreo(string correo)
        {
            try
            {
                var respuesta = _usuarioServicio.ObtenerUsuarioPorCorreo(correo);
                return Ok(respuesta);
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult ObtenerUsuarioPorId(int id)
        {
            try
            {
                var respuesta = _usuarioServicio.ObtenerUsuarioPorID(id);
                return Ok(respuesta);
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult ActualizarUsuario([FromBody] UsuarioDTO usuario, int id)
        {
            return null;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _usuarioServicio.EliminarPorId(id);
                return Ok("Usuario eliminado.");
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
