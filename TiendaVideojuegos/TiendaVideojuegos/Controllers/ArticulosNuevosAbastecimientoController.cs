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
