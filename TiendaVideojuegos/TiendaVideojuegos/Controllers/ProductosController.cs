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
            return View(await _context.Productos.Include(b => b.Articulos).ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.Include(b => b.Articulos)
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
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Precio,Descripcion,FechaLanzamiento,Plataforma,Imagen")] Productos productos)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProducto,Nombre,Precio,Descripcion,FechaLanzamiento,Plataforma,Imagen")] Productos productos)
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

        [HttpGet("Productos/ProductoSeleccionadoAdmin/{id?}")]
        public async Task<IActionResult> ProductoSeleccionadoAdmin([FromRoute] Guid id)
        {
            Productos producto = _context.Productos.Include(b => b.Articulos).FirstOrDefault(p => p.IdProducto == id);

            return View(producto);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComprarArticulosAProveedor([Bind("IdProducto,IdProveedor,Cantidad")] ComprarArticulosViewModel compra)
        {
            Productos producto = _context.Productos.Include(b => b.Articulos).FirstOrDefault(p => p.IdProducto == compra.IdProducto);
            Proveedores proveedor = _context.Proveedores.Include(b => b.ArticulosNuevosAbastecimiento).FirstOrDefault(p => p.IdProveedor == compra.IdProveedor);

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

                //actualizamos el valor del producto con el producto nuevo
                _context.Update(producto);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
            
        }

        // POST: Productos/ComprarArticuloNuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComprarArticuloNuevo([Bind("IdProducto")] Guid IdProducto)
        {
            Productos producto = _context.Productos.Include(b => b.Articulos).FirstOrDefault(p => p.IdProducto == IdProducto);
            Abonados usuario_logueado = Services.UsuarioLogueado.Usuario;
            if (producto != null && usuario_logueado != null)
            {
                Articulos articulo = _context.Articulos.Include(p => p.ArticuloNuevoAbastecimiento).Include(q => q.ArticuloSegundaManoReventa)
                    .Include(r => r.Producto).Include(s => s.Venta).FirstOrDefault(p => p.IdProducto == producto.IdProducto);

                if (articulo != null)
                {
                    ArticulosNuevosAbastecimiento articuloNuevo = articulo.ArticuloNuevoAbastecimiento;

                    if (articuloNuevo != null)
                    {
                        //se generan articulos auxiliares que insertaremos en la venta
                        Articulos articuloAux = new Articulos
                        {
                            Producto = producto,
                            IdProducto = producto.IdProducto,
                            IdUnidad = articulo.IdUnidad,
                            ArticuloNuevoAbastecimiento = null,
                            ArticuloSegundaManoReventa = null,
                            Venta = null
                        };

                        ArticulosNuevosAbastecimiento articuloNuevoAux = new ArticulosNuevosAbastecimiento
                        {
                            Articulo = articuloAux,
                            IdAbastecimiento = articuloNuevo.IdAbastecimiento,
                            IdProveedor = articuloNuevo.IdProveedor,
                            IdUnidad = articuloAux.IdUnidad,
                            Proveedor = articuloNuevo.Proveedor,
                        };

                        articuloAux.ArticuloNuevoAbastecimiento = articuloNuevoAux;
                        articuloNuevoAux.Articulo = articuloAux;
                        articuloAux.ArticuloNuevoAbastecimiento = articuloNuevoAux;

                        //se genera la venta del artículo y se elimina del almacén de artículos disponibles
                        Ventas venta = new Ventas
                        {
                            Abonado = usuario_logueado,
                            Articulo = articuloAux,
                            IdAbonado = usuario_logueado.IdAbonado,
                            IdUnidad = articuloAux.IdUnidad,
                            FechaVenta = DateTime.Now
                        };

                        articuloAux.Venta = venta;
                        venta.Articulo = articuloAux;

                        //una vez generada la venta se eliminan los artículos del sistema, se almacena la venta y se suma el dinero a la caja
                        _context.ArticulosNuevosAbastecimientos.Remove(articuloNuevo);
                        _context.Articulos.Remove(articulo);
                        _context.Ventas.Add(venta);

                        // añadimos la venta a la lista de ventas (comprados) del Abonado
                        var abonado = _context.Abonados.Include(b => b.Ventas).Include(b => b.ArticulosSegundaManoReventa).FirstOrDefault(p => p.IdAbonado == usuario_logueado.IdAbonado);
                        abonado.Ventas.Add(venta);
                        _context.Update(abonado);

                        await _context.SaveChangesAsync();

                        Services.Caja.DineroTotal += producto.Precio;

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();

        }

        // POST: Productos/ComprarArticuloSegundaMano
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComprarArticuloSegundaMano([Bind("IdProducto")] Guid IdProducto)
        {
            Productos producto = _context.Productos.Include(b => b.Articulos).FirstOrDefault(p => p.IdProducto == IdProducto);
            Abonados usuario_logueado = Services.UsuarioLogueado.Usuario;
            if (producto != null && usuario_logueado != null)
            {
                Articulos articulo = _context.Articulos.FirstOrDefault(p => p.IdProducto == producto.IdProducto);

                if (articulo != null)
                {
                    ArticulosSegundaManoReventa articuloSegundaMano = articulo.ArticuloSegundaManoReventa;

                    if (articuloSegundaMano != null)
                    {
                        //se generan articulos auxiliares que insertaremos en la venta
                        Articulos articuloAux = new Articulos
                        {
                            Producto = producto,
                            IdProducto = producto.IdProducto,
                            IdUnidad = articulo.IdUnidad,
                            ArticuloNuevoAbastecimiento = null,
                            ArticuloSegundaManoReventa = null,
                            Venta = null
                        };

                        ArticulosSegundaManoReventa articuloSegundaManoAux = new ArticulosSegundaManoReventa
                        {
                            Articulo = articuloAux,
                            Abonado = articuloSegundaMano.Abonado,
                            IdUnidad = articuloAux.IdUnidad,
                            IdAbonado = articuloSegundaMano.IdAbonado,
                            Estado = articuloSegundaMano.Estado,
                        };

                        articuloAux.ArticuloSegundaManoReventa= articuloSegundaManoAux;
                        articuloSegundaManoAux.Articulo = articuloAux;
                        articuloAux.ArticuloSegundaManoReventa = articuloSegundaManoAux;

                        //se genera la venta del artículo y se elimina del almacén de artículos disponibles
                        Ventas venta = new Ventas
                        {
                            Abonado = usuario_logueado,
                            Articulo = articuloAux,
                            IdAbonado = usuario_logueado.IdAbonado,
                            IdUnidad = articuloAux.IdUnidad,
                            FechaVenta = DateTime.Now
                        };

                        articuloAux.Venta = venta;
                        venta.Articulo = articuloAux;

                        //una vez generada la venta se eliminan los artículos del sistema, se almacena la venta y se suma el dinero a la caja
                        _context.ArticulosSegundaManoReventa.Remove(articuloSegundaManoAux);
                        _context.Articulos.Remove(articulo);
                        _context.Ventas.Add(venta);

                        // añadimos la venta a la lista de ventas (comprados) del Abonado
                        var abonado = _context.Abonados.Include(b => b.Ventas).Include(b => b.ArticulosSegundaManoReventa).FirstOrDefault(p => p.IdAbonado == usuario_logueado.IdAbonado);
                        abonado.Ventas.Add(venta);
                        _context.Update(abonado);

                        await _context.SaveChangesAsync();

                        Services.Caja.DineroTotal += producto.Precio;

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();

        }

        // GET: Productos/ComprarAbonado/id_producto
        [HttpGet("Productos/ComprarAbonado/{id?}")]
        public IActionResult ComprarAbonado([FromRoute] Guid id) // cambair a async si accede al repositorio
        {
            //var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id);

            var comprarArticuloAbonadoViewModel = new ComprarArticuloAbonadoViewModel()
            {
                IdProducto = id,
                Estado = ""
            };

            return View(comprarArticuloAbonadoViewModel);
        }

        // GET: Productos/ComprarArticuloNuevoGet/id_producto
        [HttpGet("Productos/ComprarArticuloNuevoGet/{id?}")]
        public IActionResult ComprarArticuloNuevoGet([FromRoute] Guid id) // cambair a async si accede al repositorio
        {
            var producto = _context.Productos.Include(p => p.Articulos).FirstOrDefault(p => p.IdProducto == id);

            return View(producto);
        }


        // GET: Productos/ComprarArticuloSegundaManoGet/id_producto
        [HttpGet("Productos/ComprarArticuloSegundaManoGet/{id?}")]
        public IActionResult ComprarArticuloSegundaManoGet([FromRoute] Guid id) // cambair a async si accede al repositorio
        {
            var producto = _context.Productos.Include(p => p.Articulos).FirstOrDefault(p => p.IdProducto == id);

            return View(producto);
        }

        //este es un dos en uno, crea el artículo que quiere vender el abonado, se inserta en su lista de Artículos que ha vendido, 
        //lo compramos y empieza a formar parte de los artículos de nuestro sistema.

        // POST: Productos/ComprarArticuloAbonado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComprarArticuloAbonado([Bind("IdProducto, Estado")] ComprarArticuloAbonadoViewModel compra)
        {
            Productos producto = _context.Productos.Include(p => p.Articulos).FirstOrDefault(p => p.IdProducto == compra.IdProducto);
            Abonados abonado = Services.UsuarioLogueado.Usuario;

            if (producto != null && abonado != null)
            {
                Articulos articulo = new Articulos
                {
                    Producto = producto,
                    IdProducto = producto.IdProducto,
                    IdUnidad = Guid.NewGuid(),
                    ArticuloNuevoAbastecimiento = null,
                    ArticuloSegundaManoReventa = null,
                    Venta = null
                };

                ArticulosSegundaManoReventa articulosSegundaManoReventa = new ArticulosSegundaManoReventa
                {
                    Articulo = articulo,
                    Estado = compra.Estado,
                    IdAbonado = abonado.IdAbonado,
                    IdUnidad = articulo.IdUnidad,
                    Abonado = abonado
                };

                articulo.ArticuloSegundaManoReventa = articulosSegundaManoReventa;
                articulosSegundaManoReventa.Articulo = articulo;
                articulo.ArticuloSegundaManoReventa = articulosSegundaManoReventa;

                Services.Caja.DineroTotal -= (producto.Precio);
                if (Services.Caja.DineroTotal < 0)
                {
                    return BadRequest();
                }


                //añadimos el artículo a los que ya tenemos
                await _context.Articulos.AddAsync(articulo);
                await _context.ArticulosSegundaManoReventa.AddAsync(articulosSegundaManoReventa);
                producto.Articulos.Add(articulo);
                _context.Update(producto);

                //añadimos el producto que ha vendido el abonado a su lista de articulos
                var abonadoAux = _context.Abonados.Include(b => b.Ventas).Include(b => b.ArticulosSegundaManoReventa).FirstOrDefault(p => p.IdAbonado == abonado.IdAbonado);
                abonadoAux.ArticulosSegundaManoReventa.Add(articulosSegundaManoReventa);
                _context.Update(abonadoAux);

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }

            return BadRequest();

        }



    }
}
