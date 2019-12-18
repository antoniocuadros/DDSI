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
    public class VentasController : Controller
    {
        private readonly AbonadosContext _context;

        public VentasController(AbonadosContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var abonadosContext = _context.Ventas.Include(v => v.Abonado).Include(v => v.Articulo);
            return View(await abonadosContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .Include(v => v.Abonado)
                .Include(v => v.Articulo)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdAbonado"] = new SelectList(_context.Abonados, "IdAbonado", "IdAbonado");
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUnidad,IdAbonado,FechaVenta")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                ventas.IdUnidad = Guid.NewGuid();
                _context.Add(ventas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAbonado"] = new SelectList(_context.Abonados, "IdAbonado", "IdAbonado", ventas.IdAbonado);
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad", ventas.IdUnidad);
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas == null)
            {
                return NotFound();
            }
            ViewData["IdAbonado"] = new SelectList(_context.Abonados, "IdAbonado", "IdAbonado", ventas.IdAbonado);
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad", ventas.IdUnidad);
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdUnidad,IdAbonado,FechaVenta")] Ventas ventas)
        {
            if (id != ventas.IdUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasExists(ventas.IdUnidad))
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
            ViewData["IdAbonado"] = new SelectList(_context.Abonados, "IdAbonado", "IdAbonado", ventas.IdAbonado);
            ViewData["IdUnidad"] = new SelectList(_context.Articulos, "IdUnidad", "IdUnidad", ventas.IdUnidad);
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .Include(v => v.Abonado)
                .Include(v => v.Articulo)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ventas = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(ventas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasExists(Guid id)
        {
            return _context.Ventas.Any(e => e.IdUnidad == id);
        }
    }
}
