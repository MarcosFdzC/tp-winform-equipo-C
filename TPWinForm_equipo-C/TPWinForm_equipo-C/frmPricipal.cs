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
    public partial class frmPricipal : Form
    {
        private List<Articulo> listaArticulos;
        public frmPricipal()
        {
            InitializeComponent();
        }

        private void frmPricipal_Load(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.listar();
                dataGridView1.DataSource = listaArticulos;
                dataGridView1.AutoResizeColumns();
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message + ". Por favor contacte a soporte.");
            }
        }

        private void ocultarColumnas()
        {
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Codigo"].Visible = false;
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper())); // Esta linea funciona como un foreach que recorre la lista continuamente hasta encontrar lo que se le pida
                // Por ahora solo busca por Nombre, Descripcion y Marca. Se pueden agregar tantas condiciones como se requiera.
            }
            else
            {
                listaFiltrada = listaArticulos; // Si buscamos sin ningun texto se resetea la busqueda y muestra toda la lista
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
        }
    }
}
