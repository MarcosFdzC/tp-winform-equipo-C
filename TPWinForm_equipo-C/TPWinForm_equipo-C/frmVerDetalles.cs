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
    public partial class frmVerDetalles : Form
    {
        Articulo articulo;
        private int currentImageIndex = 0;
        public frmVerDetalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmVerDetalles_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDesc.Text = articulo.Descripcion;
            txtMarca.Text = articulo.Marca.Descripcion;
            txtCategoria.Text = articulo.Categoria.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            imagenNegocio.listar(articulo);
            if (articulo.Imagenes.Count > 0)
            {
                currentImageIndex = 0;
                MostrarImagenActual();
            }
            else
            {
                pbxListaImg.Image = null;
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //ImagenNegocio imagenNegocio = new ImagenNegocio();
            //Imagen imgAux = new Imagen();
            //imgAux.IdArticulo = articulo.Id;
            //imgAux.ImagenUrl = txtAgregarImg.Text;
            //if (txtAgregarImg.Text != "")
            //{
            //    imagenNegocio.agregar(imgAux);
            //    MessageBox.Show("Imagen agregada exitosamente");
            //}
            //else
            //    MessageBox.Show("Inserte una url primero");

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            //if (articulo.Imagenes.Count > 0)
            //{
            //    currentImageIndex = (currentImageIndex - 1 + articulo.Imagenes.Count) % articulo.Imagenes.Count;
            //    MostrarImagenActual();
            //}
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            //if (articulo.Imagenes.Count > 0)
            //{
            //    currentImageIndex = (currentImageIndex + 1) % articulo.Imagenes.Count;
            //    MostrarImagenActual();
            //}
        }
        private void MostrarImagenActual()
        {
            if (articulo.Imagenes.Count > 0)
            {
                try
                {
                    pbxListaImg.Load(articulo.Imagenes[currentImageIndex].ImagenUrl);
                }
                catch
                {
                    pbxListaImg.Load("https://via.placeholder.com/150");
                }
            }
            else
            {
                pbxListaImg.Image = null;
            }
        }
    }
}
