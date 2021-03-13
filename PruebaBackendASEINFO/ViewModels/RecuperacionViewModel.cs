using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaBackendASEINFO.ViewModels
{
    public class RecuperacionViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Contrasenia), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmContra { get; set; }

        public string Token { get; set; }
    }
}
