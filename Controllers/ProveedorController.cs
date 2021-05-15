using System.Linq;
using _.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCVTienda.Controllers
{
    public class ProveedorController : Controller
    {
        TiendaCX ctx;
        public ProveedorController(TiendaCX _ctx)
        {
            ctx = _ctx;
        }

        public IActionResult Index()
        {

            ViewBag.lista = ctx.Proveedores.ToList();

            return View();
        }

        [BindProperty]
        public Proveedore Proveedore { set; get; }
        
        public IActionResult Registrar()
        {

            if (ModelState.IsValid)
            {
                var data = ctx.Proveedores.Where(x => x.IdProveedor == Proveedore.IdProveedor).SingleOrDefault();
                if (data == null)
                {
                    ctx.Proveedores.Add(Proveedore);
                }
                else
                {
                    data.Nombre = Proveedore.Nombre;
                    data.Dui = Proveedore.Dui;
                    data.Nit = Proveedore.Nit;
                    data.Correo = Proveedore.Correo;
                }
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

                return RedirectToAction("Index");
        }

         public IActionResult ViewRegistrar(){
            return View("Formulario",new Proveedore());
        }

        public IActionResult Editar(int id)
        {
            var data = ctx.Proveedores.Find(id);
            if (data != null)
            {
                return View("Formulario", data);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id){
            
            var data = ctx.Proveedores.Find(id);
            if(data!=null){
                ctx.Proveedores.Remove(data);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
    
}