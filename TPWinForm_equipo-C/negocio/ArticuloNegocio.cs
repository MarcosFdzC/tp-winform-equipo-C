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
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS03; database=CATALOGO_P3_DB; integrated security=true"; //cuidado al usar esto recordar colocarlo como lo tienen en su local
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from ARTICULOS"; //Puse eso para probar, pero aun no conecte esto al datagridview
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = lector.GetInt32(0);
                    aux.Codigo = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Imagenes = new Imagen(); //No estoy seguro de que esto vaya aca
                    aux.Categoria = new Categoria(); //Esto tampoco
                    aux.Marca = new Marca();//Esto tampoco je
                    aux.Precio = lector.GetFloat(0);

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
