using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaBackendASEINFO.ViewModels
{
    public class CorreoViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Correo { get; set; }
        
    }
}
