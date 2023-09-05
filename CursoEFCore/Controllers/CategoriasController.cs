using CursoEFCore.Database;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            // 1- Consulta tipica normal inicial sin filtro
            List<Categoria> listaCategorias = _dbContext.Categorias.ToList();

            // 2- Consulta Filtrando por fecha, trayendo los registros que esten por en cima de esta fecha
            DateTime fechaComparacion = new DateTime(2023, 02, 15);
            //List<Categoria> listaCategorias = _dbContext.Categorias.Where(f => f.FechaCreacion >= fechaComparacion).OrderBy(f => f.FechaCreacion).ToList();

            // 3- Agrupar por valores, en este caso los activos e inactivos (Este ejemplo devuelve otro tipo de Model que el que recibe la vista, asi que
            // va a dar error en el cliente, se tiene que verificar el resultado aqui con debug)
            //var listaCategorias = _dbContext.Categorias.GroupBy(c => new {c.Activo}).Select(c => new {c.Key, Count = c.Count()}).ToList();

            // 4- Take() y Skip() nos permiten filtrar una cantidad de Registros especifica, con take comamos cierta cantidad de registros y 
            // con skip saltamos un numero de registros para tomar los que siguen.
            //List<Categoria> listaCategorias = _dbContext.Categorias.Skip(3).Take(3).ToList();

            // 5- Consulta sql convencional con lenguage SQL
            //List<Categoria> listaCategorias = _dbContext.Categorias.FromSqlRaw("select * from Categorias where nombre like '1%' and activo = 0").ToList();

            return View(listaCategorias);

            // 6- Interpolacion de Strings, proteger consultas de inyecciones SQL
            //var id = 64;
            //var categoria = _dbContext.Categorias.FromSqlRaw($"select * from Categorias where categoria_id = {id}").ToList();
            //return View(categoria);


            // 6- Filtrar por NOMBRE, seleccionar columnas especificas en este caso por columna "Nombre".
            //var categorias = _dbContext.Categorias.Where(n => n.Nombre == "8 otro").Select(n => n).ToList();
            //return View(categorias);
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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var categoria = _dbContext.Categorias.FirstOrDefault(c => c.Categoria_Id == id);
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categorias.Update(categoria);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var categoria = _dbContext.Categorias.FirstOrDefault(c => c.Categoria_Id == id);
            _dbContext.Categorias.Remove(categoria);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BorrarMultiple2()
        {
            // Ordena los registros de forma decendente en ID toma 2 y los mete a la variable.
            IEnumerable<Categoria> categorias = _dbContext.Categorias.OrderByDescending(c => c.Categoria_Id).Take(2);
            _dbContext.Categorias.RemoveRange(categorias);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BorrarMultiple5()
        {
            // Ordena los registros de forma decendente en ID toma 2 y los mete a la variable.
            IEnumerable<Categoria> categorias = _dbContext.Categorias.OrderByDescending(c => c.Categoria_Id).Take(5);
            _dbContext.Categorias.RemoveRange(categorias);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
