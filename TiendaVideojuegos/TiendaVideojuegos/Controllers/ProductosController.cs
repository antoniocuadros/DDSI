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
    public class ProductosController : Controller
    {
        private readonly AbonadosContext _context;

        public ProductosController(AbonadosContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Precio,Descripcion,FechaLanzamiento,Plataforma")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.IdProducto = Guid.NewGuid();
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProducto,Nombre,Precio,Descripcion,FechaLanzamiento,Plataforma")] Productos productos)
        {
            if (id != productos.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.IdProducto))
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
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(Guid id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
        
        // GET: Productos/Comprar/id_producto
        [HttpGet("Productos/Comprar/{id?}")]
        public async Task<IActionResult> Comprar([FromRoute] Guid id)
        {
            //var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id);

            var comprarArticulosViewModel = new ComprarArticulosViewModel()
            {
                Cantidad = 0,
                IdProducto = id,
                IdProveedor = Guid.Empty,
            };

            return View(comprarArticulosViewModel);
        }

        // POST: Productos/ComprarArticulosAProveedor
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComprarArticulosAProveedor([Bind("IdProducto,IdProveedor,Cantidad")] ComprarArticulosViewModel compra)
        {
            Productos producto = _context.Productos.FirstOrDefault(p => p.IdProducto == compra.IdProducto);
            Proveedores proveedor = _context.Proveedores.FirstOrDefault(p => p.IdProveedor == compra.IdProveedor);

            if (producto != null && proveedor != null)
            {
                Articulos articulo = new Articulos { 
                    Producto = producto, 
                    IdProducto = producto.IdProducto, 
                    IdUnidad = Guid.NewGuid(), 
                    ArticuloNuevoAbastecimiento = null, 
                    ArticuloSegundaManoReventa = null, 
                    Venta = null 
                };

                ArticulosNuevosAbastecimiento articulosNuevosAbastecimiento = new ArticulosNuevosAbastecimiento
                {
                    Articulo = articulo,
                    IdAbastecimiento = Guid.NewGuid(),
                    IdProveedor = proveedor.IdProveedor,
                    IdUnidad = articulo.IdUnidad,
                    Proveedor = proveedor
                };

                articulo.ArticuloNuevoAbastecimiento = articulosNuevosAbastecimiento;
                articulosNuevosAbastecimiento.Articulo = articulo;
                articulo.ArticuloNuevoAbastecimiento = articulosNuevosAbastecimiento;

                Services.Caja.DineroTotal -= (producto.Precio) * compra.Cantidad;
                if (Services.Caja.DineroTotal < 0)
                {
                    return BadRequest();
                }

                await _context.Articulos.AddAsync(articulo);
                await _context.ArticulosNuevosAbastecimientos.AddAsync(articulosNuevosAbastecimiento);
                await _context.SaveChangesAsync();
                

                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
            
        }
    }
}
