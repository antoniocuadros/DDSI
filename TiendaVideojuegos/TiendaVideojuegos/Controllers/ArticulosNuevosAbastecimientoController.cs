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
    public class ArticulosNuevosAbastecimientoController : Controller
    {
        private readonly AbonadosContext _context;

        public ArticulosNuevosAbastecimientoController(AbonadosContext context)
        {
            _context = context;
        }

        // GET: ArticulosNuevosAbastecimientoes
        public async Task<IActionResult> Index()
        {
            var abonadosContext = _context.ArticulosNuevosAbastecimientos.Include(a => a.Articulo).Include(a => a.Proveedor);
            return View(await abonadosContext.ToListAsync());
        }

        // GET: ArticulosNuevosAbastecimientoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulosNuevosAbastecimiento = await _context.ArticulosNuevosAbastecimientos
                .Include(a => a.Articulo)
                .Include(a => a.Proveedor)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (articulosNuevosAbastecimiento == null)
            {
                return NotFound();
            }

            return View(articulosNuevosAbastecimiento);
        }

        // GET: ArticulosNuevosAbastecimientoes/Create
        public IActionResult Create()
        {
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor");
            return View();
        }

        // POST: ArticulosNuevosAbastecimientoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUnidad,IdAbastecimiento,IdProveedor")] ArticulosNuevosAbastecimiento articulosNuevosAbastecimiento)
        {
            if (ModelState.IsValid)
            {
                articulosNuevosAbastecimiento.IdUnidad = Guid.NewGuid();
                _context.Add(articulosNuevosAbastecimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad", articulosNuevosAbastecimiento.IdUnidad);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", articulosNuevosAbastecimiento.IdProveedor);
            return View(articulosNuevosAbastecimiento);
        }

        // GET: ArticulosNuevosAbastecimientoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulosNuevosAbastecimiento = await _context.ArticulosNuevosAbastecimientos.FindAsync(id);
            if (articulosNuevosAbastecimiento == null)
            {
                return NotFound();
            }
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad", articulosNuevosAbastecimiento.IdUnidad);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", articulosNuevosAbastecimiento.IdProveedor);
            return View(articulosNuevosAbastecimiento);
        }

        // POST: ArticulosNuevosAbastecimientoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdUnidad,IdAbastecimiento,IdProveedor")] ArticulosNuevosAbastecimiento articulosNuevosAbastecimiento)
        {
            if (id != articulosNuevosAbastecimiento.IdUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulosNuevosAbastecimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticulosNuevosAbastecimientoExists(articulosNuevosAbastecimiento.IdUnidad))
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
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad", articulosNuevosAbastecimiento.IdUnidad);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", articulosNuevosAbastecimiento.IdProveedor);
            return View(articulosNuevosAbastecimiento);
        }

        // GET: ArticulosNuevosAbastecimientoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulosNuevosAbastecimiento = await _context.ArticulosNuevosAbastecimientos
                .Include(a => a.Articulo)
                .Include(a => a.Proveedor)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (articulosNuevosAbastecimiento == null)
            {
                return NotFound();
            }

            return View(articulosNuevosAbastecimiento);
        }

        // POST: ArticulosNuevosAbastecimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var articulosNuevosAbastecimiento = await _context.ArticulosNuevosAbastecimientos.FindAsync(id);
            _context.ArticulosNuevosAbastecimientos.Remove(articulosNuevosAbastecimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticulosNuevosAbastecimientoExists(Guid id)
        {
            return _context.ArticulosNuevosAbastecimientos.Any(e => e.IdUnidad == id);
        }
    }
}
