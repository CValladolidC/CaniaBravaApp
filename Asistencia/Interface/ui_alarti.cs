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
    public partial class ui_alarti : Form
    {
        Funciones funciones = new Funciones();
        string _codcia;

        public ui_alarti()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void ui_calculaNumRegistros()
        {
            AlArti alarti = new AlArti();
            string codcia = this._codcia;
            txtRegTotal.Text = string.Empty;
            string numreg = alarti.getNumeroRegistrosAlarti(codcia);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void ui_ListaAlarti()
        {
            string cadenaBusqueda = string.Empty;
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = "AND a.codarti LIKE '%" + @buscar + "%' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = "AND a.desarti like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = "AND a.codartiba='" + @buscar + "' ";
                    }
                }
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " select a.codarti AS [Código Interno],a.desarti AS [Nombre Descriptivo],a.Unidad,c.desmaesgen as Familia,";
            query += "b.Precio,CASE a.estado WHEN 'V' THEN 'VIGENTE' ELSE 'ANULADO' END AS Estado ";
            query += "from alarti a left join alarti_peri b on b.codarti = a.codarti and fechafin IS NULL ";
            query += "INNER JOIN maesgen c on c.idmaesgen='110' and c.clavemaesgen=a.famarti ";
            query += "WHERE 1=1 " + cadenaBusqueda;
            query += "ORDER BY a.codarti ASC;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];

                    dgvData.Columns[0].Width = 70;
                    dgvData.Columns[1].Width = 330;
                    dgvData.Columns[2].Width = 60;
                    dgvData.Columns[3].Width = 180;
                    dgvData.Columns[4].Width = 60;
                    dgvData.Columns[5].Width = 86;

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
            ui_ListaAlarti();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string codcia = this._codcia;
            ui_updalarti ui_updalarti = new ui_updalarti();
            ui_updalarti._FormPadre = this;
            ui_updalarti._clasePadre = "ui_alarti";
            ui_updalarti.Activate();
            ui_updalarti.ui_ActualizaComboBox();
            ui_updalarti.agregar();
            ui_updalarti.BringToFront();
            ui_updalarti.ShowDialog();
            ui_updalarti.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codarti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ui_updalarti ui_updalarti = new ui_updalarti();
                ui_updalarti._FormPadre = this;
                ui_updalarti._clasePadre = "ui_alarti";
                ui_updalarti._codcia = this._codcia;
                ui_updalarti.ui_ActualizaComboBox();
                ui_updalarti.Activate();
                ui_updalarti.BringToFront();
                ui_updalarti.editar(codarti);
                ui_updalarti.ShowDialog();
                ui_updalarti.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            ui_ListaAlarti();
        }

        private void radioButtonCodigoInterno_CheckedChanged(object sender, EventArgs e)
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
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codarti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desarti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                ui_delalarti ui_delalarti = new ui_delalarti();
                ui_delalarti._FormPadre = this;
                ui_delalarti._codcia = this._codcia;
                ui_delalarti._codarti = codarti;
                ui_delalarti._desarti = desarti;
                ui_delalarti.Activate();
                ui_delalarti.BringToFront();
                ui_delalarti.ShowDialog();
                ui_delalarti.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ui_alarti_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            ui_ListaAlarti();
        }

        private void radioButtonBarras_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void cmbComedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaAlarti();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ui_ListaAlarti();
        }
    }
}