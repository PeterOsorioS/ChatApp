using Chat.DTOs.Modelos;
using Chat.Excepciones.Excepcion;
using Chat.Servicios.Interfaz;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/autenticacion")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        public AutenticacionController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost]
        [Route("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] InicioSesionDTO usuario)
        {
            try
            {
                var resultado = await _usuarioServicio.AutenticarUsuario(usuario);
                return Ok(resultado);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> Registrar([FromBody] CrearUsuarioDTO usuario)
        {
            try
            {
                var resultado = await _usuarioServicio.CrearUsuario(usuario);
                return Created(string.Empty, resultado);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
    }
}
