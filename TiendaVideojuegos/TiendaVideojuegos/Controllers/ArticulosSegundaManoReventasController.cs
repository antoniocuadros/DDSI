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
    public class ArticulosSegundaManoReventasController : Controller
    {
        private readonly AbonadosContext _context;

        public ArticulosSegundaManoReventasController(AbonadosContext context)
        {
            _context = context;
        }

        // GET: ArticulosSegundaManoReventas
        public async Task<IActionResult> Index()
        {
            var abonadosContext = _context.ArticulosSegundaManoReventa.Include(a => a.Abonado).Include(a => a.Articulo);
            return View(await abonadosContext.ToListAsync());
        }

        // GET: ArticulosSegundaManoReventas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulosSegundaManoReventa = await _context.ArticulosSegundaManoReventa
                .Include(a => a.Abonado)
                .Include(a => a.Articulo)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (articulosSegundaManoReventa == null)
            {
                return NotFound();
            }

            return View(articulosSegundaManoReventa);
        }

        // GET: ArticulosSegundaManoReventas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulosSegundaManoReventa = await _context.ArticulosSegundaManoReventa.FindAsync(id);
            if (articulosSegundaManoReventa == null)
            {
                return NotFound();
            }

            return View(articulosSegundaManoReventa);
        }

        // POST: ArticulosSegundaManoReventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdUnidad, Estado")] ArticulosSegundaManoReventa articulosSegundaManoReventa)
        {
            if (id != articulosSegundaManoReventa.IdUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulosSegundaManoReventa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticulosSegundaManoReventaExists(articulosSegundaManoReventa.IdUnidad))
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
            
            return View(articulosSegundaManoReventa);
        }

        // GET: ArticulosSegundaManoReventas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulosSegundaManoReventa = await _context.ArticulosSegundaManoReventa
                .Include(a => a.Abonado)
                .Include(a => a.Articulo)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (articulosSegundaManoReventa == null)
            {
                return NotFound();
            }

            return View(articulosSegundaManoReventa);
        }

        // POST: ArticulosSegundaManoReventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var articulosSegundaManoReventa = await _context.ArticulosSegundaManoReventa.FindAsync(id);
            _context.ArticulosSegundaManoReventa.Remove(articulosSegundaManoReventa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticulosSegundaManoReventaExists(Guid id)
        {
            return _context.ArticulosSegundaManoReventa.Any(e => e.IdUnidad == id);
        }
    }
}
