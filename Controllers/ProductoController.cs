using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCVTienda.Controllers
{
    public class ProductoController : Controller
    {
        TiendaCX ctx;
        public ProductoController(TiendaCX _ctx)
        {
            ctx = _ctx;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.lista = await ctx.Productos.Include(x => x.IdCategoriaNavigation)
                                                .ToListAsync();

            return View();
        }

        public async Task<IActionResult> ViewRegistro()
        {
            ViewBag.Categorias = await ctx.Categorias.OrderBy(x => x.Nombre).ToListAsync();
            return View("Formulario", new Producto());
        }

        public async Task<IActionResult> ViewEditar(int id)
        {
            var data = await ctx.Productos.FindAsync(id);
            if (data != null)
            {
                ViewBag.Categorias = await ctx.Categorias.OrderBy(x => x.Nombre).ToListAsync();
                return View("Formulario", data);
            }
            return RedirectToAction("Index");
        }

        [BindProperty]
        public Producto Producto { get; set; }
        public async Task<IActionResult> Editar()
        {

            var data = await ctx.Productos.Where(x => x.IdProducto == Producto.IdProducto).AnyAsync();
            if (ModelState.IsValid && data)
            {
                ctx.Productos.Update(Producto);
                ctx.Entry(Producto).State = EntityState.Modified;
                // data.Nombre = Producto.Nombre;
                // data.Descripcion = Producto.Descripcion;
                // data.Marca = Producto.Marca;
                // data.Modelo = Producto.Modelo;
                // data.IdCategoria = Producto.IdCategoria;
                await ctx.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Registro()
        {

            var data = await ctx.Productos.Where(x => x.IdProducto == Producto.IdProducto).AnyAsync();
            if (ModelState.IsValid && !data)
            {
                ctx.Productos.Add(Producto);
                await ctx.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
          public async Task<IActionResult> Eliminar(int id)
        {

            var data = await ctx.Productos.FindAsync(id);
            if (ModelState.IsValid && data!=null)
            {
                ctx.Productos.Remove(data);
                await ctx.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}