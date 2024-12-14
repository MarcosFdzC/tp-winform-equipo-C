using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public string codigo {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Marca marca { get; set; } // asociar con la clase Marca
        public Categoria categoria { get; set; } // asociar con la clase Categoria
        public Imagen imagenes { get; set; } // asociar con la clase Imagen
        public int precio { get; set; }
    }
}
