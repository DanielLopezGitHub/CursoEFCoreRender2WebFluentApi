using CursoEFCore.Database;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoEFCore.Controllers
{
    public class EtiquetasController : Controller
    {
        // - - - - Inyeccion de Dependencias - - - -
        public readonly ApplicationDbContext _dbContext;
        public EtiquetasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // - - - - Inyeccion de Dependencias - - - -



        // - - - - -  - - - - - - -- - -- - - - - - - - -  Metodos  - - - - - - - - - - - - - - - - - - - - - - - 
        [HttpGet]
        public IActionResult Index()
        {
            List<Etiqueta> etiquetas = _dbContext.Etiquetas.ToList();
            return View(etiquetas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Etiqueta etiqueta)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Etiquetas.Add(etiqueta);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var etiquetaFromDb = _dbContext.Etiquetas.FirstOrDefault(e => e.Etiqueta_Id == id);
            if(etiquetaFromDb == null)
            {
                return NotFound();
            }
            return View(etiquetaFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Etiqueta etiqueta)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Etiquetas.Update(etiqueta);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(etiqueta);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var etiquetaFromDb = _dbContext.Etiquetas.FirstOrDefault(e => e.Etiqueta_Id == id);
            if (etiquetaFromDb == null)
            {
                return NotFound();
            }
            _dbContext.Etiquetas.Remove(etiquetaFromDb);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
