using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CursoEFCore.ViewModels
{
    public class ArticuloCategoriaVM
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
