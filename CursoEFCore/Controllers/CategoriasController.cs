using CursoEFCore.Database;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoEFCore.Controllers
{
    public class CategoriasController : Controller
    {
        // - - - - Inyeccion de Dependencias - - - -
        public readonly ApplicationDbContext _dbContext;
        public CategoriasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // - - - - Inyeccion de Dependencias - - - -


        [HttpGet]
        public IActionResult Index()
        {
            List<Categoria> listaCategorias = _dbContext.Categorias.ToList();

            return View(listaCategorias);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categorias.Add(categoria);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult CrearMultipleOpcion2()
        {
            List<Categoria> categorias = new List<Categoria>();

            for (int i = 0; i < 2; i++)
            {
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                //_dbContext.Categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _dbContext.Categorias.AddRange(categorias);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CrearMultipleOpcion5()
        {
            List<Categoria> categorias = new List<Categoria>();

            for (int i = 0; i < 5; i++)
            {
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                //_dbContext.Categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _dbContext.Categorias.AddRange(categorias);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult VistaCrearMultipleOpcionFormulario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearMultipleOpcionFormulario()
        {
            // La siguiente es una de tantas formas que hay para recojer los datos del formulario
            string categoriasForm = Request.Form["Nombre"];

            // categoriasForm es una cadena con los nombres de las 5 categorias separados por ","
            // En seguida convertimos esa cadena a una Lista separando los valores.
            var listaCategorias = from val in categoriasForm.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries) select (val);

            List<Categoria> categorias = new List<Categoria>();

            foreach (var categoria in listaCategorias)
            {
                categorias.Add(new Categoria { Nombre = categoria });
            }

            _dbContext.Categorias.AddRange(categorias);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
