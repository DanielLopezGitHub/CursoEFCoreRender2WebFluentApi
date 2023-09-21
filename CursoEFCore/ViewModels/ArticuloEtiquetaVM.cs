using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CursoEFCore.ViewModels
{
    public class ArticuloEtiquetaVM
    {
        /* Este ViewModel se encargara de transportar del Metodo "AdministrarEtiquetas(int id)" a la vista "AdministrarEtiquetas"
         * todos los datos que recuperemos de la DB para poder mostrarlos en el Formulario de la Vista.
         * Y tambien servira para enviar dentro de el la informacion al metodo para postear el ArticuloEtiqueta en la DB.
         */
        public ArticuloEtiqueta ArticuloEtiqueta { get; set; }
        public Articulo Articulo { get; set; }

        // Esta es una lista de ArticuloEtiquetas, y en cada obj ArticuloEtiqueta se incluira 1 obj Articulo y 1 obj Etiqueta, ya
        // que en el modelo estan definidas esas propiedades para que el Include() pueda depositarlas ahi.
        public IEnumerable<ArticuloEtiqueta> ListaArticuloEtiquetas { get; set; }

        // Este es el DropDown de Etiquetas, en el deben de ir las etiquetas que no estan asociadas al Articulo.
        public IEnumerable<SelectListItem> ListaEtiquetas { get; set; }
    }
}
