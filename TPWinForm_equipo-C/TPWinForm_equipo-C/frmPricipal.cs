using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        int index = 0;
        public frmPricipal()
        {
            InitializeComponent();
        }

        private void frmPricipal_Load(object sender, EventArgs e)
        {
            cargar();
            cargarCampos();

        }
        private void cargar()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.listar();
                dataGridView1.DataSource = listaArticulos;
                dataGridView1.AutoResizeColumns();
                Articulo seleccionado = new Articulo();
                seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagenes[0].ImagenUrl);
                ocultarColumnas();
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "F2";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message + ". Por favor contacte a soporte.");
            }
        }

        private void cargarCampos()
        {
            try
            {
                cboCampo.Items.Add("Nombre");
                cboCampo.Items.Add("Descripcion");
                cboCampo.Items.Add("Marca");
                cboCampo.Items.Add("Categoria");
                cboCampo.Items.Add("Precio");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Codigo"].Visible = false;
        }
        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor seleccione un campo..");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor seleccione un criterio..");
                return true;
            }
            if (cboCampo.SelectedItem.ToString() == "Número")
            {
                if ( string.IsNullOrEmpty(txtFiltro.Text) )
                {
                    MessageBox.Show("Debes cargar el filtro para numéricos..");
                    return true;
                }
                if ( !(soloNumeros(txtFiltro.Text)) )
                {
                    MessageBox.Show("Solo números para cargar por un campo numérico");
                    return true;
                }
            }
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if ( !( char.IsNumber(caracter) ) )
                    return false;
            }
            return true;
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                    return;
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dataGridView1.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            switch (opcion)
            {
                case "Precio":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Mayor a:");
                    cboCriterio.Items.Add("Menor a:");
                    cboCriterio.Items.Add("Igual a:");
                    break;
                case "Nombre":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con:");
                    cboCriterio.Items.Add("Termina con:");
                    cboCriterio.Items.Add("Contiene:");
                    break;
                case "Marca":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con:");
                    cboCriterio.Items.Add("Termina con:");
                    cboCriterio.Items.Add("Contiene:");
                    break;
                case "Categoria":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con:");
                    cboCriterio.Items.Add("Termina con:");
                    cboCriterio.Items.Add("Contiene:");
                    break;
            }
        }

        private void btnFiltrarEsp_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dataGridView1.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
            frmAltaArticulo alta = new frmAltaArticulo(seleccionado);
            alta.ShowDialog();
            cargar();
        }
        
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            index = 0;
            Articulo seleccionado = new Articulo();
            if (dataGridView1.CurrentRow != null)
            {
                seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagenes[0].ImagenUrl);
            }
        }

        private void btnEliminarL_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Eliminar este articulo de manera permanente?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                    negocio.eliminarLogico(seleccionado);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la eliminación lógica: " + ex.Message);
            }
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imgNegocio = new ImagenNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Eliminar este articulo de manera permanente?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                    imgNegocio.eliminarFisico(seleccionado);
                    negocio.eliminarFisico(seleccionado);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la eliminación lógica: " + ex.Message);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            art = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
            frmVerDetalles detalles = new frmVerDetalles(art);
            detalles.ShowDialog();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    
                    Articulo seleccionado = new Articulo();
                    seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                    int max = seleccionado.Imagenes.Count;
                    if (index < max -1)
                    {
                        index++;
                        if (!(string.IsNullOrWhiteSpace(seleccionado.Imagenes[index].ImagenUrl)))
                            cargarImagen(seleccionado.Imagenes[index].ImagenUrl);
                        else
                            pbxArticulo.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
                    }

                }
            }
            catch(Exception ex)
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

                    if (dataGridView1.CurrentRow != null)
                    {
                        Articulo seleccionado = new Articulo();
                        seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                        if (!(string.IsNullOrWhiteSpace(seleccionado.Imagenes[index].ImagenUrl)))
                            cargarImagen(seleccionado.Imagenes[index].ImagenUrl);
                        else
                            pbxArticulo.Load("https://imgs.search.brave.com/kb8wBMhFd0vGUo9uR3fzClIsRoWkr9QnZ69Le5BgQiI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/Zm90b3MtcHJlbWl1/bS9maWd1cmEtZGli/dWpvcy1hbmltYWRv/cy1iYXJiYS1nYWZh/cy1tdWVzdHJhLW1l/bnNhamUtZXJyb3It/NDA0LWZyZW50ZS1j/aWVsby1zb2xlYWRv/LW51YmVzXzkxMTYy/MC0zNDQ4MC5qcGc_/c2l6ZT02MjYmZXh0/PWpwZw");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}