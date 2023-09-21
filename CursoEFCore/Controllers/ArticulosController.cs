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



        // -- - - - - - - - - - -  - -  - - - Metodos Relacion Muchos - Muchos (ArticuloEtiqueta) - - - - - - - - - - -  - - -- - -- -

        [HttpGet]
        public IActionResult AdministrarEtiquetas(int id)
        {
            ArticuloEtiquetaVM articuloEtiquetas = new ArticuloEtiquetaVM()
            {
                // Aqui tenemos que colocar 2 Includes() ya que esta tabla es Intermediaria y SIEMPRE en una tabla que sea intermediaria
                // tenemos que colocar 2 Includes() para traer los registros asociados a los IDs de esta tabla.
                // Entonces por cada Registro "ArticuloEtiquetas" vamos a traer otros 2 Registros, 1 Articulo y 1 Etiqueta.
                // Asi que con cada Registro ArticuloEtiqueta tremos 2 registros asociados 1 Articulo y 1 Etiqueta, SIEMPRE sera
                // asi con las tablas Intermedias.
                // Obtendremos TODOS los registros ArticuloEtiqueta condicionalmente con el Where, los que tengan el ID del Articulo que 
                // nos llega por parametros, despues de ese filtro ya tendremos solo los objetos ArticuloEtiqueta que contengan el ID 
                // del Articulo que nos llega por params, despues por cada obj ArticuloEtiqueta le incluimos sus propiedades Articulo y
                // Etiqueta.
                ListaArticuloEtiquetas = _dbContext.ArticuloEtiquetas.Where(ae => ae.Articulo_Id == id).Include(e => e.Etiqueta).Include(a => a.Articulo).Where(ae => ae.Articulo_Id == id),

                // Al Campo "Articulo_Id" del Objeto "ArticuloEtiqueta" le asignamos el id del Articulo que viene desde la vista.
                ArticuloEtiqueta = new ArticuloEtiqueta()
                {
                    Articulo_Id = id
                },

                // Esto es algo tipico, traemos el Articulo de la Db que coincide con el id que nos llega por params.
                Articulo = _dbContext.Articulos.FirstOrDefault(a => a.Articulo_Id == id)
            };


            // - - -  - - - - - Todo lo de a partir de aqui es para crear el DropDown  - - - - - - - - -

            // Haremos una Lista de las Etiquetas que tiene asociado determinado articulo. Nos queda una lista de enteros (id) asociados
            // de manera temporal a un articulo, cual? el que nos llega su id por params. Necesitamos esto para poder crear una Lista
            // de etiquetas que NO esten asociadas a el Articulo Actual.
            // Al Usar Select() estamos utilizando proyecciones, es decir que el objeto se convierte de un Tipo "Etiqueta" a un 
            // tipo "Int" al seleccionar "e.Etiqueta_Id" para que sea el unico que pase, ya que el ID es un Tipo Int.
            List<int> listaTemporalEtiquetasArticulo = articuloEtiquetas.ListaArticuloEtiquetas.Select(e => e.Etiqueta_Id).ToList();

            // Despues vamos a obtener desde la DB todas las etiquetas cuyos id's no esten en la lista "listaTemporalEtiquetasArticulo"
            // LOS QUE NO ESTEN AHI. Crear un NOT IN en LINQ, o sea las etiquetas que no esten asociadas al articulo actual para
            // colocarlas en una Lista. Esta Lista de Etiquetas de la DB la necesitamos para crear el Dropdown y enviarlo a la Vista.
            var listaTemporal = _dbContext.Etiquetas.Where(e => !listaTemporalEtiquetasArticulo.Contains(e.Etiqueta_Id)).ToList();

            // Aqui es cuando ya usamos la Lista de Etiquetas de arriba para Crear el Dropdown de Etiquetas en el Campo "ListaEtiquetas".
            articuloEtiquetas.ListaEtiquetas = listaTemporal.Select(i => new SelectListItem
            {
                Text = i.Titulo,
                Value = i.Etiqueta_Id.ToString()
            });

            return View(articuloEtiquetas);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AdministrarEtiquetas(ArticuloEtiquetaVM articuloEtiquetas)
        {
            if (articuloEtiquetas.ArticuloEtiqueta.Articulo_Id != 0 && articuloEtiquetas.ArticuloEtiqueta.Etiqueta_Id != 0)
            {
                _dbContext.ArticuloEtiquetas.Add(articuloEtiquetas.ArticuloEtiqueta);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(AdministrarEtiquetas), new {@id = articuloEtiquetas.ArticuloEtiqueta.Articulo_Id});
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EliminarEtiquetas(int idEtiqueta, ArticuloEtiquetaVM articuloEtiquetas)
        {
            // En este metodo solo se eliminan las tablas intermedias "ArticuloEtiqueta", no se elimina nada de nungun registro de Articulo
            // ni nada de ningun registro de Etiqueta, ni IDs ni nada, eso queda intacto, no se borra ningun ID ni te la tabla Articulo ni
            // de la tabla Etiqueta, solo se borra el registro de relacion intermedia ArticuloEtiqueta.
            int idArticulo = articuloEtiquetas.Articulo.Articulo_Id;
            ArticuloEtiqueta articuloEtiqueta = _dbContext.ArticuloEtiquetas.FirstOrDefault(ae=>ae.Etiqueta_Id  == idEtiqueta && ae.Articulo_Id == idArticulo);
            
            _dbContext.ArticuloEtiquetas.Remove(articuloEtiqueta);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = idArticulo });
        }

    }
}
