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
                dataGridView1.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message + ". Por favor contacte a soporte.");
            }
        }
    }
}
