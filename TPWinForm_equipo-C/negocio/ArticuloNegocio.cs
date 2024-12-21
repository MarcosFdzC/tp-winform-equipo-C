using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Xml.Linq;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Art.Id IdArticulo, Codigo, Nombre, Art.Descripcion ArtDescripcion, IdMarca, M.Descripcion Marca, IdCategoria, Cat.Descripcion Categoria, Img.Id IdImagen, Img.ImagenUrl, Art.Precio From ARTICULOS Art, MARCAS M, CATEGORIAS Cat, IMAGENES Img where Art.IdMarca = M.Id AND Art.IdCategoria = Cat.Id AND Img.IdArticulo = Art.Id");//Consulta final con sus alias ya incluidos, esta consulta trae todos los datos que necesitamos cargar en la lista de tipo Ariculo
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdArticulo"];//Entre corchetes va el nombre/alias de la columna. En este caso IdArticulo es un alias que le puse para diferenciarlo de los otros ids. Como por ejemplo el Id de Categoria o Marca.
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["ArtDescripcion"];
                    //instancia Marca
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    //instancia Categoria
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    //Instancia Imagen auxiliar para despues ser agregada a la lista
                    Imagen ImgAux = new Imagen();
                    ImgAux.Id = (int)datos.Lector["IdImagen"];
                    ImgAux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    ImgAux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    //Instancia Lista Imagen
                    aux.Imagenes = new List<Imagen>();
                    aux.Imagenes.Add(ImgAux);
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert Into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
