using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaBackendASEINFO.Models;

namespace PruebaBackendASEINFO.Controllers
{
    public class ProductoController : Controller
    {
        private readonly TiendaASEINFOContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductoController(TiendaASEINFOContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var tiendaASEINFOContext = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdMarcaNavigation).Include(p => p.IdTipoNavigation);
            return View(await tiendaASEINFOContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Nombre");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Nombre");
            ViewData["IdTipo"] = new SelectList(_context.Tipos, "IdTipo", "Nombre");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,Precio,Stock,ImageFile,IdMarca,IdCategoria,IdTipo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Agregamos la imagen a la carpeta Image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(producto.ImageFile.FileName);
                string extension = Path.GetExtension(producto.ImageFile.FileName);
                producto.Imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await producto.ImageFile.CopyToAsync(fileStream);
                }

                producto.FechaModifica = DateTime.Now;
                producto.IpModifica = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Nombre", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Nombre", producto.IdMarca);
            ViewData["IdTipo"] = new SelectList(_context.Tipos, "IdTipo", "Nombre", producto.IdTipo);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IpModifica", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IpModifica", producto.IdMarca);
            ViewData["IdTipo"] = new SelectList(_context.Tipos, "IdTipo", "IpModifica", producto.IdTipo);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,Precio,Stock,Imagen,IdMarca,IdCategoria,IdTipo,Habilitado,FechaModifica,IpModifica")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IpModifica", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IpModifica", producto.IdMarca);
            ViewData["IdTipo"] = new SelectList(_context.Tipos, "IdTipo", "IpModifica", producto.IdTipo);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
