using Microsoft.AspNetCore.Mvc;
using PruebaBackendASEINFO.Models;
using PruebaBackendASEINFO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaBackendASEINFO.Utils;

namespace PruebaBackendASEINFO.Controllers
{
    public class RecuperacionController : Controller
    {
        private readonly TiendaASEINFOContext _context;

        public RecuperacionController(TiendaASEINFOContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ComenzarRecuperacion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ComenzarRecuperacion(CorreoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == model.Correo);
            if (usuario != null)
            {
                RecuperaContrasenia recupera = new RecuperaContrasenia();
                recupera.IdUsuario = usuario.IdUsuario;
                recupera.Token = GenerarToken();
                recupera.FechaModifica = DateTime.Now;
                recupera.IpModifica = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                _context.Add(recupera);
                await _context.SaveChangesAsync();

                // Enviamos correo
                UtilsMethods.EnviarCorreo(usuario, recupera.Token);
                ViewBag.Message = "Se ha enviado un correo con un link de recuperación";
            }
            return View();
        }

        public async Task<IActionResult> Recuperacion(string token)
        {
            RecuperacionViewModel model = new RecuperacionViewModel();
            model.Token = token;
            return View(model);
        }

        [HttpPost]
         public async Task<IActionResult> Recuperacion(RecuperacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.RecuperaContrasenias.Include(u => u.IdUsuarioNavigation)
                .FirstOrDefaultAsync(u => u.Token == model.Token);

            if(usuario != null)
            {
                if (usuario.IdUsuarioNavigation != null)
                {
                    Usuario user = usuario.IdUsuarioNavigation;
                    user.Salt = UtilsMethods.GenerarSalt();
                    user.Contrasenia = CryptoHelper.Crypto.HashPassword(model.Contrasenia + user.Salt);
                    user.FechaModifica = DateTime.Now;
                    user.IpModifica = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                    usuario.Token = "Expired";
                    _context.Update(usuario);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Contraseña modificada con éxito";
                    return View(nameof(Index));
                }
            } else
            {
                ViewBag.Error = "Token no válido";
                return View("Index");
            }

            return View(model);
        }

        private string GenerarToken()
        {
            int longitud = 10;
            Guid miGuid = Guid.NewGuid();
            string token = miGuid.ToString().Replace("-", string.Empty).Substring(0, longitud);
            token = CryptoHelper.Crypto.HashPassword(token);
            return token;
        }
    }
}
