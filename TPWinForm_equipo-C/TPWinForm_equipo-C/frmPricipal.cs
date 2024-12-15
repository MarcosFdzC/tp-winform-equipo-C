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
        public frmPricipal()
        {
            InitializeComponent();
        }

        private void dataGridView1_Load(object sender, EventArgs e)
        {
            //POR ALGUNA RAZON NO PUEDO HACER QUE SE MUESTRE EN EL DATAGRIDVIEW ME SIENTO UN BURRO JASJDKA
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.listar();
                dataGridView1.DataSource = lista;
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            //AGREGUE SOLO ESTO
        }
    }
}
