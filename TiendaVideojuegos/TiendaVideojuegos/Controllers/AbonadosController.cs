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
    public class AbonadosController : Controller
    {
        private readonly AbonadosContext _context;

        public AbonadosController(AbonadosContext context)
        {
            _context = context;
        }

        // GET: Abonados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abonados.ToListAsync());
        }

        // GET: Abonados/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonados = await _context.Abonados
                .FirstOrDefaultAsync(m => m.IdAbonado == id);
            if (abonados == null)
            {
                return NotFound();
            }

            return View(abonados);
        }

        // GET: Abonados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abonados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAbonado,Nombre,DNI,Contraseña,Telefono,e_mail,Logueado,Direccion")] Abonados abonados)
        {
            if (ModelState.IsValid)
            {
                abonados.IdAbonado = Guid.NewGuid();
                abonados.Logueado = false;
                _context.Add(abonados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abonados);
        }

        // GET: Abonados/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonados = await _context.Abonados.FindAsync(id);
            if (abonados == null)
            {
                return NotFound();
            }
            return View(abonados);
        }

        // POST: Abonados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdAbonado,Nombre,DNI,Contraseña,Telefono,e_mail,Direccion,Logueado")] Abonados abonados)
        {
            if (id != abonados.IdAbonado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonadosExists(abonados.IdAbonado))
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
            return View(abonados);
        }

        // GET: Abonados/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonados = await _context.Abonados
                .FirstOrDefaultAsync(m => m.IdAbonado == id);
            if (abonados == null)
            {
                return NotFound();
            }

            return View(abonados);
        }

        // POST: Abonados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var abonados = await _context.Abonados.FindAsync(id);
            _context.Abonados.Remove(abonados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonadosExists(Guid id)
        {
            return _context.Abonados.Any(e => e.IdAbonado == id);
        }
    }
}
