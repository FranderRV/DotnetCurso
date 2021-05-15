using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _.Models;
namespace MCVTienda.Controllers
{
    public class ClienteController : Controller
    {
        TiendaCX ctx;
        public ClienteController(TiendaCX _ctx)
        {
            ctx = _ctx;
        }

        public IActionResult Index()
        {
            ViewBag.Clientes = ctx.Clientes.ToList();

            return View();
        }
        public IActionResult Editar(int id)
        {
            var data = (Cliente)ctx.Clientes.FirstOrDefault(x => x.IdCliente == id);
            if (data == null)
            {
                return Redirect("/Cliente");
            }
            return View("Formulario",data);
        }
        
        public IActionResult Eliminar(int id)
        {
            var data = (Cliente)ctx.Clientes.FirstOrDefault(x => x.IdCliente == id);
            if (data != null)
            {
                ctx.Clientes.Remove(data);
                ctx.SaveChanges();
            }
            return Redirect("/Cliente");
        }

        [BindProperty]
        public Cliente Cliente { get; set; }
        public IActionResult Registrar()
        {
            if (ModelState.IsValid)
            {

                var data = ctx.Clientes.Where(x => x.IdCliente == Cliente.IdCliente).SingleOrDefault();

                if (data == null)
                {
                    ctx.Clientes.Add(Cliente);
                }
                else
                {
                    data.Nombre = Cliente.Nombre;
                    data.Dui = Cliente.Dui;
                    data.Nit = Cliente.Nit;
                    data.Correo = Cliente.Correo;
                }
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }
        public IActionResult ViewRegistrar()
        {
            return View("Formulario",new Cliente());
        }
    }
}