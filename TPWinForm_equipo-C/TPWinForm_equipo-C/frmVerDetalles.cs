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
        int index = 0;
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
            cargarImagen(articulo.Imagenes[0].ImagenUrl);

        }

        private void cargarImagen(string img)
        {
            try
            {
                pbxListaImg.Load(img);
            }
            catch
            {
                pbxListaImg.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
            }
        }

        private void txtAgregarImg_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pbxListaImg.Load(txtAgregarImg.Text);
            }
            catch
            {
                pbxListaImg.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ImagenNegocio imgNegocio = new ImagenNegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Agregar imagen?", "Agregando una imágen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    Imagen imgAux = new Imagen();
                    imgAux.IdArticulo = articulo.Id;
                    imgAux.ImagenUrl = txtAgregarImg.Text;
                    imgNegocio.agregar(imgAux);
                    txtAgregarImg.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la imagen: " + ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                int max = articulo.Imagenes.Count;
                if (index < max - 1)
                {
                    index++;
                    if (!(string.IsNullOrWhiteSpace(articulo.Imagenes[index].ImagenUrl)))
                        cargarImagen(articulo.Imagenes[index].ImagenUrl);
                    else
                        pbxListaImg.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (index > 0)
                {
                    index--;
                    if (!(string.IsNullOrWhiteSpace(articulo.Imagenes[index].ImagenUrl)))
                        cargarImagen(articulo.Imagenes[index].ImagenUrl);
                    else
                        pbxListaImg.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
