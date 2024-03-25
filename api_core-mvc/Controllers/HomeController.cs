using api_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using api_core_mvc.Servicios;

namespace api_core_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicio_api;

        public HomeController(IServicio_API servicio_api)
        {
            _servicio_api = servicio_api;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await _servicio_api.Lista();

            return View(lista);
        }

        public async Task<IActionResult> Producto(int id)
        {
            Producto obj = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if (id > 0)
            {
                ViewBag.Accion = "Editar Producto";
                obj = await _servicio_api.Obtener(id);
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Producto obj)
        {
            bool respuesta = false;

            if (obj.IdProducto == 0)
                respuesta = await _servicio_api.Guardar(obj);
            else
                respuesta = await _servicio_api.Editar(obj);

            if (respuesta)
                return RedirectToAction("Index");

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _servicio_api.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Index");

            return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
