using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVideojuegos.Data;
using TiendaVideojuegos.Models;
using TiendaVideojuegos.ViewModels;

namespace TiendaVideojuegos.Controllers
{
    public class LoginController : Controller
    {
        private readonly AbonadosContext _context;

        public LoginController(AbonadosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser([Bind("DNI,Contraseña")] Abonados loginViewModel)
        {
            var usuario = _context.Abonados.Include(b => b.Ventas).Include(b => b.ArticulosSegundaManoReventa).FirstOrDefault(p => p.DNI == loginViewModel.DNI);
            
            if (usuario == null)
            {
                return NotFound();
            }
            
            if(usuario.Contraseña != loginViewModel.Contraseña)
            {
                return NotFound();
            }

            usuario.Logueado = true;

            var originalEntity = _context.Abonados.Find(usuario.IdAbonado);
            _context.Entry(originalEntity).CurrentValues.SetValues(usuario);

            await _context.SaveChangesAsync();

            Services.UsuarioLogueado.Usuario = usuario;

            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> LogoutUser()
        {
            var usuario = Services.UsuarioLogueado.Usuario;

            usuario.Logueado = false;

            var originalEntity = _context.Abonados.Find(usuario.IdAbonado);
            _context.Entry(originalEntity).CurrentValues.SetValues(usuario);

            await _context.SaveChangesAsync();

            Services.UsuarioLogueado.Usuario = null;

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}