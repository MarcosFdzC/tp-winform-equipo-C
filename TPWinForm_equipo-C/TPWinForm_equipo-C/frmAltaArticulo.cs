using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace TPWinForm_equipo_C
{
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            ArticuloNegocio artNegocio = new ArticuloNegocio();
            try
            {
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                if (decimal.TryParse(txtPrecio.Text, out decimal precio))//hacemos una conversion del textbox para que el valor "string" que devuelve convertirlo en "decimal".
                    articulo.Precio = precio;
                else
                    MessageBox.Show("Por favor, ingrese un precio válido.");
                artNegocio.agregar(articulo);
                //hasta acá agregó un articulo a la tabla ARTICULOS
                //luego procedemos a leer el id máximo( es = al articulo recien agregado) para saber cual es el Id de ese articulo asi poder agregar el dato a la tabla imagen con el url
                if (txtImagen != null)
                {
                    AccesoDatos datos = new AccesoDatos();
                    ImagenNegocio ImgNegocio = new ImagenNegocio();
                    datos.setearConsulta("select max(Id) as IdArticulo from ARTICULOS");
                    datos.ejecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Imagen ImgAux = new Imagen();
                        ImgAux.IdArticulo = (int)datos.Lector["IdArticulo"];
                        ImgAux.ImagenUrl = txtImagen.Text;
                        //Ahora tengo los datos cargados y los agregamos a la tabla Imagenes
                        ImgNegocio.agregar(ImgAux);
                    }
                    

                }

                MessageBox.Show("Agregado exitosamente!");
                Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca =  new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = categoria.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marca.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";


            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            try
            {
                pbxArticulo.Load(txtImagen.Text);

            }
            catch(Exception ex)
            {
                pbxArticulo.Load("https://i0.wp.com/mckameyanimalcenter.org/wp-content/uploads/2022/05/placeholder-661.png?fit=1200%2C800&ssl=1");
            }
        }
    }
}
