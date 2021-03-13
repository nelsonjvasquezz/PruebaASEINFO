using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaBackendASEINFO.Models;
using PruebaBackendASEINFO.ViewModels;
using CryptoHelper;
using PruebaBackendASEINFO.Utils;

namespace PruebaBackendASEINFO.Controllers
{
    public class RegistroController : Controller
    {
        private readonly TiendaASEINFOContext _context;

        public RegistroController(TiendaASEINFOContext context)
        {
            _context = context;
        }

        // GET: Registro
        public async Task<IActionResult> Index()
        {
            var tiendaASEINFOContext = _context.Usuarios.Include(u => u.IdRolNavigation);
            return View(await tiendaASEINFOContext.ToListAsync());
        }

        // GET: Registro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Registro/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IpModifica");
            return View();
        }

        // POST: Registro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = model.Username;
                usuario.Correo = model.Correo;
                usuario.Salt = UtilsMethods.GenerarSalt();
                usuario.Contrasenia = Crypto.HashPassword(model.Contrasenia + usuario.Salt);
                usuario.IdRol = 2;
                usuario.FechaModifica = DateTime.Now;
                usuario.IpModifica = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                int ultimoUsuario = _context.Usuarios.
                    OrderByDescending(u => u.IdUsuario).FirstOrDefault().IdUsuario;

                Cliente cliente = new Cliente();
                cliente.Nombre = model.Nombres;
                cliente.Apellidos = model.Apellidos;
                cliente.Direccion = model.Direccion;
                cliente.Genero = model.Genero;
                cliente.IdUsuario = ultimoUsuario;
                cliente.FechaModifica = DateTime.Now;
                cliente.IpModifica = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                _context.Add(cliente);
                await _context.SaveChangesAsync();

                UtilsMethods.EnviarCorreo(usuario);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Registro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IpModifica", usuario.IdRol);
            return View(usuario);
        }

        // POST: Registro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,NombreUsuario,Correo,Contrasenia,Imagen,IdRol,Habilitado,FechaModifica,IpModifica")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IpModifica", usuario.IdRol);
            return View(usuario);
        }

        // GET: Registro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Registro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }        
        
    }
}
