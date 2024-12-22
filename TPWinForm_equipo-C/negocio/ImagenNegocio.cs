using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listar(Articulo art)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("Select IdArticulo, ImagenUrl from IMAGENES where IdArticulo = @IdArticulo");
            datos.setearParametro("IdArticulo", art.Id);
            return lista;
        }
        public void agregar(Imagen nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert Into IMAGENES (IdArticulo, ImagenUrl) Values (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", nuevo.IdArticulo);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
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
        public void modificar(Imagen modificada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update IMAGENES set IdArticulo = @idArticulo, ImagenUrl = @imgUrl where Id = @idImagen");
                datos.setearParametro("@idArticulo", modificada.IdArticulo);
                datos.setearParametro("@imgUrl", modificada.ImagenUrl);
                datos.setearParametro("@idImagen", modificada.Id);
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
        public void eliminarFisico(Articulo eliminada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Cambiar el IdMarca del artículo a un valor negativo
                datos.setearConsulta("delete from IMAGENES where IdArticulo = @Id");
                datos.setearParametro("@Id", eliminada.Id);
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
