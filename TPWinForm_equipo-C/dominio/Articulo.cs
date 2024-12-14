using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public int Codigo {  get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; } // asociar con la clase Marca
        public Categoria Categoria { get; set; } // asociar con la clase Categoria
        public Imagen Imagenes { get; set; } // asociar con la clase Imagen
        public float Precio { get; set; }
    }
}
