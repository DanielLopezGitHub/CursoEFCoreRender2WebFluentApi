using CursoEFCore.Database;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Controllers
{
    public class UsuariosController : Controller
    {
        // - - - - Inyeccion de Dependencias - - - -
        public readonly ApplicationDbContext _dbContext;
        public UsuariosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // - - - - Inyeccion de Dependencias - - - -




        // - - - - -  - - - - - - -- - -- - - - - - - - -  Metodos Usando EF Core LINQ  - - - - - - - - - - - - - - - - - - - - - - - 

        [HttpGet]
        public IActionResult Index()
        {
            List<Usuario> listaUsuarios = _dbContext.Usuarios.ToList();
            return View(listaUsuarios);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var usuarioDesdeDb = _dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
            return View(usuarioDesdeDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Usuarios.Update(usuario);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
            _dbContext.Usuarios.Remove(usuario);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //  - - -  - - - -  - - - - -- Metodos Relacion 1 - 1   - - - - -  - - -- -  - - - -- -  -

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var usuario = _dbContext.Usuarios.Include(d => d.DetalleUsuario).FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarDetalle(Usuario usuario)
        {
            if (usuario.DetalleUsuario.DetalleUsuario_Id == 0)
            {
                // Creamos nuevo registro para tabla DetalleUsuario, creamos los detalles para ese usuario
                _dbContext.DetalleUsuarios.Add(usuario.DetalleUsuario);
                _dbContext.SaveChanges();

                // Despues le agregamos este registro de DetalleUsuario al Usuario actual.
                var usuarioDb = _dbContext.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
                usuarioDb.DetalleUsuario_Id = usuario.DetalleUsuario.DetalleUsuario_Id;
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
