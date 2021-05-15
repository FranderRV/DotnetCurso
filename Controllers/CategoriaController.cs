using System.Linq;
using System.Timers;
using _.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCVTienda.Controllers
{
    public class CategoriaController : Controller
    {
        TiendaCX ctx;

        public CategoriaController(TiendaCX _ctx)
        {
            ctx = _ctx;
        }

        public IActionResult Index()
        {
            ViewBag.lista = ctx.Categorias.ToList();
            return View();
        }

        public IActionResult ViewRegistro()
        {
            return View("Formulario", new Categoria());
        }
        [BindProperty]
        public Categoria Categoria { get; set; }
        public IActionResult Registrar()
        {
            var data = ctx.Categorias.Where(x => x.IdCategoria == Categoria.IdCategoria).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (data == null)
                {
                    ctx.Categorias.Add(Categoria);
                }
                else
                {
                    data.Nombre = Categoria.Nombre;
                }
                ctx.SaveChanges();
                
            return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {

            var data = ctx.Categorias.Find(id);

            if (data != null)
            {
                return View("Formulario", data);
            }

            return View();
        }

        public IActionResult Eliminar(int id)
        {

            var data = ctx.Categorias.Find(id);

            if (data != null)
            {
                ctx.Categorias.Remove(data);
                ctx.SaveChanges();
            }

            return View();
        }
    }
}