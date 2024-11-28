using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTOs.Modelos
{
    public class CrearUsuarioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "la contraseña es obligatorio")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; }
    }
}
