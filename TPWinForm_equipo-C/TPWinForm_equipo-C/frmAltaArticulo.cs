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
using System.IO;
using System.Configuration;

namespace TPWinForm_equipo_C
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        //Este segundo constructor nos permite reutilizar el formulario para precargar los datos.
        //(Funcion modificar)
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio artNegocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
                    articulo = new Articulo();
                
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                if (decimal.TryParse(txtPrecio.Text, out decimal precio))//hacemos una conversion del textbox para que el valor "string" que devuelve convertirlo en "decimal".
                    articulo.Precio = precio;
                else
                    MessageBox.Show("Por favor, ingrese un precio válido.");
                if(articulo.Id != 0)
                {
                    
                    artNegocio.modificar(articulo);
                    ImagenNegocio imgNegocio = new ImagenNegocio();
                    if (articulo.Imagenes[0].ImagenUrl != null)
                    {
                        Imagen imgModificar = new Imagen();

                        imgModificar.Id = articulo.Imagenes[0].Id;
                        imgModificar.IdArticulo = articulo.Id;
                        imgModificar.ImagenUrl = txtImagen.Text;
                        imgNegocio.modificar(imgModificar);
                    }
                    else
                    {
                        Imagen imgAgregar = new Imagen();
                        imgAgregar.IdArticulo = articulo.Id;
                        imgAgregar.ImagenUrl = txtImagen.Text;
                        imgNegocio.agregar(imgAgregar);
                    }
                    
                    MessageBox.Show("Articulo modificado exitosamente!");
                }
                else
                {
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
                            MessageBox.Show("Agregado exitosamente!");
                        }
                    }
                }
                if (archivo != null && (txtImagen.Text.ToUpper().Contains("HTTP")))
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
            MarcaNegocio marca = new MarcaNegocio();
            try
            {
                //cargas de los desplegables
                cboCategoria.DataSource = categoria.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marca.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                //precarga(funcion modificar)
                if(articulo != null)
                {
                    cargarImagen(articulo.Imagenes[0].ImagenUrl);
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtImagen.Text = articulo.Imagenes[0].ImagenUrl;
                    txtPrecio.Text = articulo.Precio.ToString();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
        private void cargarImagen(string img)
        {
            try
            {
                pbxArticulo.Load(img);
            }
            catch (Exception ex)
            {
                //carga un placeholder si no se agregó ninguna imagen o la url es invalida
                pbxArticulo.Load("https://i0.wp.com/mckameyanimalcenter.org/wp-content/uploads/2022/05/placeholder-661.png?fit=1200%2C800&ssl=1");
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg!|png|*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
                //guardo la imagen
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["Image-folder"] + archivo.SafeFileName);
            }
        }
    }
}
