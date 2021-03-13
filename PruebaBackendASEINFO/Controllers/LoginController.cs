using Microsoft.AspNetCore.Mvc;
using PruebaBackendASEINFO.Models;
using PruebaBackendASEINFO.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoHelper;

namespace PruebaBackendASEINFO.Controllers
{
    public class LoginController : Controller
    {
        private readonly TiendaASEINFOContext _context;

        public LoginController(TiendaASEINFOContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", login);
            }
            var usuario = await _context.Usuarios.Include(r => r.IdRolNavigation).
                FirstOrDefaultAsync(u => u.Correo == login.Correo);
            if(usuario != null)
            {
                string hashed = Crypto.HashPassword(login.Contrasenia + usuario.Salt);
                bool correcto = Crypto.VerifyHashedPassword(usuario.Contrasenia, login.Contrasenia + usuario.Salt);
                if(correcto)
                {

                } else
                {
                    ViewBag.Error = "La contraseña es incorrecta";
                    return View("Login",login);
                }
            } else
            {
                ViewBag.Error = "No se encontró el usuario";
                return View("Login", login);
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
