using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System;
using System.Threading.Tasks;
using _.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MCVTienda.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace MCVTienda.Controllers
{
    public class CompraController : Controller
    {
        TiendaCX ctx;
        Response res;
        public CompraController(TiendaCX _ctx)
        {
            ctx = _ctx;
            res = new Response();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Compras = await ctx.Compras.Include("DetalleCompras").Include(x => x.IdProveedorNavigation).ToListAsync();
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.Proveedores = await ctx.Proveedores.ToListAsync();
            ViewBag.Productos = await ctx.Productos.Select(x => new { x.IdProducto, Nombre = x.Nombre + "-" + x.Marca }).ToListAsync();

            Compra Compra = await ctx.Compras.FindAsync(id);
            Tuple<Compra, DetalleCompra> model = new Tuple<Compra, DetalleCompra>(Compra, new DetalleCompra());

            if (ModelState.IsValid)
            {
                return View("Formulario", model);
            }
            return View();
        }

        public IActionResult Detalles()
        {
            return View();
        }
        public async Task<IActionResult> Comprar()
        {
            ViewBag.Proveedores = await ctx.Proveedores.ToListAsync();
            ViewBag.Productos = await ctx.Productos.Select(x => new { x.IdProducto, Nombre = x.Nombre + "-" + x.Marca }).ToListAsync();
            Tuple<Compra, DetalleCompra> model = new Tuple<Compra, DetalleCompra>(new Compra(), new DetalleCompra());
            return View("Formulario", model);
        }

        public async Task<IActionResult> SetCompra([Bind(Prefix = "Item1")] Compra Compra)
        {

            if (ModelState.IsValid)
            {

                ctx.Compras.Add(Compra);
                ctx.Entry(Compra).State = EntityState.Added;

            }

            await ctx.SaveChangesAsync();
            res.Message = "Éxito en la compra";
            res.State = true;
            res.Data = Compra.IdCompra;
            return Json(res);
        }
        public async Task<IActionResult> ActualizarCompra([Bind(Prefix = "Item1")] Compra Compra)
        {

            if (ModelState.IsValid)
            {

                ctx.Compras.Update(Compra);
                ctx.Entry(Compra).State = EntityState.Modified;
            }

            await ctx.SaveChangesAsync();
            res.Message = "Éxito en la compra";
            res.State = true;
            res.Data = Compra.IdCompra;
            return Json(res);
        }
        public async Task<IActionResult> GetDetalles(int id)
        {

            List<DetalleCompra> Lista = await ctx.DetalleCompras.Include("IdProductoNavigation").Where(x => x.IdCompra == id).ToListAsync();

            return Json(Lista);
        }
        public async Task<IActionResult> SetDetalle([Bind(Prefix = "Item2")] DetalleCompra DetalleCompra)
        {
            if (!ModelState.IsValid)
            {
                res.Message = "No se pudo completar los DetalleCompra";
                res.State = false;
                res.Data = null;
                return Json(res);
            }
            ctx.DetalleCompras.Add(DetalleCompra);
            await ctx.SaveChangesAsync();
            res.Message = "Éxito en DetalleCompra";
            res.State = true;
            res.Data = "";
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> JSONPrueba(List<DetalleCompra> DetalleCompra)
        {
            var Prueba = DetalleCompra;
            foreach (var item in DetalleCompra)
            {
                if (ModelState.IsValid)
                {
                    ctx.DetalleCompras.Add(item);
                    await ctx.SaveChangesAsync();

                }
            }
            return Json(DetalleCompra);
        }
        private static void GetPropertyValues(Object obj)
        {
            Type t = obj.GetType();
            Console.WriteLine("Type is: {0}", t.Name);
            PropertyInfo[] props = t.GetProperties();
            Console.WriteLine("Properties (N = {0}):",
                              props.Length);
            foreach (var prop in props)
                if (prop.GetIndexParameters().Length == 0)
                    Console.WriteLine("   {0} ({1}): {2}", prop.Name,
                                      prop.PropertyType.Name,
                                      prop.GetValue(obj));
                else
                    Console.WriteLine("   {0} ({1}): <Indexed>", prop.Name,
                                      prop.PropertyType.Name);
        }
        public async Task<IActionResult> ActualizarDetalles(int id, List<DetalleCompra> DetalleCompra)
        {
            List<DetalleCompra> ViejaLista = await ctx.DetalleCompras.Where(x => x.IdCompra == id).ToListAsync();

            foreach (var item in ViejaLista)
            {

                ctx.DetalleCompras.Remove(item);
                await ctx.SaveChangesAsync();
            }

            foreach (var item in DetalleCompra)
            {
                GetPropertyValues(item);
                if (ModelState.IsValid)
                {
                    ctx.DetalleCompras.Add(item);
                    await ctx.SaveChangesAsync();

                }
                else
                {
                    Console.WriteLine("Else");
                }
            }
            return Json(DetalleCompra);
        }
    }
}