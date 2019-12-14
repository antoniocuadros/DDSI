﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TiendaVideojuegos.Data;
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
        public async Task<IActionResult> CreateProduct(LoginViewModel loginViewModel)
        {
            var usuario = _context.Abonados.FirstOrDefault(p => p.DNI == loginViewModel.DNI);

            if (usuario == null)
            {
                return NotFound();
            }
            
            if(usuario.Contraseña != loginViewModel.contraseña)
            {
                return NotFound();
            }

            usuario.Logueado = true;

            var originalEntity = _context.Abonados.Find(usuario.Id);
            _context.Entry(originalEntity).CurrentValues.SetValues(usuario);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}