using Chat.Datos.Data.Repositorios.IRepositorios;
using Chat.Entidades.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Chat.Servicios.Utilidades
{
    public class JwtUtilidad
    {
        private readonly IConfiguration _configuration;
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public JwtUtilidad(IConfiguration configuration, IContenedorTrabajo contenedorTrabajo)
        {
            _configuration = configuration;
            _contenedorTrabajo = contenedorTrabajo;
        }

        public string ObtenerToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nombre),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", usuario.Id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
