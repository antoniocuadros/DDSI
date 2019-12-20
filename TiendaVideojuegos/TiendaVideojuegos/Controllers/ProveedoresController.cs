using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVideojuegos.Data;
using TiendaVideojuegos.Models;

namespace TiendaVideojuegos.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly AbonadosContext _context;

        public ProveedoresController(AbonadosContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.Include(p => p.ArticulosNuevosAbastecimiento).ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound("Id necesario");
            }

            var proveedores = await _context.Proveedores.Include(p => p.ArticulosNuevosAbastecimiento)
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedores == null)
            {
                return NotFound("No existe el proveedor con ese id");
            }

            return View(proveedores);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProveedor,Nombre,EnlaceCatalogo")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                proveedores.IdProveedor = Guid.NewGuid();
                _context.Add(proveedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound("id necesario");
            }

            var proveedores = await _context.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return NotFound("No existe el proveedor con ese id");
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProveedor,Nombre,EnlaceCatalogo")] Proveedores proveedores)
        {
            if (id != proveedores.IdProveedor)
            {
                return NotFound("No existe el proveedor con ese id");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoresExists(proveedores.IdProveedor))
                    {
                        return NotFound("No existe el contexto de proveedores en la base de datos");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound("id necesario");
            }

            var proveedores = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedores == null)
            {
                return NotFound("No existe el proveedor con ese id");
            }

            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var proveedores = await _context.Proveedores.FindAsync(id);
            _context.Proveedores.Remove(proveedores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoresExists(Guid id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }
}
