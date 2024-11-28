using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chat.Middleware
{
    public class TokenHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        public TokenHandlerMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _config["Jwt:Audience"],
                    }, out SecurityToken validatedToken);

                    context.User = claims;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }
            }
            await _next(context);
        }
    }
}
