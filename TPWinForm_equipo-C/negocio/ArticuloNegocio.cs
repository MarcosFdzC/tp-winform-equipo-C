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
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select Art.Id IdArticulo, Codigo, Nombre, Art.Descripcion ArtDescripcion, IdMarca, M.Descripcion Marca, IdCategoria, Cat.Descripcion Categoria, Img.Id IdImagen, Img.ImagenUrl, Art.Precio From ARTICULOS Art, MARCAS M, CATEGORIAS Cat, IMAGENES Img where Art.IdMarca = M.Id AND Art.IdCategoria = Cat.Id AND Img.IdArticulo = Art.Id AND";

                switch (campo)
                {
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a:":
                                consulta += "Art.Precio > " + filtro;
                                break;
                            case "Menor a:":
                                consulta += "Art.Precio < " + filtro;
                                break;
                            default:
                                consulta += "Art.Precio = " + filtro;
                                break;
                        }
                        break;
                    case "Descripcion":
                        switch (criterio)
                        {
                            case "Comienza con:":
                                consulta += "ArtDescripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termina con:":
                                consulta += "ArtDescripcion LIKE '%" + filtro + "'";
                                break;
                            case "Contiene:":
                                consulta += "ArtDescripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con:":
                                consulta += "Nombre LIKE '" + filtro + "%'";
                                break;
                            case "Termina con:":
                                consulta += "Nombre LIKE '%" + filtro + "'";
                                break;
                            case "Contiene:":
                                consulta += "Nombre LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con:":
                                consulta += "ArtDescripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termina con:":
                                consulta += "ArtDescripcion LIKE '%" + filtro + "'";
                                break;
                            case "Contiene:":
                                consulta += "ArtDescripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Categoria":
                        switch (criterio)
                        {
                            case "Comienza con:":
                                consulta += "Categoria LIKE '" + filtro + "%'";
                                break;
                            case "Termina con:":
                                consulta += "Categoria LIKE '%" + filtro + "'";
                                break;
                            case "Contiene:":
                                consulta += "Categoria LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdArticulo"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["ArtDescripcion"];

                    // Instancia Marca
                    aux.Marca = new Marca
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        Descripcion = (string)datos.Lector["Marca"]
                    };

                    // Instancia Categoria
                    aux.Categoria = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Descripcion = (string)datos.Lector["Categoria"]
                    };

                    // Instancia Imagen
                    Imagen ImgAux = new Imagen
                    {
                        Id = (int)datos.Lector["IdImagen"],
                        IdArticulo = (int)datos.Lector["IdArticulo"],
                        ImagenUrl = (string)datos.Lector["ImagenUrl"]
                    };

                    aux.Imagenes = new List<Imagen> { ImgAux };
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                datos.setearConsulta("Select Art.Id IdArticulo, Codigo, Nombre, Art.Descripcion ArtDescripcion, IdMarca, M.Descripcion Marca, IdCategoria, Cat.Descripcion Categoria, Precio From ARTICULOS Art, MARCAS M, CATEGORIAS Cat where Art.IdMarca = M.Id AND Art.IdCategoria = Cat.Id");//Consulta final-trae todos los datos menos los de la tabla img
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    if (!(datos.Lector["IdArticulo"] is DBNull))
                        aux.Id = (int)datos.Lector["IdArticulo"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["ArtDescripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["ArtDescripcion"];
                    //instancia Marca
                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    //instancia Categoria
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    //Instancia Imagen auxiliar para despues ser agregada a la lista
                    AccesoDatos datosImg = new AccesoDatos();
                    datosImg.setearConsulta("Select Id, IdArticulo, ImagenUrl from IMAGENES where IdArticulo = @imgIdArt");
                    datosImg.setearParametro("@imgIdArt", (int)datos.Lector["IdArticulo"]);
                    datosImg.ejecutarLectura();
                    aux.Imagenes = new List<Imagen>();
                    while (datosImg.Lector.Read())
                    {
                        Imagen ImgAux = new Imagen();
                        if (!(datosImg.Lector["Id"] is DBNull))
                            ImgAux.Id = (int)datosImg.Lector["Id"];
                        if (!(datosImg.Lector["IdArticulo"] is DBNull))
                            ImgAux.IdArticulo = (int)datosImg.Lector["IdArticulo"];
                        if (!(datosImg.Lector["ImagenUrl"] is DBNull))
                            ImgAux.ImagenUrl = (string)datosImg.Lector["ImagenUrl"];
                        //Instancia Lista Imagen
                        aux.Imagenes.Add(ImgAux);
                    }
                    datosImg.cerrarConexion();
                    if (!(datos.Lector["Precio"] is DBNull))
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
        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, Precio = @precio where Id = @idArticulo");
                datos.setearParametro("@idArticulo", modificar.Id);
                datos.setearParametro("@codigo", modificar.Codigo);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@descripcion", modificar.Descripcion);
                datos.setearParametro("@idMarca", modificar.Marca.Id);
                datos.setearParametro("@idCategoria", modificar.Categoria.Id);
                datos.setearParametro("@precio", modificar.Precio);
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
        public void eliminarFisico(Articulo eliminarFisico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Cambiar el IdMarca del artículo a un valor negativo
                datos.setearConsulta("Delete from ARTICULOS where id = @Id");
                datos.setearParametro("@Id", eliminarFisico.Id);
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

        public void eliminarLogico(Articulo eliminarLogico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Cambiar el IdMarca del artículo a un valor negativo
                datos.setearConsulta("UPDATE ARTICULOS SET IdMarca = IdMarca * -1 WHERE Id = @Id");
                datos.setearParametro("@Id", eliminarLogico.Id);
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
