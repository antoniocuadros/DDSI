using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVideojuegos.Data;
using TiendaVideojuegos.Models;
using TiendaVideojuegos.ViewModels;

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

        // POST: Productos/ComprarArticuloAbonado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevolverArticulo([Bind("IdUnidad, Estado")] DevolverArticuloViewModel devolverArticuloViewModel)
        {
            Abonados usuario_logueado = Services.UsuarioLogueado.Usuario;
       
            if (usuario_logueado != null)
            {
                Ventas venta = usuario_logueado.Ventas.FirstOrDefault(p => p.IdUnidad == devolverArticuloViewModel.IdUnidad);
                Articulos articuloDevolver = venta.Articulo;

                if (articuloDevolver != null)
                {
                    //se generan articulos auxiliares que insertaremos en la venta
                    Articulos articuloAux = new Articulos
                    {
                        Producto = articuloDevolver.Producto,
                        IdProducto = articuloDevolver.IdProducto,
                        IdUnidad = articuloDevolver.IdUnidad,
                        ArticuloNuevoAbastecimiento = null,
                        ArticuloSegundaManoReventa = null,
                        Venta = null
                    };

                    ArticulosSegundaManoReventa articuloSegundaManoAux = new ArticulosSegundaManoReventa
                    {
                        Articulo = articuloAux,
                        Abonado = usuario_logueado,
                        IdUnidad = articuloAux.IdUnidad,
                        IdAbonado = usuario_logueado.IdAbonado,
                        Estado = devolverArticuloViewModel.Estado,
                    };

                    articuloAux.ArticuloSegundaManoReventa = articuloSegundaManoAux;
                    articuloSegundaManoAux.Articulo = articuloAux;
                    articuloAux.ArticuloSegundaManoReventa = articuloSegundaManoAux;

                    //se añade el artículo a nuestro sistema

                    Services.Caja.DineroTotal -= (articuloAux.Producto.Precio);
                    if (Services.Caja.DineroTotal < 0)
                    {
                        return BadRequest();
                    }

                    await _context.Articulos.AddAsync(articuloAux);
                    await _context.ArticulosSegundaManoReventa.AddAsync(articuloSegundaManoAux);

                    var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == articuloAux.IdProducto);
                    producto.Articulos.Add(articuloAux);
                    _context.Update(producto);

                    // quitamos la venta de la lista de ventas (comprados) del Abonado
                    var abonado = _context.Abonados.FirstOrDefault(p => p.IdAbonado == usuario_logueado.IdAbonado);
                    abonado.Ventas.Remove(venta);
                    _context.Update(abonado);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest();
                }
                
            }

            return BadRequest();

        }

    }
}
