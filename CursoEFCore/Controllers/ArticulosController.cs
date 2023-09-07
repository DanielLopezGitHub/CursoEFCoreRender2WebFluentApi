using CursoEFCore.Database;
using CursoEFCore.Models;
using CursoEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Controllers
{
    public class ArticulosController : Controller
    {
        // - - - - Inyeccion de Dependencias - - - -
        public readonly ApplicationDbContext _dbContext;
        public ArticulosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // - - - - Inyeccion de Dependencias - - - -




        [HttpGet]
        public IActionResult Index()
        {
            // Opcion 1: de traer datos relacionados de Foreign Keys (Solo trae el ID de la categoria)
            //var listaArticulos = _dbContext.Articulos.ToList();

            //foreach (var articulo in listaArticulos)
            //{
            //    // Opcion 2: Carga manual se generan muchas consultas SQL, no es muy eficiente si necesitamos cargar mas tablas relacionadas
            //    //articulo.Categoria = _dbContext.Categorias.FirstOrDefault(c => c.Categoria_Id == articulo.Categoria_Id);

            //    // Opcion 3: Explicit Loading
            //    _dbContext.Entry(articulo).Reference(c => c.Categoria).Load();
            //}

            // Opcion 4: Eager Loading (Include) la manera mas eficiente de traer registros de tablas relacionadas.
            List<Articulo> listaArticulos = _dbContext.Articulos.Include(c => c.Categoria).ToList();

            return View(listaArticulos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _dbContext.Categorias.Select(item => new SelectListItem { Text = item.Nombre, Value = item.Categoria_Id.ToString() });

            return View(articuloCategorias);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Articulos.Add(articulo);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _dbContext.Categorias.Select(item => new SelectListItem { Text = item.Nombre, Value = item.Categoria_Id.ToString() });
            return View(articuloCategorias);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id == null)
            {
                return View();
            }

            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _dbContext.Categorias.Select(item => new SelectListItem { Text = item.Nombre, Value = item.Categoria_Id.ToString() });

            articuloCategorias.Articulo = _dbContext.Articulos.FirstOrDefault(a => a.Articulo_Id == id);
            if (articuloCategorias == null)
            {
                return NotFound();
            }

            return View(articuloCategorias);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ArticuloCategoriaVM articuloVm)
        {
            if(articuloVm.Articulo.Articulo_Id == 0)
            {
                return View(articuloVm.Articulo);
            }
            else
            {
                _dbContext.Articulos.Update(articuloVm.Articulo);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var articulo = _dbContext.Articulos.FirstOrDefault(a => a.Articulo_Id == id);
            _dbContext.Articulos.Remove(articulo);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
