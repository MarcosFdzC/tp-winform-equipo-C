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
using negocio;

namespace TPWinForm_equipo_C
{
    public partial class frmABM : Form
    {
        List<Marca> listaMarca;
        List<Categoria> listaCategoria;
        string abm;
        public frmABM(List<Marca>marcas)
        {
            InitializeComponent();
            this.listaMarca = marcas;
            abm = "marcas";
        }
        public frmABM(List<Categoria> categorias)
        {
            InitializeComponent();
            this.listaCategoria = categorias;
            abm = "categoria";
        }

        private void frmABMMarca_Load(object sender, EventArgs e)
        {
            try
            {
                if (listaCategoria != null) 
                {
                    cboListar.DataSource = listaCategoria;
                    lblABM.Text = "Categorias: ";
                }
                else
                {
                    cboListar.DataSource = listaMarca;
                    lblABM.Text = "Marcas: ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error" + ex.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (abm == "marcas")
                {
                   MarcaNegocio marcaNegocio = new MarcaNegocio();
                    if ( !(string.IsNullOrWhiteSpace(txtAgregar.Text)) )
                    {
                        marcaNegocio.agregar(txtAgregar.Text);
                        MessageBox.Show("Marca agregada exitosamente!");
                        txtAgregar.Text = "";
                        actualizarMarcas();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un valor válido");
                    }
                }
                else
                {
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    if (!(string.IsNullOrWhiteSpace(txtAgregar.Text)))
                    {
                        categoriaNegocio.agregar(txtAgregar.Text);
                        MessageBox.Show("Categoria agregada exitosamente!");
                        txtAgregar.Text = "";
                        actualizarCategorias();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un valor válido");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (abm == "marcas")
                {
                    if (!(cboListar.SelectedItem is DBNull))
                    {
                        if ( !(string.IsNullOrWhiteSpace(txtModificar.Text)) )
                        {
                            Marca nuevaMarca = new Marca();
                            MarcaNegocio negocio = new MarcaNegocio();
                            nuevaMarca = (Marca)cboListar.SelectedItem;
                            nuevaMarca.Descripcion = txtModificar.Text;
                            negocio.modificar(nuevaMarca);
                            MessageBox.Show("Marca modificada correctamente");
                            txtModificar.Text = "";
                            actualizarMarcas();
                        }
                        else
                        {
                            MessageBox.Show("Por favor, ingrese un texto válido.");
                        }
                    }
                }
                else
                {
                    if (!(cboListar.SelectedItem is DBNull))
                    {
                        if (!(string.IsNullOrWhiteSpace(txtModificar.Text)))
                        {
                            Categoria nuevaCategoria = new Categoria();
                            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                            nuevaCategoria = (Categoria)cboListar.SelectedItem;
                            nuevaCategoria.Descripcion = txtModificar.Text;
                            categoriaNegocio.modificar(nuevaCategoria);
                            MessageBox.Show("Categoria modificada correctamente");
                            txtModificar.Text = "";
                            actualizarCategorias();
                        }
                        else
                        {
                            MessageBox.Show("Por favor, ingrese un texto válido.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void actualizarMarcas() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            cboListar.DataSource = marcaNegocio.listar();
        }
        private void actualizarCategorias() 
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            categoriaNegocio.listar();
            cboListar.DataSource = categoriaNegocio.listar();
        }
    }
}
