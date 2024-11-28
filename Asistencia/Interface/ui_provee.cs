using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_provee : Form
    {
        Funciones funciones = new Funciones();
        public ui_provee()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_provee_Load(object sender, EventArgs e)
        {
            ui_ListaProvee();
        }

        public void ui_calculaNumRegistros()
        {
            Provee provee = new Provee();
            Funciones funciones = new Funciones();

            txtRegTotal.Text = string.Empty;
            string numreg = provee.getNumeroRegistrosProvee();
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void ui_ListaProvee()
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select codprovee,desprovee,ruc,CASE estado WHEN 'V' THEN 'VIGENTE' ELSE 'ANULADO' END,website,";
            query = query + "fcrea,fmod,usuario,dirprovee from provee ";
            query = query + "order by codprovee asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Codigo";
                    dgvData.Columns[1].HeaderText = "Apellidos y Nombres ó Razón Social";
                    dgvData.Columns[2].HeaderText = "R.U.C.";
                    dgvData.Columns[3].HeaderText = "Estado";
                    dgvData.Columns["website"].Visible = false;
                    dgvData.Columns["fcrea"].Visible = false;
                    dgvData.Columns["fmod"].Visible = false;
                    dgvData.Columns["usuario"].Visible = false;
                    dgvData.Columns["dirprovee"].Visible = false;
                    dgvData.Columns[0].Width = 70;
                    dgvData.Columns[1].Width = 400;
                    dgvData.Columns[2].Width = 100;
                    dgvData.Columns[3].Width = 100;

                    dgvData.AllowUserToResizeRows = false;
                    dgvData.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvData.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                ui_calculaNumRegistros();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaProvee();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updprovee ui_updprovee = new ui_updprovee();
            ui_updprovee._FormPadre = this;
            ui_updprovee._tipo = "F";
            ui_updprovee.Activate();
            ui_updprovee.ui_listarComboBox();
            ui_updprovee.agregar();
            ui_updprovee.BringToFront();
            ui_updprovee.ShowDialog();
            ui_updprovee.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codprovee = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ui_updprovee ui_updprovee = new ui_updprovee();
                ui_updprovee._FormPadre = this;
                ui_updprovee._tipo = "F";
                ui_updprovee.Activate();
                ui_updprovee.BringToFront();
                ui_updprovee.ui_listarComboBox();
                ui_updprovee.editar(codprovee);
                ui_updprovee.ShowDialog();
                ui_updprovee.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                Funciones funciones = new Funciones();
                txtRegEncontrados.Text = string.Empty;
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " codprovee='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " desprovee like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " ruc='" + @buscar + "' ";
                    }

                }

                string query = "select codprovee,desprovee,ruc,estado,website,";
                query = query + "fcrea,fmod,usuario,dirprovee from provee ";
                query = query + "where " + @cadenaBusqueda + "order by codprovee asc;";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblData");
                        funciones.formatearDataGridView(dgvData);
                        dgvData.DataSource = myDataSet.Tables["tblData"];
                        dgvData.Columns[0].HeaderText = "Codigo";
                        dgvData.Columns[1].HeaderText = "Apellidos y Nombres ó Razón Social";
                        dgvData.Columns[2].HeaderText = "R.U.C.";
                        dgvData.Columns[3].HeaderText = "Estado";
                        dgvData.Columns["website"].Visible = false;
                        dgvData.Columns["fcrea"].Visible = false;
                        dgvData.Columns["fmod"].Visible = false;
                        dgvData.Columns["usuario"].Visible = false;
                        dgvData.Columns["dirprovee"].Visible = false;
                        dgvData.Columns[0].Width = 70;
                        dgvData.Columns[1].Width = 350;
                        dgvData.Columns[2].Width = 100;
                        dgvData.Columns[3].Width = 100;
                    }

                    ui_calculaNumRegistros();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaProvee();
        }

        private void radioButtonCodigoInterno_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonRuc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codprovee = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprovee = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Proveedor " + @desprovee + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Provee provee = new Provee();
                    provee.delProvee(codprovee);
                    this.ui_ListaProvee();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}