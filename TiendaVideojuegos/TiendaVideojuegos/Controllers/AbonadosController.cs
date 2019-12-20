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
            return View(await _context.Abonados.Include(b => b.ArticulosSegundaManoReventa).Include(b => b.Ventas).ToListAsync());
        }

        // GET: Abonados/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonados = await _context.Abonados.Include(b => b.ArticulosSegundaManoReventa).Include(b => b.Ventas)
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
                return NotFound("No existe el abonado a editar");
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
                        return NotFound("No existe el contexto abonados");
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
                return NotFound("Un id es necesario");
            }

            var abonados = await _context.Abonados
                .FirstOrDefaultAsync(m => m.IdAbonado == id);
            if (abonados == null)
            {
                return NotFound("no existe un abonado con ese id");
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

        // GET: Abonados/Devolver
        [HttpGet("Abonados/Devolver")]
        public async Task<IActionResult> Devolver()
        {
            var listaaux = await _context.Ventas.Include(b => b.Abonado).Include(b => b.Articulo).ToListAsync();

            var lista = new List<Ventas>();

            //Guid idProducto = new Guid();

            foreach(var item in listaaux)
            {
                if (item.IdAbonado == Services.UsuarioLogueado.Usuario.IdAbonado)
                {
                    lista.Add(item);
                    //idProducto = item.Articulo.IdProducto;
                }
            }

            //var devolverViewModel = new DevolverViewModel()
            //{
            //    nombreProducto = _context.Productos.FirstOrDefault(a => a.IdProducto == idProducto).Nombre,
            //    PrecioProducto = _context.Productos.FirstOrDefault(a => a.IdProducto == idProducto).Precio,
            //    ventas = lista,
            //};

            return View(lista);
        }

        // GET: Abonados/DevolverConcreto
        [HttpGet("Abonados/DevolverConcreto/{id?}")]
        public async Task<IActionResult> DevolverConcreto([FromRoute] Guid id)
        {
            var devolverArticuloViewModel = new DevolverArticuloViewModel()
            {
                IdUnidad = id,
            };

            return View(devolverArticuloViewModel);
        }


        // POST: Abonados/Devolver
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevolverArticulo([Bind("IdUnidad")] DevolverArticuloViewModel devolverArticuloViewModel)
        {
            Abonados usuario_logueado = Services.UsuarioLogueado.Usuario;
       
            if (usuario_logueado != null)
            {
                Ventas venta = _context.Ventas.Include(a => a.Articulo).Include(b => b.Abonado).FirstOrDefault(p => p.IdUnidad == devolverArticuloViewModel.IdUnidad);
                Articulos articuloDevolver = venta.Articulo;
                Productos producto = _context.Productos.FirstOrDefault(a => a.IdProducto == articuloDevolver.IdProducto);

                if (articuloDevolver != null)
                {

                    //se añade el artículo a nuestro sistema

                    Services.Caja.DineroTotal -= (producto.Precio);
                    if (Services.Caja.DineroTotal < 0)
                    {
                        return BadRequest("No hay suficiente dinero en caja");
                    }

                    articuloDevolver.Vendido = false;

                    // quitamos la venta de la lista de ventas (comprados) del Abonado
                    var abonado = _context.Abonados.Include(a => a.ArticulosSegundaManoReventa).Include(b => b.Ventas).FirstOrDefault(p => p.IdAbonado == usuario_logueado.IdAbonado);
                    abonado.Ventas.Remove(venta);
                    _context.Update(abonado);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest("Articulo a devolver no existe");
                }
                
            }

            return BadRequest("Es necesario que el abonado esté registrado para realizar esta operación");

        }

    }
}
