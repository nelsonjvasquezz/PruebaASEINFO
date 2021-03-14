using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PruebaBackendASEINFO.Models;

namespace PruebaBackendASEINFO.ViewModels
{
    public class RegistroViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Correo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Contraseña")]
        public String Contrasenia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public String Nombres { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public String Apellidos { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Dirección")]
        public String Direccion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Género")]
        public String Genero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Contrasenia), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmContra { get; set; }
        public string Imagen { get; set; }
    }
}
